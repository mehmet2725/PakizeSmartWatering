using System;

namespace PakizeSmartWatering.Domain.Entities;

public class MoistureLog : BaseEntity
{
    public Guid PlantId { get; set; }
    public Plant? Plant { get; set; }
    
    // Yüzdelik nem değeri (0-100 arası)
    public double MoisturePercentage { get; set; } 
}