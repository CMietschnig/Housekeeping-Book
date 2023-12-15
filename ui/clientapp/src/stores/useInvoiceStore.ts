import { defineStore } from 'pinia'
import type { IInvoice } from '@/interfaces/IInvoice'

export interface InvoiceStoreState {
  invoices: Array<IInvoice>
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
    },
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
    },
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
    },
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
    },
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
    },
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
    },
  ]
}

export const useInvoiceStore = defineStore({
  id: 'invoice-store',
  state: (): InvoiceStoreState => ({
    ...DefaultInvoiceState
  }),
  getters: {
    getInvoices: (state): Array<IInvoice> => state.invoices
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
