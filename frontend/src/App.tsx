import { BrowserRouter, Routes, Route } from 'react-router-dom';
import { QueryClient, QueryClientProvider } from '@tanstack/react-query';
import Layout from '@/components/layout/Layout';
import Dashboard from '@/pages/Dashboard';
import AccountsList from '@/pages/Accounts/AccountsList';
import InvoicesList from '@/pages/Invoices/InvoicesList';
import JournalEntriesList from '@/pages/JournalEntries/JournalEntriesList';

const queryClient = new QueryClient({
  defaultOptions: {
    queries: {
      refetchOnWindowFocus: false,
      retry: 1,
    },
  },
});

export default function App() {
  return (
    <QueryClientProvider client={queryClient}>
      <BrowserRouter>
        <Routes>
          <Route element={<Layout />}>
            <Route path="/" element={<Dashboard />} />
            <Route path="/accounts" element={<AccountsList />} />
            <Route path="/invoices" element={<InvoicesList />} />
            <Route path="/journal-entries" element={<JournalEntriesList />} />
          </Route>
        </Routes>
      </BrowserRouter>
    </QueryClientProvider>
  );
}