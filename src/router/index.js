import { createRouter, createWebHistory } from 'vue-router'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      name: 'wallet',
      component: () => import('../views/WalletView.vue'),
    },
    {
      path: '/buy',
      name: 'buy',
      component: () => import('../views/BuyView.vue'),
    },
    {
      path: '/sell',
      name: 'sell',
      component: () => import('../views/SellView.vue'),
    },
    {
      path: '/transactions',
      name: 'transactions',
      component: () => import('../views/TransactionsView.vue'),
    },
  ],
})

export default router
