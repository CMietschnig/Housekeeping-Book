<script setup lang="ts">
import { useI18n } from 'vue-i18n'

defineProps({
  sum: {
    type: Number,
    required: true
  },
  contributionMembers: {
    type: Number,
    required: true
  }
})

const { t } = useI18n()
</script>

<template>
  <div v-if="contributionMembers > 1" class="d-flex flex-column">
    <span>
      {{ t('general.totalContributionPerPerson') + ' (' + contributionMembers + ')' }}
      <font-awesome-icon
        class="ps-1"
        icon="fa-solid fa-circle-info"
        v-b-tooltip.hover.top="t('general.perPersonSetting')"
    /></span>
    <span class="fw-bold"> {{ (sum / contributionMembers).toFixed(2) }} €</span>
  </div>
</template>
