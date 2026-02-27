using PakizeSmartWatering.Domain.Entities;

namespace PakizeSmartWatering.Application.Repositories;

public interface IDeviceRepository : IRepository<Device>
{
    // İleride "Mac Adresine Göre Cihaz Bul" gibi metotlar buraya gelecek.
}