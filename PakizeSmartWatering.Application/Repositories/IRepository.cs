using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PakizeSmartWatering.Domain.Entities;

namespace PakizeSmartWatering.Application.Repositories;

// T parametresi sadece BaseEntity'den miras alan sınıflar (Plant, Device vb.) olabilir
public interface IRepository<T> where T : BaseEntity
{
    Task<T?> GetByIdAsync(Guid id);
    Task<IEnumerable<T>> GetAllAsync();
    Task AddAsync(T entity);
    Task UpdateAsync(T entity);
    Task DeleteAsync(T entity);
}