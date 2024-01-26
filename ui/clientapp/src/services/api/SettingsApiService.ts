import axios from 'axios'
import { EnviromentVariables } from '@/enums/EnviromentVariables'

class SettingsApiService {
    private static readonly baseUrl: string = EnviromentVariables.API_BASE_URL

    async updateNumberOfPeople(id: number, people: number): Promise<number | undefined> {
        try {
          const response = await axios.put(
            `${SettingsApiService.baseUrl}/api/settings/updateNumberOfPeople`,
            {
              id,
              people
            },
            {
              headers: {
                'content-type': 'application/json'
              }
            }
          )
          return response.status
        } catch (e) {
          console.error(
            'Could not update number of people by id ' + id + ' with people ' + people + '. ' + e
          )
          return undefined // Return undefined in case of an error
        }
      }
}
export default new SettingsApiService()