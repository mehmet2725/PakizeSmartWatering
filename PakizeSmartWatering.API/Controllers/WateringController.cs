using Microsoft.AspNetCore.Mvc;
using PakizeSmartWatering.Application.DTOs;
using PakizeSmartWatering.Application.Services;

namespace PakizeSmartWatering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WateringController : ControllerBase
{
    private readonly IWateringService _wateringService;

    public WateringController(IWateringService wateringService)
    {
        _wateringService = wateringService;
    }

    // Next.js üzerinden "Sula" butonuna basıldığında burası çalışacak
    [HttpPost("trigger")]
    public async Task<IActionResult> TriggerWatering([FromBody] WateringRequestDto request)
    {
        // Servise PIN kodunu ve süreyi yolluyoruz, sonucu alıyoruz
        bool isSuccess = await _wateringService.TriggerManualWateringAsync(request.PlantId, request.DurationSeconds, request.PinCode);

        if (!isSuccess)
        {
            // Şifre yanlışsa 401 Unauthorized (Yetkisiz) hatası dönüyoruz
            return Unauthorized(new { message = "Hatalı PIN kodu! Sulama işlemi iptal edildi." });
        }

        return Ok(new { message = "Şifre doğru! Pakize sulanıyor..." });
    }

    // Frontend'de geçmişi göstermek için GET isteği
    [HttpGet("history/{plantId}")]
    public async Task<IActionResult> GetHistory(Guid plantId)
    {
        var history = await _wateringService.GetWateringHistoryAsync(plantId);
        return Ok(history);
    }
}