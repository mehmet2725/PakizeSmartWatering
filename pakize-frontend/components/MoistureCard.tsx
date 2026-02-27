"use client"; // Next.js'e bunun tarayıcıda çalışacak bir arayüz parçası olduğunu söylüyoruz

import { motion } from "framer-motion";
import { Droplets } from "lucide-react";

interface MoistureCardProps {
  percentage: number;
}

export default function MoistureCard({ percentage }: MoistureCardProps) {
  // Neme göre renk ve mesaj belirliyoruz (Otomatik tasarım zekası!)
  const isDry = percentage < 30;
  const statusColor = isDry ? "text-red-400" : "text-emerald-400";
  const ringColor = isDry ? "ring-red-500/30" : "ring-emerald-500/30";
  const glowColor = isDry ? "bg-red-500" : "bg-emerald-500";
  const message = isDry ? "Çok susadım kanka! 🥵" : "Keyfim yerinde 🌿";

  return (
    <motion.div
      // Ekrana gelirken aşağıdan yumuşakça belirme animasyonu
      initial={{ opacity: 0, y: 30 }}
      animate={{ opacity: 1, y: 0 }}
      transition={{ duration: 0.6, ease: "easeOut" }}
      className="bg-neutral-900 border border-neutral-800 rounded-3xl p-8 flex flex-col items-center justify-center shadow-2xl relative overflow-hidden w-full max-w-sm"
    >
      {/* Arka plan köşe parlama efekti (Ambiyans için) */}
      <div className={`absolute -top-10 -right-10 w-32 h-32 rounded-full blur-[60px] opacity-20 ${glowColor}`} />

      {/* Başlık */}
      <div className="flex items-center space-x-2 mb-8 text-neutral-400">
        <Droplets className="w-5 h-5" />
        <h2 className="text-sm font-medium uppercase tracking-widest">Toprak Nemi</h2>
      </div>

      {/* Yuvarlak Nem Göstergesi (Nefes Alma Animasyonlu) */}
      <motion.div 
        animate={{ scale: [1, 1.02, 1] }}
        transition={{ repeat: Infinity, duration: 4, ease: "easeInOut" }}
        className={`w-40 h-40 rounded-full flex items-center justify-center ring-4 ring-offset-[12px] ring-offset-neutral-900 ${ringColor} mb-8`}
      >
        <span className={`text-6xl font-bold ${statusColor}`}>
          %{percentage}
        </span>
      </motion.div>

      {/* Durum Mesajı */}
      <p className="text-neutral-300 font-medium text-lg">{message}</p>
    </motion.div>
  );
}