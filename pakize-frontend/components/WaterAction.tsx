"use client";

import { useState } from "react";
import { motion, AnimatePresence } from "framer-motion";
import { Droplet, Lock, Timer, X, Loader2 } from "lucide-react";
import { api } from "../services/api";

export default function WaterAction() {
  const [isOpen, setIsOpen] = useState(false);
  const [pin, setPin] = useState("");
  const [duration, setDuration] = useState(5);
  const [isLoading, setIsLoading] = useState(false);
  const [message, setMessage] = useState<{ text: string; type: "success" | "error" | null }>({ text: "", type: null });

  const handleWatering = async () => {
    setIsLoading(true);
    setMessage({ text: "", type: null });

    try {
      // API'mize Sulama İsteğini Atıyoruz
      const response = await api.post("/Watering/trigger", {
        // TODO: Buradaki plantId'yi ileride API'den dinamik çekeceğiz, şimdilik test ID'ni yapıştırabilirsin
        plantId: "e82174f9-bd63-45cc-b2dc-adaa8d1951c3", 
        durationSeconds: duration,
        pinCode: pin
      });

      setMessage({ text: "💧 " + response.data.message, type: "success" });

      // YENİ EKLENEN SATIR: Başarılı olunca havaya 'wateringCompleted' adında bir sinyal fişeği atıyoruz!
      window.dispatchEvent(new Event("wateringCompleted"));
      
      // Başarılı olursa 2 saniye sonra pencereyi kapat
      setTimeout(() => {
        setIsOpen(false);
        setPin("");
        setMessage({ text: "", type: null });
      }, 2000);

    } catch (error: any) {
      // PIN yanlışsa veya sunucu hatası varsa
      const errorMsg = error.response?.data?.message || "Bir hata oluştu!";
      setMessage({ text: "❌ " + errorMsg, type: "error" });
    } finally {
      setIsLoading(false);
    }
  };

  return (
    <>
      {/* ANA SULA BUTONU */}
      <motion.button
        whileHover={{ scale: 1.05 }}
        whileTap={{ scale: 0.95 }}
        onClick={() => setIsOpen(true)}
        className="mt-8 bg-emerald-500 hover:bg-emerald-400 text-neutral-950 font-bold text-lg px-12 py-4 rounded-full flex items-center space-x-3 shadow-[0_0_30px_rgba(16,185,129,0.3)] transition-colors"
      >
        <Droplet className="w-6 h-6 fill-neutral-950" />
        <span>PAKİZE'Yİ SULA</span>
      </motion.button>

      {/* AÇILIR PENCERE (MODAL) */}
      <AnimatePresence>
        {isOpen && (
          <motion.div
            initial={{ opacity: 0 }}
            animate={{ opacity: 1 }}
            exit={{ opacity: 0 }}
            className="fixed inset-0 z-50 flex items-center justify-center p-4 bg-black/60 backdrop-blur-sm"
          >
            <motion.div
              initial={{ scale: 0.9, y: 20 }}
              animate={{ scale: 1, y: 0 }}
              exit={{ scale: 0.9, y: 20 }}
              className="bg-neutral-900 border border-neutral-800 p-6 rounded-3xl w-full max-w-sm relative shadow-2xl"
            >
              {/* Kapat Butonu */}
              <button 
                onClick={() => setIsOpen(false)}
                className="absolute top-4 right-4 text-neutral-400 hover:text-white"
              >
                <X className="w-6 h-6" />
              </button>

              <h3 className="text-xl font-bold text-white mb-6 flex items-center">
                <Lock className="w-5 h-5 mr-2 text-emerald-400" />
                Güvenlik Doğrulaması
              </h3>

              {/* Süre Seçimi */}
              <div className="mb-6">
                <label className="text-neutral-400 text-sm mb-2 flex items-center">
                  <Timer className="w-4 h-4 mr-2" /> Sulama Süresi (Saniye)
                </label>
                <div className="flex space-x-2">
                  {[3, 5, 10].map((sec) => (
                    <button
                      key={sec}
                      onClick={() => setDuration(sec)}
                      className={`flex-1 py-2 rounded-xl font-medium transition-colors ${
                        duration === sec 
                        ? "bg-emerald-500 text-neutral-950" 
                        : "bg-neutral-800 text-neutral-400 hover:bg-neutral-700"
                      }`}
                    >
                      {sec}s
                    </button>
                  ))}
                </div>
              </div>

              {/* PIN Girişi */}
              <div className="mb-6">
                <label className="text-neutral-400 text-sm mb-2 block">Gizli PIN Kodu</label>
                <input
                  type="password"
                  value={pin}
                  onChange={(e) => setPin(e.target.value)}
                  placeholder="****"
                  maxLength={4}
                  className="w-full bg-neutral-1000 bg-black/50 border border-neutral-800 rounded-xl px-4 py-3 text-white text-center text-2xl tracking-[0.5em] focus:outline-none focus:border-emerald-500 transition-colors"
                />
              </div>

              {/* Geri Bildirim Mesajı */}
              {message.text && (
                <div className={`mb-4 p-3 rounded-lg text-sm font-medium ${message.type === "success" ? "bg-emerald-500/10 text-emerald-400" : "bg-red-500/10 text-red-400"}`}>
                  {message.text}
                </div>
              )}

              {/* Onay Butonu */}
              <button
                onClick={handleWatering}
                disabled={isLoading || pin.length < 4}
                className="w-full bg-emerald-500 hover:bg-emerald-400 disabled:opacity-50 disabled:cursor-not-allowed text-neutral-950 font-bold py-3 rounded-xl flex items-center justify-center transition-colors"
              >
                {isLoading ? <Loader2 className="w-5 h-5 animate-spin" /> : "Suyu Aç"}
              </button>
            </motion.div>
          </motion.div>
        )}
      </AnimatePresence>
    </>
  );
}