<template>
  <v-app>
    <v-main>
      <router-view />
    </v-main>
  </v-app>
</template>

<script lang="ts" setup>
console.log("dsads");
const apiBaseUrl = import.meta.env.VITE_API_URL;

console.log(`API Base URL: ${apiBaseUrl}`); // https://dev-api.example.com (hoặc production URL)

</script>
