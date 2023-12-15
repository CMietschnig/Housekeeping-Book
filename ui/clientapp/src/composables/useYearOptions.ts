import type { ISelectOption } from '@/interfaces/ISelectOption'
import { ref } from 'vue'

export default function useYearOptions() {
  const yearOptions = ref<ISelectOption[]>([
    {
      value: 2016,
      text: '2016'
    },
    {
      value: 2017,
      text: '2017'
    },
    {
      value: 2018,
      text: '2018'
    },
    {
      value: 2019,
      text: '2019'
    },
    {
      value: 2020,
      text: '2020'
    },
    {
      value: 2021,
      text: '2021'
    },
    {
      value: 2022,
      text: '2022'
    },
    {
      value: 2023,
      text: '2023'
    },
    {
      value: 2024,
      text: '2024'
    },
    {
      value: 2025,
      text: '2025'
    },
    {
      value: 2026,
      text: '2026'
    }
  ])
  return {
    yearOptions
  }
}
