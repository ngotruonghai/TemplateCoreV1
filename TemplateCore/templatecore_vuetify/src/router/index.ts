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
        path: 'account',
        name: 'Account',
        component: import('../router/UserAcount/Acount.vue'), // Component con cho /main/home
      },
      {
        path: 'dashboard',
        name: 'Dashboard',
        component: import('../router/Dashboard/Dashboard.vue'), // Component con cho /main/home
      },
      {
        path:'userId/:id',
        name:'UserId',
        component: import('../router/UserAcount/UserInfor.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'createuser',
        name:'CreateUser',
        component: import('../router/UserAcount/CreateAccountNew.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'themquytrinh',
        name:'ThemQuyTrinh',
        component: import('../router/QuyTrinhNode/AddQuyTrinh.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'taophongban',
        name:'TaoPhongBan',
        component: import('../router/PhongBan/CreatePhongBan.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'phongban',
        name:'PhongBan',
        component: import('../router/PhongBan/DanhSachPhongBan.vue'), // Component con cho /main/home
        props: true
      }
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
