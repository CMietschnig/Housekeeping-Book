import type { Month } from '@/enums/MonthEnums'

export interface ISettings {
  SettingsId: number
  ContributionMembersCount: number
  Year: string
  MonthId: Month
  CreateTimestamp: Date | string
  UpdateTimestamp: Date | string
}
