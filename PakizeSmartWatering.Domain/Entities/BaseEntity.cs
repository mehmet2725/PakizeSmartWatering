using System;

namespace PakizeSmartWatering.Domain.Entities;

public abstract class BaseEntity
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow; // Plesk sunucusu nerede olursa olsun, saat karmaşası olmasın diye Evrensel Saat (UTC) tutuyoruz.
}