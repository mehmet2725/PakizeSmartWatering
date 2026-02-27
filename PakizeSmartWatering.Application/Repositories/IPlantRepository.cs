using System;
using System.Threading.Tasks;
using PakizeSmartWatering.Domain.Entities;

namespace PakizeSmartWatering.Application.Repositories;

// IRepository'nin tüm özelliklerini alır, üzerine özel metotlar ekler
public interface IPlantRepository : IRepository<Plant>
{
    // Pakize'nin tüm cihaz bilgileri ve geçmiş loglarıyla birlikte getirilmesi
    Task<Plant?> GetPlantWithDetailsAsync(Guid id);
}