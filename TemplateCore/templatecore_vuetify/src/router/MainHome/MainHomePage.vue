<template>
    <v-layout class="rounded rounded-md">
      <v-navigation-drawer id="navigation">
        <v-list>
          <v-list-item title="">
            <Navigation />
          </v-list-item>
        </v-list>
      </v-navigation-drawer>
  
      <v-app-bar title="tests"></v-app-bar>
  
      <v-main class="d-flex" id="dflex">
        <router-view />
      </v-main>
  
    </v-layout>
  </template>
  <script lang="ts" setup>

  onBeforeMount(() => {

  });
  </script>
  <style>
  #navigation{
    background-color: #2e628d;
  }
  #dflex{
    height: 100vh;
  }
</style>