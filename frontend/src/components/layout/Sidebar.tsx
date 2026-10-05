import { Link, useLocation } from 'react-router-dom';
import { LayoutDashboard, Wallet, FileText, BookOpen } from 'lucide-react';
import { cn } from '@/lib/utils';

const menuItems = [
  { path: '/', label: 'داشبورد', icon: LayoutDashboard },
  { path: '/accounts', label: 'حساب‌ها', icon: Wallet },
  { path: '/invoices', label: 'فاکتورها', icon: FileText },
  { path: '/journal-entries', label: 'اسناد حسابداری', icon: BookOpen },
];

export default function Sidebar() {
  const location = useLocation();

  return (
    <aside className="w-64 bg-slate-900 text-white min-h-screen p-6 flex flex-col">
      <div className="mb-8">
        <h1 className="text-2xl font-bold">🧮 سامانه حسابداری</h1>
        <p className="text-sm text-slate-400 mt-1">Accounting System</p>
      </div>

      <nav className="space-y-2 flex-1">
        {menuItems.map((item) => {
          const Icon = item.icon;
          const isActive = location.pathname === item.path;

          return (
            <Link
              key={item.path}
              to={item.path}
              className={cn(
                'flex items-center gap-3 px-4 py-3 rounded-lg transition-colors',
                isActive
                  ? 'bg-blue-600 text-white'
                  : 'text-slate-300 hover:bg-slate-800'
              )}
            >
              <Icon size={20} />
              <span>{item.label}</span>
            </Link>
          );
        })}
      </nav>

      <div className="mt-8 pt-8 border-t border-slate-700">
        <p className="text-xs text-slate-500 leading-relaxed">
          ساخته شده با ❤️ توسط
          <br />
          <span className="text-slate-400">امید سکاکی</span>
        </p>
      </div>
    </aside>
  );
}