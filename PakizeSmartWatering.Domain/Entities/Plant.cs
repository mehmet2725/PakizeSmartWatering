using System;
using System.Collections.Generic;

namespace PakizeSmartWatering.Domain.Entities;

public class Plant : BaseEntity
{
    public string Name { get; set; } = string.Empty; // "Pakize"
    public string Species { get; set; } = string.Empty; // "Gynura Aurantiaca"
    
    // Sulamayı tetikleyecek minimum nem yüzdesi (Örn: %30)
    public double MoistureThreshold { get; set; } 
    
    // Hangi cihaza bağlı? (Yabancı Anahtar / Foreign Key)
    public Guid DeviceId { get; set; } 
    public Device? Device { get; set; } 

    // Pakize'nin geçmiş veri kayıtları (Bire-Çok ilişki)
    public ICollection<MoistureLog> MoistureLogs { get; set; } = new List<MoistureLog>();
    public ICollection<WateringHistory> WateringHistories { get; set; } = new List<WateringHistory>();
}