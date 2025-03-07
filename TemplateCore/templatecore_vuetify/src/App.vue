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
import { callApi, callAuthenticationAPI, convertToDate } from '@/providers/data-provider';
const router = useRouter();

async function LoadDanhSachPhongBan() {
  try {
    const result = await callAuthenticationAPI('/api/accountpermission/AccountPermission', 'GET', {}, { timeout: 15000 });
    
  } catch (error) {
    router.push('/');
  }
}

onMounted(() => {
  const Url = localStorage.getItem("Url") || '';
  const token = localStorage.getItem("Token");

  if (token === null || token === "") {
    router.push('/');
  } 
  else if (Url === null || Url === "/" || Url === "") {
    LoadDanhSachPhongBan();
    router.push("/home/dashboard");
  }
  else {
    LoadDanhSachPhongBan();
    router.push(Url);
  }
});

</script>
