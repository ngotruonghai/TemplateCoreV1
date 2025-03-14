<template>
  <v-layout style="height: 100vh; overflow: hidden;" class="rounded rounded-md">
    <v-navigation-drawer id="navigation">
      <a-menu id="dddddd" v-model:openKeys="openKeys" v-model:selectedKeys="selectedKeys" style="width: 256px"
        mode="inline" :items="items" @click="handleClick"></a-menu>
    </v-navigation-drawer>

    <!-- <v-app-bar title="" id="TitleName">
  
    </v-app-bar> -->

    <v-main style="height: 100%; overflow-y: auto;">
      <router-view />
    </v-main>
  </v-layout>

</template>
<script lang="ts" setup>
import { h, ref } from 'vue';
import type { MenuProps } from 'ant-design-vue';
import {
  MailOutlined,
  CalendarOutlined,
  AppstoreOutlined,
  SettingOutlined,
} from '@ant-design/icons-vue';
import type { MenuTheme } from 'ant-design-vue';
const theme = ref<MenuTheme>('dark');
const selectedKeys = ref(['']);
const openKeys = ref(['']);
const items = ref([
  {
    key: '1',
    icon: () => h(MailOutlined),
    label: 'Navigation One',
    title: 'Navigation One',
  },
  {
    key: '2',
    icon: () => h(CalendarOutlined),
    label: 'Navigation Two',
    title: 'Navigation Two',
  },
  {
    key: 'sub1',
    icon: () => h(AppstoreOutlined),
    label: 'Navigation Three',
    title: 'Navigation Three',
    children: [
      {
        key: '3',
        label: 'Option 3',
        title: 'Option 3',
      },
      {
        key: '4',
        label: 'Option 4',
        title: 'Option 4',
      },
      {
        key: 'sub1-2',
        label: 'Submenu',
        title: 'Submenu',
        children: [
          {
            key: '5',
            label: 'Option 5',
            title: 'Option 5',
          },
          {
            key: '6',
            label: 'Option 6',
            title: 'Option 6',
          },
        ],
      },
    ],
  },
  {
    key: 'sub2',
    icon: () => h(SettingOutlined),
    label: 'Navigation Four',
    title: 'Navigation Four',
    children: [
      {
        key: '7',
        label: 'Option 7',
        title: 'Option 7',
      },
      {
        key: '15649846165161/dsajhdsj/soaidjosji',
        label: 'Option 8',
        title: 'Option 8',
      },
      {
        key: '9',
        label: 'Option 9',
        title: 'Option 9',
      },
      {
        key: '10',
        label: 'Option 10',
        title: 'Option 10',
      },
    ],
  },
]);

const handleClick: MenuProps['onClick'] = menuInfo => {
  console.log('click ', menuInfo);
};

watch(openKeys, val => {
  console.log('openKeys', val);
});

onBeforeMount(() => {

});

</script>

<style scoped>
.ListItemClass {
  color: #f5f5f5;
}

.SelectedTile:hover {
  border-radius: 4px;
  background: #455A64
}

.SelectedTile-active {
  border-radius: 4px;
  background: rgba(10, 204, 117, 0.19)
}
</style>