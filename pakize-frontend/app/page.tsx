import MoistureCard from "@/components/MoistureCard";
import WaterAction from "@/components/WaterAction";
import HistoryTable from "@/components/HistoryTable"; // YENİ EKLEDİK

export default function Home() {
  return (
    <main className="min-h-screen bg-neutral-950 text-white flex flex-col items-center py-12 px-6">
      
      <div className="text-center mb-10">
        <h1 className="text-4xl md:text-5xl font-extrabold tracking-tight mb-4">
          Pakize <span className="text-emerald-400">Dashboard</span>
        </h1>
        <p className="text-neutral-400">Otonom Akıllı Sulama Sistemi</p>
      </div>

      <MoistureCard percentage={45} />
      
      <WaterAction />

      {/* GEÇMİŞ TABLOSUNU BURAYA EKLEDİK */}
      <HistoryTable />
      
    </main>
  );
}