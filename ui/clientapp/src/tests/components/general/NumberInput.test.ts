import NumberInput from '@/components/general/NumberInput.vue'
import FontAwesomeIcon from '@/tests/fontAwesomeIcon.Mock'
import { mount } from '@vue/test-utils'
import { describe, it, expect } from 'vitest'

describe('NumberInput.vue', () => {
  it('onlyAddable true -> should have only add input field and add button', () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: true,
        placeholder: 'placeholder',
        number: 2,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const number = wrapper.find('.number')
    const editButton = wrapper.find('.edit-btn')
    const deleteButton = wrapper.find('.delete-btn')
    const updateInputField = wrapper.find('.update-input-field')
    const updateButton = wrapper.find('.update-btn')
    const resetButton = wrapper.find('.reset-btn')
    const addInputField = wrapper.find('.add-input-field')
    const addButton = wrapper.find('.add-btn')

    // Assert
    expect(number.exists()).toBe(false)
    expect(editButton.exists()).toBe(false)
    expect(deleteButton.exists()).toBe(false)
    expect(updateInputField.exists()).toBe(false)
    expect(updateButton.exists()).toBe(false)
    expect(resetButton.exists()).toBe(false)

    expect(addInputField.exists()).toBe(true)
    expect(addButton.exists()).toBe(true)
  })
  it('onlyAddable false,  isEditMode true -> should have only update input field, update button and reset button', async () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: false,
        placeholder: 'placeholder',
        number: 2,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const editButton = wrapper.find('.edit-btn')
    expect(editButton.exists()).toBe(true)
    await editButton.trigger('click')

    const number = wrapper.find('.number')
    const editButtonAfterClick = wrapper.find('.edit-btn')
    const deleteButton = wrapper.find('.delete-btn')
    const updateInputField = wrapper.find('.update-input-field')
    const updateButton = wrapper.find('.update-btn')
    const resetButton = wrapper.find('.reset-btn')
    const addInputField = wrapper.find('.add-input-field')
    const addButton = wrapper.find('.add-btn')

    // Assert
    expect(number.exists()).toBe(false)
    expect(editButtonAfterClick.exists()).toBe(false)
    expect(deleteButton.exists()).toBe(false)

    expect(updateInputField.exists()).toBe(true)
    expect(updateButton.exists()).toBe(true)
    expect(resetButton.exists()).toBe(true)

    expect(addInputField.exists()).toBe(false)
    expect(addButton.exists()).toBe(false)
  })
  it('onlyAddable false,  isEditMode false, canDelete true -> should have only invoice number, edit button and delete button', () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: false,
        placeholder: 'placeholder',
        number: 2,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const number = wrapper.find('.number')
    const editButton = wrapper.find('.edit-btn')
    const deleteButton = wrapper.find('.delete-btn')
    const updateInputField = wrapper.find('.update-input-field')
    const updateButton = wrapper.find('.update-btn')
    const resetButton = wrapper.find('.reset-btn')
    const addInputField = wrapper.find('.add-input-field')
    const addButton = wrapper.find('.add-btn')

    // Assert
    expect(number.exists()).toBe(true)
    expect(editButton.exists()).toBe(true)
    expect(deleteButton.exists()).toBe(true)

    expect(updateInputField.exists()).toBe(false)
    expect(updateButton.exists()).toBe(false)
    expect(resetButton.exists()).toBe(false)
    expect(addInputField.exists()).toBe(false)
    expect(addButton.exists()).toBe(false)
  })
  it('onlyAddable false,  isEditMode false, canDelete false -> should have only invoice number and edit button', () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: false,
        placeholder: 'placeholder',
        number: 2,
        id: 'test',
        canDelete: false
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const number = wrapper.find('.number')
    const editButton = wrapper.find('.edit-btn')
    const deleteButton = wrapper.find('.delete-btn')
    const updateInputField = wrapper.find('.update-input-field')
    const updateButton = wrapper.find('.update-btn')
    const resetButton = wrapper.find('.reset-btn')
    const addInputField = wrapper.find('.add-input-field')
    const addButton = wrapper.find('.add-btn')

    // Assert
    expect(number.exists()).toBe(true)
    expect(editButton.exists()).toBe(true)

    expect(deleteButton.exists()).toBe(false)
    expect(updateInputField.exists()).toBe(false)
    expect(updateButton.exists()).toBe(false)
    expect(resetButton.exists()).toBe(false)
    expect(addInputField.exists()).toBe(false)
    expect(addButton.exists()).toBe(false)
  })
  it('should emit delete number', async () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: false,
        placeholder: 'placeholder',
        number: 2,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const deleteButton = wrapper.find('.delete-btn')
    await deleteButton.trigger('click')

    // Assert
    expect(wrapper.emitted('deleteNumber')).toBeTruthy()
    expect(wrapper.emitted('deleteNumber')).toStrictEqual([['test']])
  })
  it('should emit update number 2', async () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: false,
        placeholder: 'placeholder',
        number: 2,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const editButton = wrapper.find('.edit-btn')
    await editButton.trigger('click')
    const updateButton = wrapper.find('.update-btn')
    await updateButton.trigger('click')

    // Assert
    expect(wrapper.emitted('updateNumber')).toBeTruthy()
    expect(wrapper.emitted('updateNumber')).toStrictEqual([[{ id: 'test', invoiceTotal: 2 }]])
  })
  it('should emit add number 2', async () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: true,
        placeholder: 'placeholder',
        number: 2,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const addButton = wrapper.find('.add-btn')
    await addButton.trigger('click')

    // Assert
    expect(wrapper.emitted('addNumber')).toBeTruthy()
    expect(wrapper.emitted('addNumber')).toStrictEqual([[2]])
  })
  it('should NOT emit add number when number is null', async () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: true,
        placeholder: 'placeholder',
        number: null,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const addButton = wrapper.find('.add-btn')
    await addButton.trigger('click')

    // Assert
    expect(wrapper.emitted('addNumber')).toBeFalsy()
  })
  it('should NOT emit update number when number is null', async () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: false,
        placeholder: 'placeholder',
        number: null,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const editButton = wrapper.find('.edit-btn')
    await editButton.trigger('click')
    const updateButton = wrapper.find('.update-btn')
    await updateButton.trigger('click')

    // Assert
    expect(wrapper.emitted('updateNumber')).toBeFalsy()
  })
  it('should NOT emit add number when number is 0', async () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: true,
        placeholder: 'placeholder',
        number: 0,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const addButton = wrapper.find('.add-btn')
    await addButton.trigger('click')

    // Assert
    expect(wrapper.emitted('addNumber')).toBeFalsy()
  })
  it('should NOT emit update number when number is 0', async () => {
    // Arrange
    const wrapper = mount(NumberInput, {
      props: {
        onlyAddable: false,
        placeholder: 'placeholder',
        number: 0,
        id: 'test'
      },
      global: {
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const editButton = wrapper.find('.edit-btn')
    await editButton.trigger('click')
    const updateButton = wrapper.find('.update-btn')
    await updateButton.trigger('click')

    // Assert
    expect(wrapper.emitted('updateNumber')).toBeFalsy()
  })
})
