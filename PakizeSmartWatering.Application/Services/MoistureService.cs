using System;
using System.Threading.Tasks;
using PakizeSmartWatering.Application.Repositories;
using PakizeSmartWatering.Domain.Entities;

namespace PakizeSmartWatering.Application.Services;

public class MoistureService : IMoistureService
{
    private readonly IMoistureLogRepository _moistureLogRepository;
    private readonly IPlantRepository _plantRepository;

    public MoistureService(IMoistureLogRepository moistureLogRepository, IPlantRepository plantRepository)
    {
        _moistureLogRepository = moistureLogRepository;
        _plantRepository = plantRepository;
    }

    public async Task ProcessNewMoistureDataAsync(Guid deviceId, double moisturePercentage)
    {
        // 1. Tüm bitkileri getir ve bu cihaza (deviceId) bağlı olan bitkiyi bul
        var plants = await _plantRepository.GetAllAsync();
        var myPlant = plants.FirstOrDefault(p => p.DeviceId == deviceId);

        if (myPlant == null)
        {
            throw new Exception("Bu cihaza kayıtlı bir bitki bulunamadı!");
        }

        // 2. Bitkiyi bulduysak log kaydını gerçek PlantId ile oluştur
        var log = new MoistureLog
        {
            PlantId = myPlant.Id, // Artık Guid.Empty değil, gerçek Pakize'nin ID'si!
            MoisturePercentage = moisturePercentage,
            CreatedAt = DateTime.UtcNow
        };

        await _moistureLogRepository.AddAsync(log);

        // Otonom Sulama Mantığı (Gelecekteki eklentimiz)
        // Eğer moisturePercentage < myPlant.MoistureThreshold ise -> Suyu aç emri ver!
    }

    public async Task<double> GetLatestMoistureLevelAsync(Guid plantId)
    {
        // Burada veritabanından o bitkiye ait en son logu çekeceğiz.
        // Şimdilik arayüz hata vermesin diye sahte (mock) bir değer dönüyoruz.
        return await Task.FromResult(45.5);
    }
}