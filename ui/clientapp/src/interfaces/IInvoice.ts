import type { IStore } from './IStore'

export interface IInvoice {
  InvoiceId: number
  Total: number
  CreateTimestamp: Date | string
  UpdateTimestamp: Date | string
  MonthlyInvoiceSummaryId: number
  Store: IStore | null
}
