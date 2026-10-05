import { useQuery } from '@tanstack/react-query';
import { Wallet, FileText, BookOpen, TrendingUp } from 'lucide-react';
import { accountsApi } from '@/api/accounts';
import { invoicesApi } from '@/api/invoices';
import { journalEntriesApi } from '@/api/journalEntries';
import { formatCurrency, formatDate } from '@/lib/utils';

export default function Dashboard() {
  const { data: accounts = [], isLoading: loadingAccounts } = useQuery({
    queryKey: ['accounts'],
    queryFn: accountsApi.getAll,
  });

  const { data: invoices = [], isLoading: loadingInvoices } = useQuery({
    queryKey: ['invoices'],
    queryFn: invoicesApi.getAll,
  });

  const { data: journalEntries = [], isLoading: loadingEntries } = useQuery({
    queryKey: ['journalEntries'],
    queryFn: journalEntriesApi.getAll,
  });

  const totalInvoiceAmount = invoices.reduce((sum, inv) => sum + inv.total, 0);
  const isLoading = loadingAccounts || loadingInvoices || loadingEntries;

  const stats = [
    { label: 'حساب‌ها', value: accounts.length, icon: Wallet, color: 'bg-blue-500' },
    { label: 'فاکتورها', value: invoices.length, icon: FileText, color: 'bg-green-500' },
    { label: 'اسناد حسابداری', value: journalEntries.length, icon: BookOpen, color: 'bg-purple-500' },
    { label: 'مجموع فروش', value: formatCurrency(totalInvoiceAmount), icon: TrendingUp, color: 'bg-orange-500' },
  ];

  return (
    <div>
      <h1 className="text-3xl font-bold mb-8">📊 داشبورد</h1>

      {/* Stats Cards */}
      <div className="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-4 gap-6 mb-8">
        {stats.map((stat, index) => {
          const Icon = stat.icon;
          return (
            <div
              key={index}
              className="bg-white rounded-xl p-6 shadow-sm border border-slate-200 hover:shadow-md transition-shadow"
            >
              <div className="flex items-center justify-between mb-4">
                <div className={`${stat.color} p-3 rounded-lg text-white`}>
                  <Icon size={24} />
                </div>
              </div>
              <p className="text-slate-500 text-sm">{stat.label}</p>
              <p className="text-2xl font-bold mt-1">
                {isLoading ? '...' : stat.value}
              </p>
            </div>
          );
        })}
      </div>

      {/* Recent Invoices */}
      <div className="bg-white rounded-xl p-6 shadow-sm border border-slate-200">
        <h2 className="text-xl font-bold mb-4">📄 آخرین فاکتورها</h2>
        {loadingInvoices ? (
          <p className="text-slate-500 text-center py-8">در حال بارگذاری...</p>
        ) : invoices.length === 0 ? (
          <p className="text-slate-500 text-center py-8">هیچ فاکتوری وجود ندارد</p>
        ) : (
          <table className="w-full">
            <thead>
              <tr className="border-b border-slate-200 text-slate-500 text-sm">
                <th className="text-right py-3 font-medium">شماره</th>
                <th className="text-right py-3 font-medium">تاریخ</th>
                <th className="text-right py-3 font-medium">وضعیت</th>
                <th className="text-right py-3 font-medium">مبلغ</th>
              </tr>
            </thead>
            <tbody>
              {invoices.slice(0, 5).map((invoice) => (
                <tr key={invoice.id} className="border-b border-slate-100 hover:bg-slate-50">
                  <td className="py-3 font-mono text-sm">{invoice.number}</td>
                  <td className="py-3 text-sm">{formatDate(invoice.issueDate)}</td>
                  <td className="py-3">
                    <span className="px-2 py-1 rounded-full text-xs bg-green-100 text-green-700">
                      {invoice.status}
                    </span>
                  </td>
                  <td className="py-3 font-bold">{formatCurrency(invoice.total)}</td>
                </tr>
              ))}
            </tbody>
          </table>
        )}
      </div>
    </div>
  );
}