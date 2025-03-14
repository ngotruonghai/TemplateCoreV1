/**
 * main.ts
 *
 * Bootstraps Vuetify and other plugins then mounts the App`
 */

// Plugins
import { registerPlugins } from '@/plugins'
import router from './router/index'
import Antd from 'ant-design-vue'
import 'ant-design-vue/dist/reset.css';
// import 'ant-design-vue/dist/antd.css'

// Components
import App from './App.vue'

// Composables
import { createApp } from 'vue'

const app = createApp(App)

registerPlugins(app)
app.use(router);
app.use(Antd);
app.mount('#app')
