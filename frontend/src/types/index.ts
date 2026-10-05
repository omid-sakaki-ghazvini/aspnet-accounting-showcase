// ============================================================
// Account
// ============================================================
export type AccountType = 'Asset' | 'Liability' | 'Equity' | 'Revenue' | 'Expense';

export interface Account {
  id: number;
  code: string;
  name: string;
  type: AccountType;
  isActive: boolean;
  description?: string;
}

export interface CreateAccountDto {
  code: string;
  name: string;
  type: number;
  parentAccountId?: number | null;
  description?: string;
}

// ============================================================
// Invoice
// ============================================================
export type InvoiceStatus = 'Draft' | 'Sent' | 'Paid' | 'Overdue' | 'Cancelled';

export interface InvoiceItem {
  id: number;
  description: string;
  quantity: number;
  unitPrice: number;
  lineTotal: number;
}

export interface Invoice {
  id: number;
  number: string;
  issueDate: string;
  dueDate?: string;
  status: InvoiceStatus;
  subTotal: number;
  taxAmount: number;
  total: number;
  notes?: string;
  items: InvoiceItem[];
}

// ============================================================
// Journal Entry
// ============================================================
export type JournalEntryStatus = 'Draft' | 'Posted' | 'Reversed';

export interface JournalLine {
  id: number;
  accountId: number;
  accountName: string;
  debit: number;
  credit: number;
  description?: string;
}

export interface JournalEntry {
  id: number;
  number: string;
  date: string;
  description: string;
  reference?: string;
  status: JournalEntryStatus;
  totalDebit: number;
  totalCredit: number;
  isBalanced: boolean;
  lines: JournalLine[];
}