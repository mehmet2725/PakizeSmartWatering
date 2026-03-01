import axios from 'axios';

export const api = axios.create({
  // Adresi .env dosyasından çekiyoruz. Eğer bulamazsa yedeği localhost olur.
  baseURL: process.env.NEXT_PUBLIC_API_URL || 'http://localhost:5064/api',
  headers: {
    'Content-Type': 'application/json',
  },
});