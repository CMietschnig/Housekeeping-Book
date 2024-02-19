<script setup lang="ts">
import { useI18n } from 'vue-i18n'
import type { IInvoice } from '@/interfaces/IInvoice'
import { type PropType } from 'vue'

defineProps({
  invoices: {
    type: Array as PropType<Array<IInvoice>>,
    required: true
  }
})

const { t } = useI18n()
</script>

<template>
  <div
    v-if="invoices.length > 0"
    class="d-flex flex-column align-items-center align-items-sm-start pe-0 pe-sm-5 pb-4"
  >
    <span>{{ t('general.numberOfInvoices') }}</span>
    <span class="ps-0 ps-sm-4">{{ invoices.length }} </span>
  </div>
</template>
