<script setup lang="ts">

defineProps({
  month: String,
  sum: Number
})
</script>

<template>
  <div class="overview-container border p-3">
    <h2>{{ month }}</h2>
    <span> {{ sum }} €</span>
  </div>
</template>

<style scoped>
.overview-container {
  flex-basis: 100%;
}
</style>