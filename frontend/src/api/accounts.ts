import api from './axios';
import type { Account, CreateAccountDto } from '../types';

export const accountsApi = {
  getAll: async (): Promise<Account[]> => {
    const { data } = await api.get('/api/accounts');
    return data;
  },

  getById: async (id: number): Promise<Account> => {
    const { data } = await api.get(`/api/accounts/${id}`);
    return data;
  },

  create: async (dto: CreateAccountDto): Promise<Account> => {
    const { data } = await api.post('/api/accounts', dto);
    return data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/api/accounts/${id}`);
  },
};