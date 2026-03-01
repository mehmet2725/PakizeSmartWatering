"use client";

import { useState, useEffect } from "react";
import { motion } from "framer-motion";
import { History, Clock, CalendarCheck } from "lucide-react";
import { api } from "../services/api";
import { WateringHistory } from "../types";

export default function HistoryTable() {
    const [history, setHistory] = useState<WateringHistory[]>([]);
    const [loading, setLoading] = useState(true);

    // Gerçek ID'yi çevre değişkeninden alıyoruz
    const plantId = process.env.NEXT_PUBLIC_PLANT_ID;

    useEffect(() => {
        const fetchHistory = async () => {
            if (!plantId) return; // ID henüz yüklenmediyse bekle
            
            try {
                const response = await api.get(`/Watering/history/${plantId}`);

                // Gelen veriyi tarihe göre en yeniden en eskiye sıralıyoruz
                const sortedData = response.data.sort((a: WateringHistory, b: WateringHistory) =>
                    new Date(b.createdAt).getTime() - new Date(a.createdAt).getTime()
                );

                setHistory(sortedData);
            } catch (error) {
                console.error("Geçmiş verileri çekilemedi!", error);
            } finally {
                setLoading(false);
            }
        };

        // 1. Sayfa ilk açıldığında çalıştır
        fetchHistory();

        // 2. Havaya atılan 'wateringCompleted' sinyalini dinle, duyarsan tekrar çalıştır!
        window.addEventListener("wateringCompleted", fetchHistory);

        // 3. Bileşen ekrandan giderse dinlemeyi bırak (Performans için önemli)
        return () => {
            window.removeEventListener("wateringCompleted", fetchHistory);
        };
    }, [plantId]);

    return (
        <motion.div
            initial={{ opacity: 0, y: 30 }}
            animate={{ opacity: 1, y: 0 }}
            transition={{ duration: 0.6, delay: 0.2 }}
            className="mt-12 w-full max-w-md bg-neutral-900/50 border border-neutral-800 rounded-3xl p-6 shadow-xl"
        >
            <div className="flex items-center space-x-2 mb-6 text-neutral-300">
                <History className="w-5 h-5 text-emerald-400" />
                <h3 className="text-lg font-bold tracking-wide">Son Sulama İşlemleri</h3>
            </div>

            {loading ? (
                <div className="text-center text-neutral-500 py-4 animate-pulse">
                    Kayıtlar yükleniyor...
                </div>
            ) : history.length === 0 ? (
                <div className="text-center text-neutral-500 py-4">
                    Henüz sulama kaydı yok.
                </div>
            ) : (
                <div className="space-y-4 max-h-64 overflow-y-auto pr-2 custom-scrollbar">
                    {history.map((item, index) => (
                        <motion.div
                            key={item.id}
                            initial={{ opacity: 0, x: -20 }}
                            animate={{ opacity: 1, x: 0 }}
                            transition={{ delay: index * 0.1 }}
                            className="bg-neutral-950 border border-neutral-800 rounded-2xl p-4 flex items-center justify-between hover:border-neutral-700 transition-colors"
                        >
                            <div className="flex items-center space-x-4">
                                <div className={`p-2 rounded-full ${item.isManualTrigger ? 'bg-emerald-500/10 text-emerald-400' : 'bg-blue-500/10 text-blue-400'}`}>
                                    <CalendarCheck className="w-5 h-5" />
                                </div>
                                <div>
                                    <p className="text-neutral-200 text-sm font-medium">
                                        {new Date(item.createdAt + "Z").toLocaleDateString("tr-TR", {
                                            day: "numeric",
                                            month: "long",
                                            hour: "2-digit",
                                            minute: "2-digit"
                                        })}
                                    </p>
                                    <p className="text-neutral-500 text-xs mt-1">
                                        {item.isManualTrigger ? "Manuel (Sen suladın)" : "Otonom (Sensör suladı)"}
                                    </p>
                                </div>
                            </div>
                            <div className="flex items-center space-x-1 text-neutral-400 bg-neutral-900 px-3 py-1 rounded-full text-xs font-bold border border-neutral-800">
                                <Clock className="w-3 h-3" />
                                <span>{item.pumpRunDurationSeconds}s</span>
                            </div>
                        </motion.div>
                    ))}
                </div>
            )}
        </motion.div>
    );
}