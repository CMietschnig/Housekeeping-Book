import { defineStore } from 'pinia'
import type { IInvoice } from '@/interfaces/IInvoice'
import axios from 'axios'

export interface InvoiceStoreState {
  invoices: Array<IInvoice>
  monthTotals: Array<number>
  comment: string
}

const DefaultInvoiceState: InvoiceStoreState = {
  invoices: [],
  monthTotals: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120],
  comment: ""
}

export const useInvoiceStore = defineStore({
  id: 'invoice-store',
  state: (): InvoiceStoreState => ({
    ...DefaultInvoiceState
  }),
  getters: {
    getInvoices: (state): Array<IInvoice> => state.invoices,
    getMonthTotals: (state): Array<number> => state.monthTotals,
    getComment: (state): string => state.comment
  },
  actions: {
    async getInvoicesPerMonthAndYear(month: number, year: string) {
      try {
        const response = await axios.post(
          'http://localhost:65513/api/invoices/getInvoicesPerMonthAndYear',
          { month, year },
          {
            headers: {
              'content-type': 'application/json'
            }
          }
        )
        this.$patch((state) => {
          state.invoices = response.data
        })
      } catch (e) {
        console.error('Could not get invoices per month and year ' + month + ' ' + year + '. ' + e)
      }
    },
    async getCommentPerMonthAndYear(month: number, year: string) {
      try {
        const response = await axios.post(
          'http://localhost:65513/api/invoices/getCommentPerMonthAndYear',
          { month, year },
          {
            headers: {
              'content-type': 'application/json'
            }
          }
        )
        this.$patch((state) => {
          state.comment = response.data
        })
      } catch (e) {
        console.error('Could not get comment per month and year ' + month + ' ' + year + '. ' + e)
      }
    },
    async addInvoiceToMonthAndYear(month: number, year: number, invoiceTotal: number) {
      try {
        const response = await axios.post(
          'http://localhost:65513/api/invoices/addInvoiceToMonthAndYear',
          { month, year, invoiceTotal },
          {
            headers: {
              'content-type': 'application/json'
            }
          }
        )
        if (response.status === 200) {
          console.log("addInvoiceToMonthAndYear was successful!");
        }
      } catch (e) {
        console.error('Could not add invoice to month and year ' + e)
      }
    },
    async updateInvoiceById(id: number, invoiceTotal: number) {
      try {
        const response = await axios.put(
          'http://localhost:65513/api/invoices/updateInvoiceById',
          {
            id,
            invoiceTotal
          },
          {
            headers: {
              'content-type': 'application/json'
            }
          }
        )
        if(response.status === 200) {
          console.log('updateInvoiceById was successful!')
        }
      } catch (e) {
        console.error('Could not update invoice by id ' + id + '. ' + e)
      }
    },
    async updateCommentByMonthAndYear(month: number, year: number, comment: string) {
      try {
        const response = await axios.put(
          'http://localhost:65513/api/invoices/updateCommentByMonthAndYear',
          {
            month,
            year,
            comment
          },
          {
            headers: {
              'content-type': 'application/json'
            }
          }
        )
        this.$patch((state) => {
          state.comment = response.data
        })
      } catch (e) {
        console.error(
          'Could not update comment by month and year ' +
            month +
            ' ' +
            year +
            ' ' +
            comment +
            ' ' +
            e
        )
      }
    },
    async deleteInvoiceById(id: number) {
      try {
        const response = await axios.post(
          'http://localhost:65513/api/invoices/deleteInvoiceById',
          { id },
          {
            headers: {
              'content-type': 'application/json'
            }
          }
        )
        if(response.status === 200) {
          console.log('deleteInvoiceById was successful!')
        }
      } catch (e) {
        console.error('Could not delete number with id ' + id + '. ' + e)
      }
    }
  }
})
