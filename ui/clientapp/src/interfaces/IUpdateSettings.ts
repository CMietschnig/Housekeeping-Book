import type { Month } from '@/enums/MonthEnums'

export interface IUpdateSettings {
  SettingsId: number
  ContributionMembersCount: number
  Year: string
  MonthId: Month
}
