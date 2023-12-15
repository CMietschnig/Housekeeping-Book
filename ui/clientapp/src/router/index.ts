import { createRouter, createWebHistory } from 'vue-router'
import DashboardView from '../views/DashboardView.vue'
import EditMonthView from '../views/EditMonthView.vue'
import SettingsView from '@/views/SettingsView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'Dashboard',
      component: DashboardView
    },
    {
      path: '/editMonth',
      name: 'Edit month',
      component: EditMonthView
    },
    {
      path: '/settings',
      name: 'Settings',
      component: SettingsView
    }
  ]
})

export default router
