import InvoicesApiService from '@/services/api/InvoicesApiService'
import { useInvoiceStore } from '@/stores/useInvoiceStore'
import { createPinia, setActivePinia } from 'pinia'
import { describe, it, expect, vi } from 'vitest'

describe('useInvoiceStore => updateCommentByMonthAndYear', () => {
  const month = 2
  const year = '2024'
  const comment: string = 'this is my new comment'

  it('updateCommentByMonthAndYear => should return comment', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'updateCommentByMonthAndYear')
      .mockResolvedValue(comment)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.updateCommentByMonthAndYear(month, year, comment)

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

  it('updateCommentByMonthAndYear => should return undefined and catch error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'updateCommentByMonthAndYear')
      .mockResolvedValue(undefined)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.updateCommentByMonthAndYear(month, year, comment)

    // Assert
    expect(spyInvoiceApiService).toHaveBeenCalledTimes(1)
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update comment by month and year 2 2024 this is my new comment. The response is undefined.'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateCommentByMonthAndYear => should catch error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    const spyInvoiceApiService = vi
      .spyOn(InvoicesApiService, 'updateCommentByMonthAndYear')
      .mockRejectedValue(new Error('error'))
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)

    // Act
    await sut.updateCommentByMonthAndYear(month, year, comment)

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
      'Could not update comment by month and year 2 2024 this is my new comment. Error: error'
    )

    // Clean up after the test
    spyInvoiceApiService.mockRestore()
    consoleMock.mockReset()
  })
})
