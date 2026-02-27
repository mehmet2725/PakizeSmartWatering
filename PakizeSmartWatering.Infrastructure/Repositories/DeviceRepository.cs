using PakizeSmartWatering.Application.Repositories;
using PakizeSmartWatering.Domain.Entities;
using PakizeSmartWatering.Infrastructure.Persistence;

namespace PakizeSmartWatering.Infrastructure.Repositories;

public class DeviceRepository : Repository<Device>, IDeviceRepository
{
    public DeviceRepository(AppDbContext context) : base(context)
    {
    }
}