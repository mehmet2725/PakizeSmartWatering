using Microsoft.EntityFrameworkCore;
using PakizeSmartWatering.Application.Repositories;
using PakizeSmartWatering.Application.Services;
using PakizeSmartWatering.Infrastructure.Persistence;
using PakizeSmartWatering.Infrastructure.Repositories;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// ==========================================================
// 1. VERİTABANI BAĞLANTISI (Plesk SQL Server)
// ==========================================================
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("PleskSqlConnection")));

// ==========================================================
// 2. BAĞIMLILIK ENJEKSİYONU (Dependency Injection)
// ==========================================================
// Repositories (Veritabanı Kasları)
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IPlantRepository, PlantRepository>();
builder.Services.AddScoped<IDeviceRepository, DeviceRepository>();
builder.Services.AddScoped<IMoistureLogRepository, MoistureLogRepository>();
builder.Services.AddScoped<IWateringHistoryRepository, WateringHistoryRepository>();

// Services (İş Kuralları - Beyin)
builder.Services.AddScoped<IMoistureService, MoistureService>();
builder.Services.AddScoped<IWateringService, WateringService>();

// ==========================================================
// 3. API VE DÖKÜMANTASYON (.NET 10 OpenAPI + Scalar)
// ==========================================================
builder.Services.AddControllers();
builder.Services.AddOpenApi(); // Yeni nesil Swagger alternatifi

// Frontend'in (Next.js) API'ye erişebilmesi için CORS izni veriyoruz
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowNextjs", policy =>
    {
        policy.WithOrigins("http://localhost:3000") // Vercel'e atınca buraya Vercel linkini de ekleyeceğiz
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

var app = builder.Build();

// ==========================================================
// 4. HTTP İSTEK BORU HATTI (Pipeline)
// ==========================================================
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    // Scalar arayüzünü /scalar/v1 adresinde ayağa kaldırır
    app.MapScalarApiReference(); 
}

app.UseHttpsRedirection();
app.UseCors("AllowNextjs");
app.UseAuthorization();
app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    // Sunucuya yüklendiğinde tablolar yoksa kendi kendine oluşturur
    dbContext.Database.Migrate(); 
}

app.Run();