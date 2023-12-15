import { defineStore } from "pinia";

export interface SettingsStoreState {
    month: number,
    year: number,
    people: number,
}

const DefaultSettingsState: SettingsStoreState = {
    month: new Date().getMonth(),
    year: new Date().getFullYear(),
    people: 1,
}

export const useSettingsStore = defineStore({
    id: 'settings-store',
    state: (): SettingsStoreState => ({
        ...DefaultSettingsState
    }),
    getters: {
        getMonth: (state): number => state.month,
        getYear: (state): number => state.year,
        getPeople: (state): number => state.people,
    },
    actions: {
        async selectMonth(value: number) {
            try {
              console.log('select month in store', value)
            } catch (e) {
              console.error('Could not select month ' + value + '. ' + e)
            }
          },
          async selectYear(value: number) {
            try {
              console.log('select year in store', value)
            } catch (e) {
              console.error('Could not select year ' + value + '. ' + e)
            }
          }
    }
})