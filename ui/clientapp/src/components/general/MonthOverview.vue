<script setup lang="ts">

defineProps({
  month: {
    type: String,
    required: true
  },
  sum: {
    type: Number,
    required: true
  },
  contributionMembers: {
    type: Number,
    required: true
  }
})
</script>

<template>
  <div class="overview-container border p-3">
    <h2>{{ month }}</h2>
    <span class="fw-bold fs-4"> {{ sum }} €</span>
    <SumPerContributionMember v-if="sum > 0" :sum="sum" :contribution-members="contributionMembers" />
  </div>
</template>
