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
    }
  }

  async addInvoiceToMonthAndYear(
    month: number,
    year: number,
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
      console.error('Could not add invoice to month and year ' + e)
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
      console.error('Could not update invoice by id ' + id + '. ' + e)
    }
  }

  async updateCommentByMonthAndYear(
    month: number,
    year: number,
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
    }
  }
}

export default InvoicesApiService
