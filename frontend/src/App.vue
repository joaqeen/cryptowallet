<script setup>
import { RouterLink, RouterView } from 'vue-router'
import AppNavBar from './components/layout/AppNavBar.vue';
import AppFooter from './components/layout/AppFooter.vue';
</script>

<template>
  <AppNavBar />
  <RouterView />
  <AppFooter />
</template>

<style scoped>

</style>
