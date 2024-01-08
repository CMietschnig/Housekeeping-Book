import type { IStore } from "./IStore"

export interface IInvoice {
  InvoiceId: number
  Total: number
  CreateTimestamp: Date
  UpdateTimestamp: Date
  MonthlyInvoiceSummaryId: number
  Store: IStore | null
}