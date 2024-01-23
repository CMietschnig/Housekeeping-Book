import { mount } from '@vue/test-utils'
import { describe, it, expect } from 'vitest'
import i18nMock from '@/tests/i18n.Mock'
import FontAwesomeIcon from '@/tests/fontAwesomeIcon.Mock'
import EditableComment from '@/components/general/EditableComment.vue'

describe('EditableComment component', () => {
  const id: number = 2
  const comment: string = 'this is a comment.'

  it('renders correctly in "not-edit-mode" with comment', () => {
    // Arrange
    const wrapper = mount(EditableComment, {
      props: {
        id: id,
        comment: comment
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const noEditMode = wrapper.find('.no-edit-mode')
    const editMode = wrapper.find('.edit-mode')
    const noComment = wrapper.find('.no-comment')
    const commentVisible = wrapper.find('.comment')

    // Assert
    expect(noEditMode.exists()).toBeTruthy()
    expect(editMode.exists()).toBeFalsy()

    expect(commentVisible.exists()).toBeTruthy()
    expect(noComment.exists()).toBeFalsy()
  })
  it('renders correctly in "not-edit-mode" WITHOUT comment', () => {
    // Arrange
    const wrapper = mount(EditableComment, {
      props: {
        id: id,
        comment: undefined
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const noEditMode = wrapper.find('.no-edit-mode')
    const editMode = wrapper.find('.edit-mode')
    const noComment = wrapper.find('.no-comment')
    const commentVisible = wrapper.find('.comment')

    // Assert
    expect(noEditMode.exists()).toBeTruthy()
    expect(editMode.exists()).toBeFalsy()

    expect(noComment.exists()).toBeTruthy()
    expect(commentVisible.exists()).toBeFalsy()
  })
  it('edit comment button is visible in no-edit-mode', () => {
    // Arrange
    const wrapper = mount(EditableComment, {
      props: {
        id: id,
        comment: undefined
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const noEditMode = wrapper.find('.no-edit-mode')
    const editMode = wrapper.find('.edit-mode')
    const editCommentButton = wrapper.find('.edit-comment-btn')
    const saveCommentButton = wrapper.find('.save-comment-btn')

    // Assert
    expect(noEditMode.exists()).toBeTruthy()
    expect(editMode.exists()).toBeFalsy()

    expect(editCommentButton.exists()).toBeTruthy()
    expect(saveCommentButton.exists()).toBeFalsy()
  })
  it('only save comment button is visible in edit-mode -> click on edit button', async () => {
    // Arrange
    const wrapper = mount(EditableComment, {
      props: {
        id: id,
        comment: undefined
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const noEditMode = wrapper.find('.no-edit-mode')
    const editMode = wrapper.find('.edit-mode')
    const editCommentButton = wrapper.find('.edit-comment-btn')
    const saveCommentButton = wrapper.find('.save-comment-btn')

    // Assert
    expect(noEditMode.exists()).toBe(true)
    expect(editMode.exists()).toBe(false)

    expect(editCommentButton.exists()).toBe(true)
    expect(saveCommentButton.exists()).toBe(false)

    // Act -> click the button
    await editCommentButton.trigger('click')

    const noEditModeAfterClick = wrapper.find('.no-edit-mode')
    const editModeAfterClick = wrapper.find('.edit-mode')
    const editCommentButtonAfterClick = wrapper.find('.edit-comment-btn')
    const saveCommentButtonAfterClick = wrapper.find('.save-comment-btn')

    // Assert
    expect(noEditModeAfterClick.exists()).toBe(false)
    expect(editModeAfterClick.exists()).toBe(true)

    expect(editCommentButtonAfterClick.exists()).toBe(false)
    expect(saveCommentButtonAfterClick.exists()).toBe(true)
  })
  it('only edit comment button is visible in not-edit-mode -> click on save button', async () => {
    // Arrange
    const wrapper = mount(EditableComment, {
      props: {
        id: id,
        comment: undefined
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const noEditMode = wrapper.find('.no-edit-mode')
    const editMode = wrapper.find('.edit-mode')
    const editCommentButton = wrapper.find('.edit-comment-btn')
    const saveCommentButton = wrapper.find('.save-comment-btn')

    // Assert
    expect(noEditMode.exists()).toBe(true)
    expect(editMode.exists()).toBe(false)

    expect(editCommentButton.exists()).toBe(true)
    expect(saveCommentButton.exists()).toBe(false)

    // Act -> click the edit comment button
    await editCommentButton.trigger('click')

    const noEditModeAfterClick = wrapper.find('.no-edit-mode')
    const editModeAfterClick = wrapper.find('.edit-mode')
    const editCommentButtonAfterClick = wrapper.find('.edit-comment-btn')
    const saveCommentButtonAfterClick = wrapper.find('.save-comment-btn')

    // Assert
    expect(noEditModeAfterClick.exists()).toBe(false)
    expect(editModeAfterClick.exists()).toBe(true)

    expect(editCommentButtonAfterClick.exists()).toBe(false)
    expect(saveCommentButtonAfterClick.exists()).toBe(true)

    // Act -> click on save comment button
    await saveCommentButtonAfterClick.trigger('click')

    const noEditModeAfterClickSave = wrapper.find('.no-edit-mode')
    const editModeAfterClickSave = wrapper.find('.edit-mode')
    const editCommentButtonAfterClickSave = wrapper.find('.edit-comment-btn')
    const saveCommentButtonAfterClickSave = wrapper.find('.save-comment-btn')

    // Assert
    expect(noEditModeAfterClickSave.exists()).toBe(true)
    expect(editModeAfterClickSave.exists()).toBe(false)

    expect(editCommentButtonAfterClickSave.exists()).toBe(true)
    expect(saveCommentButtonAfterClickSave.exists()).toBe(false)
  })
  it('click on save button -> should emit saveComment with undefined', async () => {
    // Arrange
    const wrapper = mount(EditableComment, {
      props: {
        id: id,
        comment: undefined
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const editCommentButton = wrapper.find('.edit-comment-btn')
    await editCommentButton.trigger('click')
    const saveCommentButtonAfterClick = wrapper.find('.save-comment-btn')

    // Assert
    expect(saveCommentButtonAfterClick.exists()).toBe(true)

    // Act -> click on save comment button
    await saveCommentButtonAfterClick.trigger('click')

    // Assert
    expect(wrapper.emitted('saveComment')).toBeTruthy()
    expect(wrapper.emitted('saveComment')).toStrictEqual([[undefined]])
  })
  it('click on save button -> should emit saveComment with comment', async () => {
    // Arrange
    const wrapper = mount(EditableComment, {
      props: {
        id: id,
        comment: comment
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const editCommentButton = wrapper.find('.edit-comment-btn')
    await editCommentButton.trigger('click')
    const saveCommentButtonAfterClick = wrapper.find('.save-comment-btn')

    // Assert
    expect(saveCommentButtonAfterClick.exists()).toBe(true)

    // Act -> click on save comment button
    await saveCommentButtonAfterClick.trigger('click')

    // Assert
    expect(wrapper.emitted('saveComment')).toBeTruthy()
    expect(wrapper.emitted('saveComment')).toStrictEqual([[comment]])
  })
  it('click on save button -> should emit saveComment with empty string', async () => {
    // Arrange
    const wrapper = mount(EditableComment, {
      props: {
        id: id,
        comment: ''
      },
      global: {
        plugins: [i18nMock],
        components: { FontAwesomeIcon }
      }
    })

    // Act
    const editCommentButton = wrapper.find('.edit-comment-btn')
    await editCommentButton.trigger('click')
    const saveCommentButtonAfterClick = wrapper.find('.save-comment-btn')

    // Assert
    expect(saveCommentButtonAfterClick.exists()).toBe(true)

    // Act -> click on save comment button
    await saveCommentButtonAfterClick.trigger('click')

    // Assert
    expect(wrapper.emitted('saveComment')).toBeTruthy()
    expect(wrapper.emitted('saveComment')).toStrictEqual([['']])
  })
})
