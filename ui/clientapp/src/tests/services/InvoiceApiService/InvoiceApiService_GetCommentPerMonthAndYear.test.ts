import InvoicesApiService from '@/services/api/InvoicesApiService'
import { describe, it, expect, vi } from 'vitest'
import MockAdapter from 'axios-mock-adapter'
import axios from 'axios'

describe('InvoiceApiService => getCommentPerMonthAndYear', () => {
  
  it('getCommentPerMonthAndYear => should return comment', async () => {
    // Arrange
    const month = 0
    const year = '2024'

    const result: string = "this is a comment"

    const mock = new MockAdapter(axios)
    mock.onPost(`http://localhost:65513/api/invoices/getCommentPerMonthAndYear`).reply(200, result)

    // Act
    const comment = await InvoicesApiService.getCommentPerMonthAndYear(month, year)

    // Assert
    expect(mock.history.post.length).toBe(1)
    expect(mock.history.post[0].url).toBe(
      `http://localhost:65513/api/invoices/getCommentPerMonthAndYear`
    )
    expect(comment).toEqual(result)

    // Clean up after the test
    mock.restore()
  })

  it('getCommentPerMonthAndYear => should return undefined', async () => {
    // Arrange
    const month = 0
    const year = '2024'

    const result = undefined;

    const mock = new MockAdapter(axios)
    mock.onPost(`http://localhost:65513/api/invoices/getCommentPerMonthAndYear`).reply(200, result)

    // Act
    const comment = await InvoicesApiService.getCommentPerMonthAndYear(month, year)

    // Assert
    expect(mock.history.post.length).toBe(1)
    expect(mock.history.post[0].url).toBe(
      `http://localhost:65513/api/invoices/getCommentPerMonthAndYear`
    )
    expect(comment).toEqual(result)

    // Clean up after the test
    mock.restore()
  })

  it('getCommentPerMonthAndYear => should catch error and return undefined', async () => {
    // Arrange
    const month = 0
    const year = '2024'

    const mock = new MockAdapter(axios)
    mock.onPost(`http://localhost:65513/api/invoices/getCommentPerMonthAndYear`).reply(500)
    const consoleMock = vi.spyOn(console, 'error').mockImplementation(() => undefined);

    // Act
    const comment = await InvoicesApiService.getCommentPerMonthAndYear(month, year)

    // Assert
    expect(mock.history.post.length).toBe(1)
    expect(mock.history.post[0].url).toBe(
      `http://localhost:65513/api/invoices/getCommentPerMonthAndYear`
    )
    expect(comment).toBeUndefined()
    expect(consoleMock).toHaveBeenCalledOnce();
    expect(consoleMock).toHaveBeenLastCalledWith('Could not get comment per month and year 0 2024. Error: Request failed with status code 500');


    // Clean up after the test
    mock.restore()
    consoleMock.mockReset();
  })
})
