import axios from 'axios'
import { EnviromentVariables } from '@/enums/EnviromentVariables'
import type { IUpdateSettings } from '@/interfaces/IUpdateSettings'
import type { ISettings } from '@/interfaces/ISettings'

class SettingsApiService {
  private static readonly baseUrl: string = EnviromentVariables.API_BASE_URL

  async updateSettingsById(updateSettingsModel: IUpdateSettings): Promise<number | undefined> {
    try {
      const response = await axios.put(
        `${SettingsApiService.baseUrl}/api/settings/updateSettingsById`,
        updateSettingsModel,
        {
          headers: {
            'content-type': 'application/json'
          }
        }
      )
      return response.status
    } catch (e) {
      console.error('Could not update settings by id ' + updateSettingsModel.SettingsId + '. ' + e)
      return undefined // Return undefined in case of an error
    }
  }
  async getSettingsById(id: number): Promise<ISettings | undefined> {
    try {
      const response = await axios.post(
        `${SettingsApiService.baseUrl}/api/settings/getSettingsById`,
        id,
        {
          headers: {
            'content-type': 'application/json'
          }
        }
      )
      return response.data
    } catch (e) {
      console.error('Could not get settings by id ' + id + '. ' + e)
      return undefined // Return undefined in case of an error
    }
  }
}
export default new SettingsApiService()
