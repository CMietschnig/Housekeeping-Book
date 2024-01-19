import { describe, it, expect } from 'vitest'
import { createPinia, setActivePinia } from 'pinia'
import { useSettingsStore } from '@/stores/useSettingsStore'

describe('useSettingsStore => getters, default state', () => {
  it('useSettingsStore => should return default states', () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useSettingsStore()
    // Assert
    expect(sut.month).toEqual(new Date().getMonth())
    expect(sut.year).toEqual(new Date().getFullYear().toString())
    expect(sut.people).toEqual(1)
  })

  it('useSettingsStore => should return correct values for getters', () => {
    // Arrange
    setActivePinia(createPinia())
    const sut = useSettingsStore()

    // Assert
    expect(sut.getMonth).toEqual(new Date().getMonth())
    expect(sut.getYear).toEqual(new Date().getFullYear().toString())
    expect(sut.getPeople).toEqual(1)
  })
})
