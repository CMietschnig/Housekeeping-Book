<script setup lang="ts">
defineProps({
  sum: {
    type: Number,
    required: true
  },
  people: {
    type: Number,
    required: true
  }
})
</script>

<template>
  <div v-if="people > 1">
    <span> {{ sum / people }}</span>
  </div>
</template>
