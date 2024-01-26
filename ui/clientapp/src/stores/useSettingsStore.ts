import SettingsApiService from '@/services/api/SettingsApiService'
import { defineStore } from 'pinia'

export interface SettingsStoreState {
  month: number
  year: string
  people: number
}

const DefaultSettingsState: SettingsStoreState = {
  month: new Date().getMonth(),
  year: new Date().getFullYear().toString(),
  people: 1
}

export const useSettingsStore = defineStore({
  id: 'settings-store',
  state: (): SettingsStoreState => ({
    ...DefaultSettingsState
  }),
  getters: {
    getMonth: (state): number => state.month,
    getYear: (state): string => state.year,
    getPeople: (state): number => state.people
  },
  actions: {
    selectMonth(value: number) {
      if (value >= 0 && value < 12) {
        this.$patch((state) => {
          state.month = value
        })
      } else {
        console.error('Could not select month ' + value + '. The value is not valid.')
      }
    },
    selectYear(value: string) {
      if (value && parseInt(value) >= 2016 && parseInt(value) < 2030) {
        this.$patch((state) => {
          state.year = value
        })
      } else {
        console.error('Could not select year ' + value + '. The value is not valid.')
      }
    },
    async updateNumberOfPeople(id: number, people: number) {
      try {
        const response = await SettingsApiService.updateNumberOfPeople(id, people)

        if (response && response >= 200 && response < 300) {
          console.log('updateNumberOfPeople was successful!')
        } else {
          console.error(
            'Could not update number of people by id ' +
              id +
              ' with people ' +
              people +
              '. Status code: ' +
              response
          )
        }
      } catch (e) {
        console.error(
          'Could not update number of people by id ' + id + ' with people ' + people + '. ' + e
        )
      }
    }
  }
})
