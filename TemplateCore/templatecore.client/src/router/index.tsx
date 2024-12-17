import { createRouter, createWebHashHistory } from 'vue-router';

const routes = [{
  path:'/basic',
  name:'basic',
  component: () => import('../components/Basic.vue')
}];

const router = createRouter({
  history: createWebHashHistory(),
  routes,
});

// app.use(router);
export default router;
