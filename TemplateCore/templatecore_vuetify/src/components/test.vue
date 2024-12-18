<template>
  <v-layout class="rounded rounded-md">
    <v-navigation-drawer>
      <v-list>
        <v-list-item title="Navigation drawer">
          <Testcomponent />
          <Testcomponent />
        </v-list-item>
      </v-list>
    </v-navigation-drawer>

    <v-app-bar title="test"></v-app-bar>

    <v-main class="d-flex" style="min-height: 800px;">
      <v-container class="">
        <v-row class="mb-6" no-gutters>
          <v-col cols="4">
            <v-sheet class="pa-2 ma-2">
              .v-col-4
            </v-sheet>
          </v-col>
          <v-col cols="4" offset="4">
            <v-sheet class="pa-2 ma-2">
              .v-col-4 .offset-4
            </v-sheet>
          </v-col>
        </v-row>
        <v-row class="mb-6" no-gutters>
          <v-col cols="4">
            <v-sheet class="pa-2 ma-2">
              .v-col-4
            </v-sheet>
          </v-col>
          <v-col cols="4" offset="4">
            <v-sheet class="pa-2 ma-2">
              .v-col-4 .offset-4
            </v-sheet>
          </v-col>
        </v-row>
      </v-container>
    </v-main>

  </v-layout>
</template>
<script setup>

</script>