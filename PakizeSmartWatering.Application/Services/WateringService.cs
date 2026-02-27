using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PakizeSmartWatering.Application.Repositories;
using PakizeSmartWatering.Domain.Entities;

namespace PakizeSmartWatering.Application.Services;

public class WateringService : IWateringService
{
    private readonly IWateringHistoryRepository _wateringHistoryRepository;
    
    // Senin gizli şifren (İleride bunu appsettings.json içine taşıyıp daha güvenli yapacağız)
    private const string SECRET_PIN = "1234"; 

    public WateringService(IWateringHistoryRepository wateringHistoryRepository)
    {
        _wateringHistoryRepository = wateringHistoryRepository;
    }

    public async Task<bool> TriggerManualWateringAsync(Guid plantId, int durationSeconds, string pinCode)
    {
        // 1. Güvenlik Kontrolü: PIN doğru mu?
        if (pinCode != SECRET_PIN)
        {
            // Şifre yanlışsa işlem yapma, false dön.
            return false; 
        }

        // 2. Şifre doğruysa, sulama geçmişine bu işlemi kaydet.
        var history = new WateringHistory
        {
            PlantId = plantId,
            IsManualTrigger = true,
            PumpRunDurationSeconds = durationSeconds,
            CreatedAt = DateTime.UtcNow
        };

        await _wateringHistoryRepository.AddAsync(history);

        // 3. TODO: Burada ESP8266'ya suyu açması için bir sinyal/komut gönderilecek.
        // (Bunu API katmanında ESP8266'nın bize attığı isteğe cevap olarak veya SignalR ile yapacağız).

        return true; // İşlem başarılı!
    }

    public async Task<IEnumerable<WateringHistory>> GetWateringHistoryAsync(Guid plantId)
    {
        // Tüm geçmişi getir
        return await _wateringHistoryRepository.GetAllAsync();
    }
}