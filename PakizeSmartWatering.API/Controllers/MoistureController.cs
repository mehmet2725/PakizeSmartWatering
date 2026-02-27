using Microsoft.AspNetCore.Mvc;
using PakizeSmartWatering.Application.DTOs;
using PakizeSmartWatering.Application.Services;

namespace PakizeSmartWatering.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MoistureController : ControllerBase
{
    private readonly IMoistureService _moistureService;

    public MoistureController(IMoistureService moistureService)
    {
        _moistureService = moistureService;
    }

    // ESP8266 buraya POST isteği atacak
    [HttpPost("data")]
    public async Task<IActionResult> ReceiveMoistureData([FromBody] MoistureDataDto request)
    {
        // Gelen veriyi Application katmanındaki servisimize iletiyoruz
        await _moistureService.ProcessNewMoistureDataAsync(request.DeviceId, request.MoisturePercentage);
        
        return Ok(new { message = "Nem verisi başarıyla kaydedildi kanka!" });
    }
}