<script setup lang="ts">
import { useI18n } from 'vue-i18n'

defineProps({
  sum: {
    type: Number,
    required: true
  },
  showText: {
    type:Boolean,
    required: false,
    default: false,
  },
  text: {
    type: String,
    required: false,
    default: ''
  }
})

// variables
const { t } = useI18n()
</script>

<template>
  <div class="border-top w-100">
    <span v-if="showText" class="pe-4"> {{ t(text) }}</span>
    <span class="fw-bold fs-4">{{ sum }} €</span>
  </div>
</template>
