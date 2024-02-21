import { describe, it, expect, vi } from 'vitest'
import MockAdapter from 'axios-mock-adapter'
import axios from 'axios'
import type { ISettings } from '@/interfaces/ISettings'
import SettingsApiService from '@/services/api/SettingsApiService'

describe('SettingsApiService => getSettingsById', () => {
  it('getSettingsById => should return settings', async () => {
    // Arrange
    const result: ISettings = {
      SettingsId: 1,
      ContributionMembersCount: 3,
      PreferredColorMode: "dark",
      CreateTimestamp: '2024-02-01T23:00:00.000Z',
      UpdateTimestamp: '2024-02-02T23:00:00.000Z'
    }

    const mock = new MockAdapter(axios)
    mock.onPost(`http://localhost:65513/api/settings/getSettingsById`).reply(200, result)

    // Act
    const settings = await SettingsApiService.getSettingsById(1)

    // Assert
    expect(mock.history.post.length).toBe(1)
    expect(mock.history.post[0].url).toBe(`http://localhost:65513/api/settings/getSettingsById`)
    expect(settings).toEqual(result)

    // Clean up after the test
    mock.restore()
  })

  it('getSettingsById => should return undefined', async () => {
    // Arrange
    const result = undefined

    const mock = new MockAdapter(axios)
    mock.onPost(`http://localhost:65513/api/settings/getSettingsById`).reply(200, result)

    // Act
    const settings = await SettingsApiService.getSettingsById(1)

    // Assert
    expect(mock.history.post.length).toBe(1)
    expect(mock.history.post[0].url).toBe(`http://localhost:65513/api/settings/getSettingsById`)
    expect(settings).toEqual(result)

    // Clean up after the test
    mock.restore()
  })

  it('getSettingsById => should catch error and return undefined', async () => {
    // Arrange
    const mock = new MockAdapter(axios)
    mock.onPost(`http://localhost:65513/api/settings/getSettingsById`).reply(500)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // Act
    const settings = await SettingsApiService.getSettingsById(1)

    // Assert
    expect(mock.history.post.length).toBe(1)
    expect(mock.history.post[0].url).toBe(`http://localhost:65513/api/settings/getSettingsById`)
    expect(settings).toBeUndefined()
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not get settings by id 1. Error: Request failed with status code 500'
    )

    // Clean up after the test
    mock.restore()
    consoleMock.mockReset()
  })
})
