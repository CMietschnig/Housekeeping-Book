import type { IInvoice } from '@/interfaces/IInvoice'
import axios from 'axios'
import { EnviromentVariables } from '@/enums/EnviromentVariables'

class InvoicesApiService {
  private static readonly baseUrl: string = EnviromentVariables.API_BASE_URL

  async getInvoicesPerMonthAndYear(month: number, year: string): Promise<IInvoice[] | undefined> {
    try {
      const response = await axios.post(
        `${InvoicesApiService.baseUrl}/api/invoices/getInvoicesPerMonthAndYear`,
        { month, year },
        {
          headers: {
            'content-type': 'application/json'
          }
        }
      )
      return response.data
    } catch (e) {
      console.error('Could not get invoices per month and year ' + month + ' ' + year + '. ' + e)
      return undefined // Return undefined in case of an error
    }
  }

  async getCommentPerMonthAndYear(month: number, year: string): Promise<string | undefined> {
    try {
      const response = await axios.post(
        `${InvoicesApiService.baseUrl}/api/invoices/getCommentPerMonthAndYear`,
        { month, year },
        {
          headers: {
            'content-type': 'application/json'
          }
        }
      )
      return response.data
    } catch (e) {
      console.error('Could not get comment per month and year ' + month + ' ' + year + '. ' + e)
      return undefined // Return undefined in case of an error
    }
  }

  async addInvoiceToMonthAndYear(
    month: number,
    year: string,
    invoiceTotal: number
  ): Promise<number | undefined> {
    try {
      const response = await axios.post(
        `${InvoicesApiService.baseUrl}/api/invoices/addInvoiceToMonthAndYear`,
        { month, year, invoiceTotal },
        {
          headers: {
            'content-type': 'application/json'
          }
        }
      )
      return response.status
    } catch (e) {
      console.error(
        'Could not add invoice to month and year ' +
          month +
          ' ' +
          year +
          ' with total ' +
          invoiceTotal +
          '. ' +
          e
      )
      return undefined // Return undefined in case of an error
    }
  }

  async updateInvoiceById(id: number, invoiceTotal: number): Promise<number | undefined> {
    try {
      const response = await axios.put(
        `${InvoicesApiService.baseUrl}/api/invoices/updateInvoiceById`,
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
      return response.status
    } catch (e) {
      console.error(
        'Could not update invoice by id ' + id + ' with total ' + invoiceTotal + '. ' + e
      )
      return undefined // Return undefined in case of an error
    }
  }

  async updateCommentByMonthAndYear(
    month: number,
    year: string,
    comment: string
  ): Promise<string | undefined> {
    try {
      const response = await axios.put(
        `${InvoicesApiService.baseUrl}/api/invoices/updateCommentByMonthAndYear`,
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
      return response.data
    } catch (e) {
      console.error(
        'Could not update comment by month and year ' + month + ' ' + year + ' ' + comment + ' ' + e
      )
      return undefined // Return undefined in case of an error
    }
  }

  async deleteInvoiceById(id: number): Promise<number | undefined> {
    try {
      const response = await axios.post(
        `${InvoicesApiService.baseUrl}/api/invoices/deleteInvoiceById`,
        { id },
        {
          headers: {
            'content-type': 'application/json'
          }
        }
      )
      return response.status
    } catch (e) {
      console.error('Could not delete number with id ' + id + '. ' + e)
      return undefined // Return undefined in case of an error
    }
  }
}

export default new InvoicesApiService()
