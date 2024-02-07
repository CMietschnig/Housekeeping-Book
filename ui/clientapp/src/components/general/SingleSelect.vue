<script setup lang="ts">
import { type PropType } from 'vue'
import type { ISelectOption } from '@/interfaces/ISelectOption'

defineProps({
  selectOptions: {
    type: Array as PropType<ISelectOption[]>,
    required: true
  },
  selected: {
    type: [Number, String] as PropType<number | string>,
    required: false,
    default: undefined
  }
})

const emit = defineEmits(['updateSelected'])

const select = (option: ISelectOption) => {
  emit('updateSelected', option)
}
</script>

<template>
  <BDropdown
    :text="selected?.toString()"
    variant="primary"
    size="md"
    class="single-select w-100"
    menu-class="w-100"
  >
    <BDropdownItem
      v-for="(option, index) in selectOptions"
      :key="index"
      @click="select(option)"
      variant="primary"
      >{{ option.text }}</BDropdownItem
    >
  </BDropdown>
</template>
