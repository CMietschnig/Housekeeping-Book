<script setup lang="ts">
import useColorModes from '@/composables/useColorModes'
import { useSettingsStore } from '@/stores/useSettingsStore'
import { storeToRefs } from 'pinia'
import { onBeforeMount } from 'vue';
import { useI18n } from 'vue-i18n'

const settingsStore = useSettingsStore()
const { getPreferredColorMode: savedPreferredColorMode } = storeToRefs(settingsStore)

onBeforeMount(async () => {
  await settingsStore.getSettingsById(1)
  changeMode(savedPreferredColorMode.value);
})

const { t } = useI18n()
const { colorModes } = useColorModes()

const changeMode = (mode: string) => {
  document.documentElement.setAttribute('data-bs-theme', mode.toLowerCase())
}
</script>
<template>
  <BDropdown
    :text="t('general.theme')"
    variant="primary"
    size="md"
    class="color-mode-switcher"
    menu-class="w-100"
  >
    <BDropdownItem
      v-for="(mode, index) in colorModes"
      :key="index"
      @click="changeMode(mode.value.toString())"
      variant="primary"
      >{{ mode.text }}</BDropdownItem
    >
  </BDropdown>
</template>
