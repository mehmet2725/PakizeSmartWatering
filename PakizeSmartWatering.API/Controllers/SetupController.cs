using Microsoft.AspNetCore.Mvc;
using PakizeSmartWatering.Domain.Entities;
using PakizeSmartWatering.Infrastructure.Persistence;

namespace PakizeSmartWatering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SetupController : ControllerBase
{
    private readonly AppDbContext _context;

    public SetupController(AppDbContext context)
    {
        _context = context;
    }

    // Bu uç nokta sadece ilk kurulumda bir kez çalıştırılacak
    [HttpPost("init")]
    public async Task<IActionResult> InitializeSystem()
    {
        // Eğer veritabanında zaten bitki varsa tekrar ekleme
        if (_context.Plants.Any())
        {
            var existingPlant = _context.Plants.First();
            return Ok(new { Message = "Sistem zaten kurulu!", PlantId = existingPlant.Id, DeviceId = existingPlant.DeviceId });
        }

        // 1. Önce NodeMCU cihazımızı oluşturuyoruz
        var device = new Device
        {
            Id = Guid.NewGuid(),
            MacAddress = "A1:B2:C3:D4:E5:F6", // Gerçek donanım gelince bunu güncelleriz
            Name = "Pakize-NodeMCU",
            IsOnline = true
        };

        // 2. Sonra Pakize'yi oluşturup cihazı ona bağlıyoruz
        var plant = new Plant
        {
            Id = Guid.NewGuid(),
            Name = "Pakize",
            Species = "Gynura Aurantiaca",
            MoistureThreshold = 30.0,
            DeviceId = device.Id
        };

        _context.Devices.Add(device);
        _context.Plants.Add(plant);
        await _context.SaveChangesAsync();

        return Ok(new
        {
            Message = "Pakize ve Cihazı veritabanına başarıyla eklendi!",
            PlantId = plant.Id,
            DeviceId = device.Id
        });
    }
}