import api from './axios';
import type { Invoice } from '../types';

export const invoicesApi = {
  getAll: async (): Promise<Invoice[]> => {
    const { data } = await api.get('/api/invoices');
    return data;
  },

  getById: async (id: number): Promise<Invoice> => {
    const { data } = await api.get(`/api/invoices/${id}`);
    return data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/api/invoices/${id}`);
  },
};