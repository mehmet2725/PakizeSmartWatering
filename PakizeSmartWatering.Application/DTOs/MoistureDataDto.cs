using System;

namespace PakizeSmartWatering.Application.DTOs;

// NodeMCU'dan API'ye gelecek olan JSON verisinin C# karşılığı
public record MoistureDataDto(Guid DeviceId, double MoisturePercentage);