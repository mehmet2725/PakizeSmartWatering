export interface MoistureLog {
  id: string;
  plantId: string;
  moisturePercentage: number;
  createdAt: string;
}

export interface WateringHistory {
  id: string;
  plantId: string;
  isManualTrigger: boolean;
  pumpRunDurationSeconds: number;
  createdAt: string;
}