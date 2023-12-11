<script setup lang="ts">
import LocaleChanger from "./LocaleChanger.vue";
import { useI18n } from "vue-i18n";

const { t } = useI18n();
</script>

<template>
  <BNavbar toggleable="lg" variant="primary" v-b-color-mode="'dark'">
    <BNavbarBrand href="#">{{ t("general.menu") }}</BNavbarBrand>
    <BNavbarToggle target="nav-collapse" />
    <BCollapse id="nav-collapse" is-nav>
      <BNavbarNav>
        <BNavItem to="/"> {{ t("general.dashboard") }} </BNavItem>
        <BNavItem to="/editMonth">{{ t("general.editMonth") }}</BNavItem>
        <LocaleChanger />
      </BNavbarNav>
    </BCollapse>
  </BNavbar>
</template>
