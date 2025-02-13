import { createRouter, createWebHistory,createWebHashHistory  } from 'vue-router'


const routess = [
  {
    path: '/',
    name: 'Đăng nhập',
    component:  () => import('../router/Login/loginPage.vue')
  },
  {
    path: '/home',
    name: 'Home',
    component:  () => import('../router/MainHome/MainHomePage.vue'),
     redirect: '/home',
    children: [
      {
        path: 'account',
        name: 'Account',
        component:() => import('../router/UserAcount/Acount.vue'), // Component con cho /main/home
      },
      {
        path: 'dashboard',
        name: 'Dashboard',
        component:() => import('../router/Dashboard/Dashboard.vue'), // Component con cho /main/home
      },
      {
        path:'userId/:id',
        name:'UserId',
        component:() => import('../router/UserAcount/UserInfor.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'createuser',
        name:'CreateUser',
        component:() => import('../router/UserAcount/CreateAccountNew.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'taophongban',
        name:'TaoPhongBan',
        component:() => import('../router/PhongBan/CreatePhongBan.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'phongban',
        name:'PhongBan',
        component:() => import('../router/PhongBan/DanhSachPhongBan.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'danhsachquytrinh',
        name:'DanhSachQuyTrinh',
        component:() => import('../router/QuyTrinhNode/DanhSachQuyTrinh.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'taoquytrinh',
        name:'TaoQuyTrinh',
        component:() => import('../router/QuyTrinhNode/AddQuyTrinh.vue'), // Component con cho /main/home
        props: true
      },
      {
        path:'quytrinh/:id',
        name:'QuyTrinhId',
        component:() => import('../router/QuyTrinhNode/UpdateQuyTrinh.vue'), // Component con cho /main/home
        props: true
      },
    ]
  }, 
];

const router = createRouter({
  history: createWebHistory("/"), // ✅ Sử dụng biến môi trường
  routes: routess,//setupLayouts(routes),
})

router.onError((err, to) => {
  console.error('Router Error:', err)

  // Kiểm tra lỗi import động
  if (err.message.includes('Failed to fetch dynamically imported module')) {
    if (!localStorage.getItem('vuetify:dynamic-reload')) {
      console.log('Reloading page to fix dynamic import error...')
      localStorage.setItem('vuetify:dynamic-reload', 'true')
      window.location.reload()
    } else {
      console.error('Dynamic import error persists after reload:', err)
    }
  }
})


router.isReady().then(() => {
  localStorage.removeItem('vuetify:dynamic-reload')
})

export default router
