using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PakizeSmartWatering.Domain.Entities;

namespace PakizeSmartWatering.Application.Services;

public interface IWateringService
{
    // Web üzerinden (Next.js) senin manuel olarak butona basıp sulama başlatman
    Task<bool> TriggerManualWateringAsync(Guid plantId, int durationSeconds, string pinCode);

    // Frontend'de "Geçmiş Sulamalar" tablosunu göstermek için
    Task<IEnumerable<WateringHistory>> GetWateringHistoryAsync(Guid plantId);
}