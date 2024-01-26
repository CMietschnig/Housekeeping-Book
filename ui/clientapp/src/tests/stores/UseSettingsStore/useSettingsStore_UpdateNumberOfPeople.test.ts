import SettingsApiService from '@/services/api/SettingsApiService'
import { useSettingsStore } from '@/stores/useSettingsStore'
import { createPinia, setActivePinia } from 'pinia'
import { describe, it, expect, vi } from 'vitest'

describe('useSettingsStore => updateNumberOfPeople', () => {
  const id: number = 3
  const people: number = 5

  it('updateNumberOfPeople => should catch log because returns 200 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useSettingsStore()

    const spySettingsApiService = vi
      .spyOn(SettingsApiService, 'updateNumberOfPeople')
      .mockResolvedValue(200)
    const consoleMock = vi.spyOn(console, 'log').mockImplementation(() => undefined)

    // check states before act
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)

    // Act
    await sut.updateNumberOfPeople(id, people)

    // Assert
    expect(spySettingsApiService).toHaveBeenCalledTimes(1)
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith('updateNumberOfPeople was successful!')

    // Clean up after the test
    spySettingsApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateNumberOfPeople => should catch log because returns 201 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useSettingsStore()

    const spySettingsApiService = vi
      .spyOn(SettingsApiService, 'updateNumberOfPeople')
      .mockResolvedValue(201)
    const consoleMock = vi.spyOn(console, 'log').mockImplementation(() => undefined)

    // check states before act
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)

    // Act
    await sut.updateNumberOfPeople(id, people)

    // Assert
    expect(spySettingsApiService).toHaveBeenCalledTimes(1)
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith('updateNumberOfPeople was successful!')

    // Clean up after the test
    spySettingsApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateNumberOfPeople => should catch error because returns undefined', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useSettingsStore()

    const spySettingsApiService = vi
      .spyOn(SettingsApiService, 'updateNumberOfPeople')
      .mockResolvedValue(undefined)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)

    // Act
    await sut.updateNumberOfPeople(id, people)

    // Assert
    expect(spySettingsApiService).toHaveBeenCalledTimes(1)
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update number of people by id 3 with people 5. Status code: undefined'
    )

    // Clean up after the test
    spySettingsApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateNumberOfPeople => should catch error because returns error', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useSettingsStore()

    const spySettingsApiService = vi
      .spyOn(SettingsApiService, 'updateNumberOfPeople')
      .mockRejectedValue(new Error('error'))
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)

    // Act
    await sut.updateNumberOfPeople(id, people)

    // Assert
    expect(spySettingsApiService).toHaveBeenCalledTimes(1)
    expect(spySettingsApiService).rejects.toThrow('error')
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update number of people by id 3 with people 5. Error: error'
    )

    // Clean up after the test
    spySettingsApiService.mockRestore()
    consoleMock.mockReset()
  })

  it('updateNumberOfPeople => should catch error because returns 300 resopnse', async () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useSettingsStore()

    const spySettingsApiService = vi
      .spyOn(SettingsApiService, 'updateNumberOfPeople')
      .mockResolvedValue(300)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // check states before act
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)

    // Act
    await sut.updateNumberOfPeople(id, people)

    // Assert
    expect(spySettingsApiService).toHaveBeenCalledTimes(1)
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update number of people by id 3 with people 5. Status code: 300'
    )

    // Clean up after the test
    spySettingsApiService.mockRestore()
    consoleMock.mockReset()
  })
})
