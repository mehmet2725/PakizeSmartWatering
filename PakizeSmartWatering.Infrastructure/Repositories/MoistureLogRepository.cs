using PakizeSmartWatering.Application.Repositories;
using PakizeSmartWatering.Domain.Entities;
using PakizeSmartWatering.Infrastructure.Persistence;

namespace PakizeSmartWatering.Infrastructure.Repositories;

public class MoistureLogRepository : Repository<MoistureLog>, IMoistureLogRepository
{
    public MoistureLogRepository(AppDbContext context) : base(context)
    {
    }
}