using System;

namespace PakizeSmartWatering.Domain.Entities;

public class Device : BaseEntity
{
    public string MacAddress { get; set; } = string.Empty; // ESP8266'nın eşsiz MAC adresi
    public string Name { get; set; } = string.Empty; // Örn: "Oda-NodeMCU"
    public bool IsOnline { get; set; }
    public DateTime? LastSeenAt { get; set; } // Cihazdan en son ne zaman sinyal aldık?
    
    // Bir cihazın bir bitkisi olur (Şimdilik)
    public Plant? Plant { get; set; } 
}