import api from './axios';
import type { JournalEntry } from '../types';

export const journalEntriesApi = {
  getAll: async (): Promise<JournalEntry[]> => {
    const { data } = await api.get('/api/journal-entries');
    return data;
  },

  delete: async (id: number): Promise<void> => {
    await api.delete(`/api/journal-entries/${id}`);
  },
};