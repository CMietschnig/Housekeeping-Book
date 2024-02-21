import InvoicesApiService from '@/services/api/InvoicesApiService'
import { useInvoiceStore } from '@/stores/useInvoiceStore'
import { createPinia, setActivePinia } from 'pinia'
import { describe, it, expect, vi } from 'vitest'

describe('useInvoiceStore => getCommentPerMonthAndYear', () => {
  const month = 2
  const year = '2024'
  const comment: string = 'this is my comment'

  it('getCommentPerMonthAndYear => should return comment', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'getCommentPerMonthAndYear')
      .mockResolvedValue(comment)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.getCommentPerMonthAndYear(month, year)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual(comment)
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
  })

  it('getCommentPerMonthAndYear => should return undefined and catch error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'getCommentPerMonthAndYear')
      .mockResolvedValue(undefined)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.getCommentPerMonthAndYear(month, year)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not get comment per month and year 2 2024. The response is undefined.'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('getCommentPerMonthAndYear => should catch error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'getCommentPerMonthAndYear')
      .mockRejectedValue(new Error('error'))
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.getCommentPerMonthAndYear(month, year)

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
      'Could not get comment per month and year 2 2024. Error: error'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })
})
