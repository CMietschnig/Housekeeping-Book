import type { IInvoice } from '@/interfaces/IInvoice'
import InvoicesApiService from '@/services/api/InvoicesApiService'
import { useInvoiceStore } from '@/stores/useInvoiceStore'
import { createPinia, setActivePinia } from 'pinia'
import { describe, it, expect, vi } from 'vitest'

describe('useInvoiceStore => getInvoicesPerMonthAndYear', () => {
  const month = 2
  const year = '2024'
  const invoices: IInvoice[] = [
    {
      InvoiceId: 1,
      MonthlyInvoiceSummaryId: 2,
      Store: null,
      CreateTimestamp: '2024-02-01T23:00:00.000Z',
      UpdateTimestamp: '2024-03-01T23:00:00.000Z',
      Total: 34.56
    },
    {
      InvoiceId: 2,
      MonthlyInvoiceSummaryId: 3,
      Store: null,
      CreateTimestamp: '2024-04-01T23:00:00.000Z',
      UpdateTimestamp: '2024-05-01T23:00:00.000Z',
      Total: 67.98
    }
  ]

  it('getInvoicesPerMonthAndYear => should return invoices', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'getInvoicesPerMonthAndYear')
      .mockResolvedValue(invoices)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)

    // Act
    await sut.getInvoicesPerMonthAndYear(month, year)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual(invoices)
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(102.54)

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
  })

  it('getInvoicesPerMonthAndYear => should return undefined and catch error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'getInvoicesPerMonthAndYear')
      .mockResolvedValue(undefined)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)

    // Act
    await sut.getInvoicesPerMonthAndYear(month, year)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not get invoices per month and year 2 2024. The response is undefined.'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('getInvoicesPerMonthAndYear => should catch error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'getInvoicesPerMonthAndYear')
      .mockRejectedValue(new Error('error'))
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)

    // Act
    await sut.getInvoicesPerMonthAndYear(month, year)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(spyInvoiceApiService).rejects.toThrow('error')
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not get invoices per month and year 2 2024. Error: error'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })
})
