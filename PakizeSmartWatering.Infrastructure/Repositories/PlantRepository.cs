using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PakizeSmartWatering.Application.Repositories;
using PakizeSmartWatering.Domain.Entities;
using PakizeSmartWatering.Infrastructure.Persistence;

namespace PakizeSmartWatering.Infrastructure.Repositories;

// Hem ortak Repository'den miras alıyor (CRUD için), hem de IPlantRepository sözleşmesini imzalıyor
public class PlantRepository : Repository<Plant>, IPlantRepository
{
    public PlantRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<Plant?> GetPlantWithDetailsAsync(Guid id)
    {
        // Pakize'yi getirirken bağlı olduğu cihazı ve sulama geçmişini de sorguya dahil et (JOIN)
        return await _context.Plants
            .Include(p => p.Device)
            .Include(p => p.WateringHistories)
            .Include(p => p.MoistureLogs.OrderByDescending(m => m.CreatedAt).Take(10)) // Sadece son 10 nem kaydını al ki performansı yormasın
            .FirstOrDefaultAsync(p => p.Id == id);
    }
}