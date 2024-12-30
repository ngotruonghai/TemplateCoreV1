<template>
    <v-layout class="rounded rounded-md">
      <v-navigation-drawer>
        <v-list>
          <v-list-item title="Template Core">
            <Navigation />
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
  
      <v-app-bar title="test"></v-app-bar>
  
      <v-main class="d-flex" style="min-height: 800px;">
        <router-view />
      </v-main>
  
    </v-layout>
  </template>
  <script lang="ts" setup>

  onBeforeMount(() => {

  });
  </script>