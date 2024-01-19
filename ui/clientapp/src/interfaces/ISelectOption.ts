import type { ComputedRef } from "vue"

export interface ISelectOption {
  value: Number | String
  text: String | ComputedRef<string>
}
