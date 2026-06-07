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
      component: () => import('../views/SellView.vue'), //Si se declara asi, se carga el componente solo cuando se accede a esta ruta, lo que mejora el rendimiento de la aplicación al no cargar todos los componentes al inicio.
    },                                                  //En cambio, si declaramos el componente de esta forma: component: BuyView, se cargará el componente al inicio de la aplicación, lo que puede afectar el rendimiento si el componente es pesado o si hay muchos componentes.
    {                                                   //Declararlo como la segunda forma es util cuando la vista es muy demandada.
      path: '/transactions',                            //Para declararlo como componente, primero hay que importarlo.
      name: 'transactions',
      component: () => import('../views/TransactionsView.vue'),
    },
    {
      path: '/userRegister',                            //Para declararlo como componente, primero hay que importarlo.
      name: 'userRegister',
      component: () => import('../views/UserRegisterView.vue'),
    },
  ],
})

export default router
