import InvoicesApiService from '@/services/api/InvoicesApiService'
import { useInvoiceStore } from '@/stores/useInvoiceStore'
import { createPinia, setActivePinia } from 'pinia'
import { describe, it, expect, vi } from 'vitest'

describe('useInvoiceStore => addInvoiceToMonthAndYear', () => {
  const month = 2
  const year = '2024'
  const invoiceTotal: number = 23.56

  it('addInvoiceToMonthAndYear => should catch log because returns 200 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'addInvoiceToMonthAndYear')
      .mockResolvedValue(200)
    const consoleMock = vi.spyOn(console, 'log').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')

    // Act
    await sut.addInvoiceToMonthAndYear(month, year, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith('addInvoiceToMonthAndYear was successful!')

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('addInvoiceToMonthAndYear => should catch log because returns 201 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'addInvoiceToMonthAndYear')
      .mockResolvedValue(201)
    const consoleMock = vi.spyOn(console, 'log').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')

    // Act
    await sut.addInvoiceToMonthAndYear(month, year, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith('addInvoiceToMonthAndYear was successful!')

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('addInvoiceToMonthAndYear => should catch error because returns undefined', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'addInvoiceToMonthAndYear')
      .mockResolvedValue(undefined)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')

    // Act
    await sut.addInvoiceToMonthAndYear(month, year, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not add invoice to month and year2 2024 with total 23.56. Status code: undefined'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('addInvoiceToMonthAndYear => should catch error because returns error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'addInvoiceToMonthAndYear')
      .mockRejectedValue(new Error('error'))
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')

    // Act
    await sut.addInvoiceToMonthAndYear(month, year, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(spyInvoiceApiService).rejects.toThrow('error')
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not add invoice to month and year 2 2024 with total 23.56. Error: error'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('addInvoiceToMonthAndYear => should catch error because returns 300 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'addInvoiceToMonthAndYear')
      .mockResolvedValue(300)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')

    // Act
    await sut.addInvoiceToMonthAndYear(month, year, invoiceTotal)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not add invoice to month and year2 2024 with total 23.56. Status code: 300'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })
})
