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

function test(name: string) {
  console.log(name);

}

onMounted(() => {
  const Url = localStorage.getItem("Url") || '';
  const token = localStorage.getItem("Token");

  if (token === null || token === "") {
    router.push('/');
  } else {
    router.push(Url);
  }

  /* cấu hình .NET
    app.UseEndpoints(endpoints =>
  {
      endpoints.MapFallbackToFile("index.html"); // Điều hướng tất cả request về index.html
  });
  
  */
});

</script>
