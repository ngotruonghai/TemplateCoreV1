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

const router = useRouter();

onMounted(() => {
  const Url = localStorage.getItem("Url") || '';
  const token = localStorage.getItem("Token");

  if (token === null || token === "") {
    router.push('/');
  } 
  else if (Url === null || Url === "/" || Url === "") {
    router.push("/home/dashboard");
  }
  else {
    router.push(Url);
  }
});

</script>
