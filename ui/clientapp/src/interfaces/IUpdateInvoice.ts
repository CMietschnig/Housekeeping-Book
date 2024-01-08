import type { IStore } from "./IStore"

export interface IUpdateInvoice {
  InvoiceId: number | string
  Total: number
  CreateTimestamp: Date
  UpdateTimestamp: Date
  MonthlyInvoiceSummaryId: number | null
  Store: IStore | null
}