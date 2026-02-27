using Microsoft.EntityFrameworkCore;
using PakizeSmartWatering.Domain.Entities;

namespace PakizeSmartWatering.Infrastructure.Persistence;

public class AppDbContext : DbContext
{
    // API katmanından (Program.cs) veritabanı yolunu (Connection String) gönderebilmek için
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Veritabanında oluşacak tablolarımız
    public DbSet<Plant> Plants { get; set; }
    public DbSet<Device> Devices { get; set; }
    public DbSet<MoistureLog> MoistureLogs { get; set; }
    public DbSet<WateringHistory> WateringHistories { get; set; }

    // Fluent API ile veritabanı kurallarımızı (Kısıtlamalar, İlişkiler) burada yazıyoruz
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // 1. Plant (Pakize) ve Device (NodeMCU) İlişkisi: 1'e 1 ilişki (Şimdilik her bitkinin 1 cihazı var)
        modelBuilder.Entity<Plant>()
            .HasOne(p => p.Device)
            .WithOne(d => d.Plant)
            .HasForeignKey<Plant>(p => p.DeviceId);

        // 2. Plant ve MoistureLog İlişkisi: 1'e Çok (Pakize'nin birden çok nem kaydı olur)
        modelBuilder.Entity<MoistureLog>()
            .HasOne(m => m.Plant)
            .WithMany(p => p.MoistureLogs)
            .HasForeignKey(m => m.PlantId)
            .OnDelete(DeleteBehavior.Cascade); // Pakize silinirse, nem geçmişi de silinsin

        // 3. Plant ve WateringHistory İlişkisi: 1'e Çok
        modelBuilder.Entity<WateringHistory>()
            .HasOne(w => w.Plant)
            .WithMany(p => p.WateringHistories)
            .HasForeignKey(w => w.PlantId)
            .OnDelete(DeleteBehavior.Cascade);

        // Bazı sütun kısıtlamaları (İsimlerin çok uzun olmasını engellemek performansı artırır)
        modelBuilder.Entity<Plant>().Property(p => p.Name).HasMaxLength(100).IsRequired();
        modelBuilder.Entity<Device>().Property(d => d.MacAddress).HasMaxLength(50).IsRequired();
    }
}