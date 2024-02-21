import InvoicesPerMonthList from '@/components/editMonth/InvoicesPerMonthList.vue'
import { mount } from '@vue/test-utils'
import { describe, it, expect } from 'vitest'
import i18nMock from '@/tests/i18n.Mock'
import type { IInvoice } from '@/interfaces/IInvoice'
import FontAwesomeIcon from '@/tests/fontAwesomeIcon.Mock'

describe('InvoicesPerMonthList component', () => {
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

  it('renders correctly with no invoices', () => {
    // Arrange
    const wrapper = mount(InvoicesPerMonthList, {
      props: {
        invoices: []
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const hint = wrapper.find('.no-invoices')
    const invoicesList = wrapper.find('.invoices-list')

    // Assert
    expect(hint.exists()).toBeTruthy()
    expect(invoicesList.exists()).toBeFalsy()
  })

  it('renders correctly with invoices', () => {
    // Arrange
    const wrapper = mount(InvoicesPerMonthList, {
      props: {
        invoices: invoices
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const hint = wrapper.find('.no-invoices')
    const invoicesList = wrapper.find('.invoices-list')

    // Assert
    expect(hint.exists()).toBeFalsy()
    expect(invoicesList.exists()).toBeTruthy()
  })

  it('emits addNumber event when a new invoice is added', async () => {
    // Arrange
    const invoiceTotal: number = 23.34
    const wrapper = mount(InvoicesPerMonthList, {
      props: {
        invoices: []
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })
    const addNumberInputComponent = await wrapper.findComponent({ ref: 'addNewInvoice' })

    // Act
    await addNumberInputComponent.vm.$emit('addNumber', invoiceTotal)

    // Assert
    expect(addNumberInputComponent.exists()).toBeTruthy()
    expect(addNumberInputComponent.emitted('addNumber')).toBeTruthy()
    expect(wrapper.emitted('addNumber')).toBeTruthy()
    expect(wrapper.emitted('addNumber')).toStrictEqual([[invoiceTotal]])
  })

  it('emits updateNumber event when a invoice is updated', async () => {
    // Arrange
    const wrapper = mount(InvoicesPerMonthList, {
      props: {
        invoices: invoices
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })
    const updateNumberInputComponent = await wrapper.findComponent({ ref: 'invoice' })
    const updateInvoice = {
      id: 1,
      invoiceTotal: 34.45
    }

    // Act
    await updateNumberInputComponent.vm.$emit('updateNumber', updateInvoice)

    // Assert
    expect(updateNumberInputComponent.exists()).toBeTruthy()
    expect(updateNumberInputComponent.emitted('updateNumber')).toBeTruthy()
    expect(wrapper.emitted('updateNumber')).toBeTruthy()
    expect(wrapper.emitted('updateNumber')).toStrictEqual([[updateInvoice]])
  })

  it('emits deleteNumber event when a invoice is deleted', async () => {
    // Arrange
    const wrapper = mount(InvoicesPerMonthList, {
      props: {
        invoices: invoices
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })
    const deleteNumberInputComponent = await wrapper.findComponent({ ref: 'invoice' })
    const id: number = 2

    // Act
    await deleteNumberInputComponent.vm.$emit('deleteNumber', id)

    // Assert
    expect(deleteNumberInputComponent.exists()).toBeTruthy()
    expect(deleteNumberInputComponent.emitted('deleteNumber')).toBeTruthy()
    expect(wrapper.emitted('deleteNumber')).toBeTruthy()
    expect(wrapper.emitted('deleteNumber')).toStrictEqual([[id]])
  })
})
