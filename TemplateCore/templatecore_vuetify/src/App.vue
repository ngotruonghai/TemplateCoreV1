<template>
  <v-app>
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts" setup>
import { LocalStorageService } from '@/providers/LocalStorageServices';
import { useRouter } from 'vue-router';
import path from 'path';
const router = useRouter();



onMounted(() => {
  const Url = localStorage.getItem("Url") || '';
  const token = localStorage.getItem("Token");
  if (token === null || token === "") {
    router.push('/');
  } else {
    router.push(Url);
  }
});

</script>
