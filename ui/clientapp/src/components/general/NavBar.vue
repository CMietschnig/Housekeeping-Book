<script setup lang="ts">
import { useI18n } from 'vue-i18n'

const { t } = useI18n()
</script>

<template>
  <BNavbar toggleable="lg" variant="primary">
    <BNavbarBrand>{{ t('general.menu') }}</BNavbarBrand>
    <BNavbarToggle target="nav-collapse" />
    <BCollapse id="nav-collapse" is-nav>
      <BNavbarNav class="justify-content-center">
        <BNavItem to="/"> {{ t('general.dashboard') }} </BNavItem>
        <BNavItem to="/editMonth">{{ t('general.editMonth') }}</BNavItem>
        <BNavItem to="/settings">{{ t('general.settings') }}</BNavItem>
      </BNavbarNav>
      <BNavbarNav class="ms-auto mb-2 mb-lg-0">
        <LanguageSelector />
        <ColorModeSwitcher />
      </BNavbarNav>
    </BCollapse>
  </BNavbar>
</template>
