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
  const token = LocalStorageService.GetToken();
  const url = router.currentRoute.value.fullPath;

  if(token == null){
    router.push('/login');
  }
  else{
    router.push(localStorage.getItem("Url")??"/");
  }
 
});

</script>
