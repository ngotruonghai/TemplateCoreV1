<template>
  <v-layout style="height: 100vh; overflow: hidden;" class="rounded rounded-md">
    <v-navigation-drawer id="navigation"  >
      <v-list>
        <v-list-item title="">
          <Navigation @dataSent="handleData" />
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <!-- <v-app-bar title="" id="TitleName">
  
    </v-app-bar> -->

    <v-main style="height: 100%; overflow-y: auto;">
      <router-view />
    </v-main>
  </v-layout>

</template>
<script lang="ts" setup>
const receivedData = ref<string | null>(null);

const handleData = (data: string) => {
  receivedData.value = data;
};

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