import { describe, it, expect, vi } from 'vitest'
import MockAdapter from 'axios-mock-adapter'
import axios from 'axios'
import SettingsApiService from '@/services/api/SettingsApiService'
import type { IUpdateSettings } from '@/interfaces/IUpdateSettings'

describe('SettingsApiService => updateSettingsById', () => {
  const updateSettingsModel: IUpdateSettings = {
    SettingsId: 3,
    ContributionMembersCount: 4,
    PreferredColorMode: "dark"
  }

  it('updateSettingsById => should return number 200', async () => {
    // Arrange
    const expectedResult: number = 200

    const mock = new MockAdapter(axios)
    mock.onPut(`http://localhost:65513/api/settings/updateSettingsById`).reply(200)

    // Act
    const response = await SettingsApiService.updateSettingsById(updateSettingsModel)

    // Assert
    expect(mock.history.put.length).toBe(1)
    expect(mock.history.put[0].url).toBe(`http://localhost:65513/api/settings/updateSettingsById`)
    expect(response).toEqual(expectedResult)

    // Clean up after the test
    mock.restore()
  })

  it('updateSettingsById => should return number 201', async () => {
    // Arrange
    const expectedResult: number = 201

    const mock = new MockAdapter(axios)
    mock.onPut(`http://localhost:65513/api/settings/updateSettingsById`).reply(201)

    // Act
    const response = await SettingsApiService.updateSettingsById(updateSettingsModel)

    // Assert
    expect(mock.history.put.length).toBe(1)
    expect(mock.history.put[0].url).toBe(`http://localhost:65513/api/settings/updateSettingsById`)
    expect(response).toEqual(expectedResult)

    // Clean up after the test
    mock.restore()
  })

  it('updateSettingsById => should return undefined: Error 404', async () => {
    // Arrange
    const mock = new MockAdapter(axios)
    mock.onPut(`http://localhost:65513/api/settings/updateSettingsById`).reply(404)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // Act
    const response = await SettingsApiService.updateSettingsById(updateSettingsModel)

    // Assert
    expect(mock.history.put.length).toBe(1)
    expect(mock.history.put[0].url).toBe(`http://localhost:65513/api/settings/updateSettingsById`)
    expect(response).toBeUndefined()
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update settings by id 3. Error: Request failed with status code 404'
    )

    // Clean up after the test
    mock.restore()
    consoleMock.mockReset()
  })

  it('updateSettingsById => should catch error and return undefined: Error 500', async () => {
    // Arrange
    const mock = new MockAdapter(axios)
    mock.onPut(`http://localhost:65513/api/settings/updateSettingsById`).reply(500)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // Act
    const response = await SettingsApiService.updateSettingsById(updateSettingsModel)

    // Assert
    expect(mock.history.put.length).toBe(1)
    expect(mock.history.put[0].url).toBe(`http://localhost:65513/api/settings/updateSettingsById`)
    expect(response).toBeUndefined()
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update settings by id 3. Error: Request failed with status code 500'
    )

    // Clean up after the test
    mock.restore()
    consoleMock.mockReset()
  })
})
