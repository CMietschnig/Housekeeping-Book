import { defineStore } from 'pinia'
import type { IInvoice } from '@/interfaces/IInvoice'
import InvoicesApiService from '@/services/api/InvoicesApiService'

export interface InvoiceStoreState {
  invoices: Array<IInvoice>
  monthTotals: Array<number>
  comment: string
}

const DefaultInvoiceState: InvoiceStoreState = {
  invoices: [],
  monthTotals: [10, 20, 30, 40, 50, 60, 70, 80, 90, 100, 110, 120],
  comment: ''
}

const invoicesApiService = new InvoicesApiService()

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
        const invoices = await invoicesApiService.getInvoicesPerMonthAndYear(month, year)

        if (invoices) {
          this.$patch((state) => {
            state.invoices = invoices
          })
        } else {
          console.error(
            'Could not get invoices per month and year ' +
              month +
              ' ' +
              year +
              '. The response is undefined.'
          )
        }
      } catch (e) {
        console.error('Could not get invoices per month and year ' + month + ' ' + year + '. ' + e)
      }
    },
    async getCommentPerMonthAndYear(month: number, year: string) {
      try {
        const comment = await invoicesApiService.getCommentPerMonthAndYear(month, year)

        if (comment) {
          this.$patch((state) => {
            state.comment = comment
          })
        } else {
          console.error(
            'Could not get comment per month and year ' +
              month +
              ' ' +
              year +
              '. The response is undefined.'
          )
        }
      } catch (e) {
        console.error('Could not get comment per month and year ' + month + ' ' + year + '. ' + e)
      }
    },
    async addInvoiceToMonthAndYear(month: number, year: number, invoiceTotal: number) {
      try {
        const response = await invoicesApiService.addInvoiceToMonthAndYear(
          month,
          year,
          invoiceTotal
        )

        if (response === 200) {
          console.log('addInvoiceToMonthAndYear was successful!')
        }
      } catch (e) {
        console.error('Could not add invoice to month and year ' + e)
      }
    },
    async updateInvoiceById(id: number, invoiceTotal: number) {
      try {
        const response = await invoicesApiService.updateInvoiceById(id, invoiceTotal)

        if (response === 200) {
          console.log('updateInvoiceById was successful!')
        }
      } catch (e) {
        console.error('Could not update invoice by id ' + id + '. ' + e)
      }
    },
    async updateCommentByMonthAndYear(month: number, year: number, comment: string) {
      try {
        const newComment = await invoicesApiService.updateCommentByMonthAndYear(
          month,
          year,
          comment
        )

        if (newComment) {
          this.$patch((state) => {
            state.comment = newComment
          })
        } else {
          console.error(
            'Could not update comment by month and year ' +
              month +
              ' ' +
              year +
              ' ' +
              comment +
              '. The response is undefined.'
          )
        }
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
        const response = await invoicesApiService.deleteInvoiceById(id)

        if (response === 200) {
          console.log('deleteInvoiceById was successful!')
        }
      } catch (e) {
        console.error('Could not delete number with id ' + id + '. ' + e)
      }
    }
  }
})
