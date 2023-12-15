import type { ComputedRef } from "vue"

export interface ISelectOption {
  value: Number
  text: String | ComputedRef<string>
}
