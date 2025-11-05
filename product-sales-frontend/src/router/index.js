import { createRouter, createWebHistory } from 'vue-router'
import LoginView from '../views/LoginView.vue'
import ProductsView from '../views/ProductsView.vue'
import ClientsView from '../views/ClientsView.vue'
import SalesView from '../views/SalesView.vue'

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: [
    {
      path: '/',
      redirect: '/login'
    },
    {
      path: '/login',
      name: 'login',
      component: LoginView
    },
    {
      path: '/products',
      name: 'products',
      component: ProductsView,
      meta: { requiresAuth: true }
    },
    {
      path: '/clients',
      name: 'clients',
      component: ClientsView,
      meta: { requiresAuth: true }
    },
    {
      path: '/sales',
      name: 'sales',
      component: SalesView,
      meta: { requiresAuth: true }
    }
  ]
})

// Route guard for authentication
router.beforeEach((to, from, next) => {
  const token = localStorage.getItem('token');
  
  if (to.meta.requiresAuth && !token) {
    next('/login');
  } else if (to.path === '/login' && token) {
    next('/products');
  } else {
    next();
  }
});

export default router