<script setup lang="ts">
const changeMode = (mode: string) => {
  document.documentElement.setAttribute('data-bs-theme', mode.toLowerCase())
  // add settingsstore method to save the preferred color mode in database
}
</script>

<template>
  <div>
    <BDropdown right text="Mode" class="color-mode-switcher">
      <BDropdownItem @click="changeMode('light')">Light</BDropdownItem>
      <BDropdownItem @click="changeMode('dark')">Dark</BDropdownItem>
      <BDropdownDivider />
      <BDropdownItem @click="changeMode('blue')">Blue</BDropdownItem>
    </BDropdown>
  </div>
</template>

<style scoped></style>
