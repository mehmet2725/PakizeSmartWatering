using System;
using System.Threading.Tasks;

namespace PakizeSmartWatering.Application.Services;

public interface IDeviceService
{
    // Cihazdan her nem verisi geldiğinde veya "Ben buradayım" sinyali (Ping) geldiğinde son görülme zamanını günceller
    Task UpdateDeviceLastSeenAsync(Guid deviceId);

    // Cihazın şu an internete ve sisteme bağlı olup olmadığını kontrol eder
    Task<bool> IsDeviceOnlineAsync(Guid deviceId);
}