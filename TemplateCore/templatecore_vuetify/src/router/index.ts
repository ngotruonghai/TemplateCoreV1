import { createRouter, createWebHistory } from 'vue-router/auto'
import MainPage from '../router/MainHome/MainHomePage.vue'
import LoginPage from '../router/Login/loginPage.vue'

const routess = [
  {
    path: '/',
    name: 'Trang chủ',
    component: () => MainPage
  },
  {
    path: '/home',
    name: 'Home',
    component: () => MainPage,
    children: [
      {
        path: 'acount',
        name: 'Acount',
        component: import('../router/UserAcount/Acount.vue'), // Component con cho /main/home
      },
      {
        path: 'dashboard',
        name: 'Dashboard',
        component: import('../router/Dashboard/Dashboard.vue'), // Component con cho /main/home
      },
    ]
  },
  {
    path: '/login',
    name: 'Đăng nhập',
    component: () => LoginPage
  },
];

const router = createRouter({
  history: createWebHistory(import.meta.env.BASE_URL),
  routes: routess,//setupLayouts(routes),
})

// Workaround for https://github.com/vitejs/vite/issues/11804
router.onError((err, to) => {
  if (err?.message?.includes?.('Failed to fetch dynamically imported module')) {
    if (!localStorage.getItem('vuetify:dynamic-reload')) {
      console.log('Reloading page to fix dynamic import error')
      localStorage.setItem('vuetify:dynamic-reload', 'true')
      location.assign(to.fullPath)
    } else {
      console.error('Dynamic import error, reloading page did not fix it', err)
    }
  } else {
    console.error(err)
  }
})

router.isReady().then(() => {
  localStorage.removeItem('vuetify:dynamic-reload')
})

export default router
