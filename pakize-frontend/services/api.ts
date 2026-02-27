import axios from 'axios';

export const api = axios.create({
  // Senin .NET API'nin adresi (Test ederken portun 5064'tü, eğer değişirse burayı güncelleriz)
  baseURL: 'http://localhost:5064/api',
  headers: {
    'Content-Type': 'application/json',
  },
});