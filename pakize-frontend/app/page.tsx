"use client";

import { useState, useEffect } from "react";
import { api } from "@/services/api";
import MoistureCard from "@/components/MoistureCard";
import WaterAction from "@/components/WaterAction";
import HistoryTable from "@/components/HistoryTable";

export default function Home() {
  const [moisture, setMoisture] = useState<number>(0);

  const fetchMoisture = async () => {
    try {
      const deviceId = process.env.NEXT_PUBLIC_DEVICE_ID;
      const response = await api.get(`/Moisture/last/${deviceId}`);
      
      if (response.data) {
        setMoisture(response.data.moisturePercentage);
      }
    } catch (error) {
      console.error("Veri çekilemedi:", error);
    }
  };

  useEffect(() => {
    fetchMoisture();
    const interval = setInterval(fetchMoisture, 5000);
    return () => clearInterval(interval);
  }, []);

  return (
    <main className="min-h-screen bg-neutral-950 text-white py-12 px-6">
      {/* Tüm içeriği sınırlayan merkezi kapsayıcı */}
      <div className="max-w-4xl mx-auto flex flex-col items-center">
        
        {/* Header Bölümü */}
        <div className="text-center mb-12">
          <h1 className="text-4xl md:text-5xl font-extrabold tracking-tight mb-4">
            Pakize <span className="text-emerald-400">Dashboard</span>
          </h1>
          <p className="text-neutral-400">Otonom Akıllı Sulama Sistemi</p>
        </div>

        {/* Ana Grid veya Layout Düzeni */}
        <div className="w-full grid grid-cols-1 md:grid-cols-2 gap-8 items-start">
          {/* Sol taraf: Nem Kartı */}
          <div className="flex justify-center">
            <MoistureCard percentage={moisture} />
          </div>

          {/* Sağ taraf: Su Aksiyonu */}
          <div className="flex flex-col justify-center h-full">
            <WaterAction />
          </div>
        </div>

        {/* Alt Kısım: Tablo (Tam Genişlik) */}
        <div className="w-full mt-16">
          <div className="flex items-center mb-6">
             <div className="w-1 h-6 bg-emerald-500 rounded-full mr-3" />
             <h3 className="text-xl font-semibold">Son Ölçümler</h3>
          </div>
          <div className="bg-neutral-900/50 rounded-xl p-4 border border-white/5">
            <HistoryTable />
          </div>
        </div>
        
      </div>
    </main>
  );
}