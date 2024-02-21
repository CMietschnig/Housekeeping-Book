// src/tests/axios.test.ts
import axios from 'axios'
import MockAdapter from 'axios-mock-adapter'
import { describe, it, expect } from 'vitest'

describe('Axios', () => {
  it('should mock Axios post', async () => {
    const mock = new MockAdapter(axios)
    const result = [{ id: 1, data: 'mocked data' }]
    mock.onPost('https://example.com/api/mock').reply(200, result)

    const response = await axios.post('https://example.com/api/mock')

    expect(response.data).toEqual(result)

    mock.restore() // Clean up after the test
  })
})
