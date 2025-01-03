<template>
  <v-layout style="height: 100vh; overflow: hidden;" class="rounded rounded-md">
    <v-navigation-drawer id="navigation" app>
      <v-list>
        <v-list-item title="">
          <Navigation @dataSent="handleData" />
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar title="" id="TitleName" app>
      <div>
        {{ receivedData }}
      </div>
    </v-app-bar>

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

<style>
#TitleName {
  font-size: 18px;
  /* Kích thước chữ */
  padding: 10px 20px;
  /* Khoảng cách bên trong */
  display: flex;
  align-items: center;
  justify-content: space-between;
}
</style>