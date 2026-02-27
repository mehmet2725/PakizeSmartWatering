using System;

namespace PakizeSmartWatering.Domain.Entities;

public class WateringHistory : BaseEntity
{
    public Guid PlantId { get; set; }
    public Plant? Plant { get; set; }

    // Next.js'ten sen basarsan True, sensör %30'un altını görüp kendi suladıysa False olacak.
    public bool IsManualTrigger { get; set; } 
    
    // Pompanın kaç saniye çalıştığı bilgisi (Belki ileride 5 saniye az gelir, 10 saniye çalıştırırız diye logluyoruz)
    public int PumpRunDurationSeconds { get; set; } 
}