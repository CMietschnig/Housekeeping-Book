import { describe, it, expect, vi } from 'vitest'
import MockAdapter from 'axios-mock-adapter'
import axios from 'axios'
import SettingsApiService from '@/services/api/SettingsApiService'

describe('SettingsApiService => updateNumberOfPeople', () => {
  it('updateNumberOfPeople => should return number 200', async () => {
    // Arrange
    const id = 1
    const people = 3

    const expectedResult: number = 200

    const mock = new MockAdapter(axios)
    mock.onPut(`http://localhost:65513/api/settings/updateNumberOfPeople`).reply(200)

    // Act
    const response = await SettingsApiService.updateNumberOfPeople(id, people)

    // Assert
    expect(mock.history.put.length).toBe(1)
    expect(mock.history.put[0].url).toBe(`http://localhost:65513/api/settings/updateNumberOfPeople`)
    expect(response).toEqual(expectedResult)

    // Clean up after the test
    mock.restore()
  })

  it('updateNumberOfPeople => should return number 201', async () => {
    // Arrange
    const id = 2
    const people = 2

    const expectedResult: number = 201

    const mock = new MockAdapter(axios)
    mock.onPut(`http://localhost:65513/api/settings/updateNumberOfPeople`).reply(201)

    // Act
    const response = await SettingsApiService.updateNumberOfPeople(id, people)

    // Assert
    expect(mock.history.put.length).toBe(1)
    expect(mock.history.put[0].url).toBe(`http://localhost:65513/api/settings/updateNumberOfPeople`)
    expect(response).toEqual(expectedResult)

    // Clean up after the test
    mock.restore()
  })

  it('updateNumberOfPeople => should return undefined: Error 404', async () => {
    // Arrange
    const id = 3
    const people = 6

    const mock = new MockAdapter(axios)
    mock.onPut(`http://localhost:65513/api/settings/updateNumberOfPeople`).reply(404)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // Act
    const response = await SettingsApiService.updateNumberOfPeople(id, people)

    // Assert
    expect(mock.history.put.length).toBe(1)
    expect(mock.history.put[0].url).toBe(`http://localhost:65513/api/settings/updateNumberOfPeople`)
    expect(response).toBeUndefined()
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update number of people by id 3 with people 6. Error: Request failed with status code 404'
    )

    // Clean up after the test
    mock.restore()
    consoleMock.mockReset()
  })

  it('updateNumberOfPeople => should catch error and return undefined: Error 500', async () => {
    // Arrange
    const id = 5
    const people = 4

    const mock = new MockAdapter(axios)
    mock.onPut(`http://localhost:65513/api/settings/updateNumberOfPeople`).reply(500)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined)

    // Act
    const response = await SettingsApiService.updateNumberOfPeople(id, people)

    // Assert
    expect(mock.history.put.length).toBe(1)
    expect(mock.history.put[0].url).toBe(`http://localhost:65513/api/settings/updateNumberOfPeople`)
    expect(response).toBeUndefined()
    expect(consoleMock).toHaveBeenCalledOnce()
    expect(consoleMock).toHaveBeenLastCalledWith(
      'Could not update number of people by id 5 with people 4. Error: Request failed with status code 500'
    )

    // Clean up after the test
    mock.restore()
    consoleMock.mockReset()
  })
})
