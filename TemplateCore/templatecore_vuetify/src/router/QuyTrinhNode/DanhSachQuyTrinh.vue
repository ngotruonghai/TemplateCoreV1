<template>
    <div class="ChillColor">
        <v-container>
            <div class="layoutChill">
                <v-row>
                    <v-col cols="12" md="12" style="float: right;">
                        <button class="btnAdd btn" v-on:click="btnCreateClick">
                            <span class="icon-border">
                                <i class="fas fa-plus"></i>
                            </span> Thêm mới quy trình
                        </button>
                    </v-col>
                </v-row>
            </div>
        </v-container>
    </div>
</template>

<script lang="ts" setup>
import { useRouter } from 'vue-router';
const router = useRouter();

function btnCreateClick() {
    router.push("/home/taoquytrinh");
}
</script>