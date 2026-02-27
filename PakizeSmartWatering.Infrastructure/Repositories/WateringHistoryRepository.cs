using PakizeSmartWatering.Application.Repositories;
using PakizeSmartWatering.Domain.Entities;
using PakizeSmartWatering.Infrastructure.Persistence;

namespace PakizeSmartWatering.Infrastructure.Repositories;

public class WateringHistoryRepository : Repository<WateringHistory>, IWateringHistoryRepository
{
    public WateringHistoryRepository(AppDbContext context) : base(context)
    {
    }
}