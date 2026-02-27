using System;

namespace PakizeSmartWatering.Application.DTOs;

// Next.js'ten API'ye (Manuel Sulama için) gelecek olan JSON verisinin C# karşılığı
public record WateringRequestDto(Guid PlantId, int DurationSeconds, string PinCode);