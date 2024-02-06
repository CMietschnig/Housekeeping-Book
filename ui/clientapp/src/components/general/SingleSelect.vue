<script setup lang="ts">
import { type PropType } from 'vue'
import { useVModel } from '@vueuse/core'

const props = defineProps({
  selectOptions: {
    type: Array,
    required: true
  },
  selected: {
    type: [Number, String, null] as PropType<number | string | null>,
    required: false,
    default: null
  },
  placeholder: {
    type: String,
    required: true
  }
})

const emit = defineEmits(['update:selected'])

// variables
const selectedValue = useVModel(props, 'selected', emit)
</script>

<template>
  <BFormSelect v-model="selectedValue" :options="selectOptions" class="single-select">
    <template #first>
      <BFormSelectOption :value="null" disabled>{{ placeholder }}</BFormSelectOption>
    </template>
  </BFormSelect>
</template>
