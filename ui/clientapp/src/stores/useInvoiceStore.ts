import { defineStore } from 'pinia'
import type { IInvoice } from '@/interfaces/IInvoice'

export interface InvoiceStoreState {
  invoices: Array<IInvoice>
  monthTotals: Array<number>
}

const DefaultInvoiceState: InvoiceStoreState = {
  invoices: [
    {
      id: 1,
      sum: 2334.434343
    },
    {
      id: 2,
      sum: 34
    },
    {
      id: 3,
      sum: 45
    }
  ],
  monthTotals: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120]
}

export const useInvoiceStore = defineStore({
  id: 'invoice-store',
  state: (): InvoiceStoreState => ({
    ...DefaultInvoiceState
  }),
  getters: {
    getInvoices: (state): Array<IInvoice> => state.invoices,
    getMonthTotals: (state): Array<number> => state.monthTotals
  },
  actions: {
    async deleteNumber(id: number) {
      try {
        console.log('delete number in store', id)
      } catch (e) {
        console.error('Could not delete number with id ' + id + '. ' + e)
      }
    },
    async saveNumber(id: number, number: number) {
      try {
        console.log('save number in store', id, number)
      } catch (e) {
        console.error('Could not save number ' + number + ' with id ' + id + '. ' + e)
      }
    },
    async addNumber(number: number) {
      try {
        console.log('add number in store', number)
      } catch (e) {
        console.error('Could not add number ' + number + '. ' + e)
      }
    }
  }
})
