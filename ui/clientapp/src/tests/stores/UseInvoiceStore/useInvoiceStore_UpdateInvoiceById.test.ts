import InvoicesApiService from '@/services/api/InvoicesApiService'
import { useInvoiceStore } from '@/stores/useInvoiceStore'
import { createPinia, setActivePinia } from 'pinia'
import { describe, it, expect, vi } from 'vitest'

describe('useInvoiceStore => updateInvoiceById', () => {
  const id: number = 3
  const invoiceTotal: number = 23.56

  it('updateInvoiceById => should catch log because returns 200 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'updateInvoiceById')
      .mockResolvedValue(200)
    const consoleMock = vi.spyOn(console, 'log').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.updateInvoiceById(id, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith('updateInvoiceById was successful!')

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateInvoiceById => should catch log because returns 201 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'updateInvoiceById')
      .mockResolvedValue(201)
    const consoleMock = vi.spyOn(console, 'log').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.updateInvoiceById(id, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith('updateInvoiceById was successful!')

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateInvoiceById => should catch error because returns undefined', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'updateInvoiceById')
      .mockResolvedValue(undefined)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.updateInvoiceById(id, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update invoice by id 3 with total 23.56. Status code: undefined'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateInvoiceById => should catch error because returns error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'updateInvoiceById')
      .mockRejectedValue(new Error('error'))
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.updateInvoiceById(id, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(spyInvoiceApiService).rejects.toThrow('error')
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update invoice by id 3 with total 23.56. Error: error'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateInvoiceById => should catch error because returns 300 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'updateInvoiceById')
      .mockResolvedValue(300)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.updateInvoiceById(id, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update invoice by id 3 with total 23.56. Status code: 300'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })
})
