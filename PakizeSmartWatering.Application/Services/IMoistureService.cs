using System;
using System.Threading.Tasks;

namespace PakizeSmartWatering.Application.Services;

public interface IMoistureService
{
    // ESP8266'dan gelen ham nem verisini kaydetme ve otonom sulama kontrolü
    Task ProcessNewMoistureDataAsync(Guid deviceId, double moisturePercentage);

    // Next.js (Frontend) ekranında Pakize'nin anlık nem durumunu göstermek için
    Task<double> GetLatestMoistureLevelAsync(Guid plantId);
}