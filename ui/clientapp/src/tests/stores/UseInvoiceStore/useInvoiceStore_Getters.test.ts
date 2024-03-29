import { useInvoiceStore } from '@/stores/useInvoiceStore'
import { createPinia, setActivePinia } from 'pinia'
import { describe, it, expect } from 'vitest'

describe('useInvoiceStore => getters, default state', () => {
  it('useInvoiceStore => should return default states', () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    // Assert
    expect(sut.invoices).toEqual([])
    expect(sut.monthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.comment).toEqual('')
    expect(sut.monthlySum).toEqual(0)
    expect(sut.annualMonthlyAverage).toEqual(0)
  })

  it('useInvoiceStore => should return correct values for getters', () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useInvoiceStore()

    // Assert
    expect(sut.getInvoices).toEqual([])
    expect(sut.getMonthTotals).toEqual([10, 20, 30, 40, 50])
    expect(sut.getComment).toEqual('')
    expect(sut.getMonthlySum).toEqual(0)
    expect(sut.getAnnualMonthlyAverage).toEqual(0)
  })
})
