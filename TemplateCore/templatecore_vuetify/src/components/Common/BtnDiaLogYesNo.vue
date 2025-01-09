<template>
    <div class="text-center pa-4">
        <!-- Dialog -->
        <v-dialog v-model="dialog" max-width="400" persistent>
            <template v-slot:activator="{ props: activatorProps }">
                <v-btn @click="handleButtonClick" class="btnAdd btn">
                    <div class="no-uppercase">
                        {{ TenButton }}
                    </div>
                </v-btn>
            </template>

            <v-card :text="props.Messgase" :title="props.Title">
                <template v-slot:actions>
                    <v-spacer></v-spacer>

                    <v-btn @click="btnClickDiaLog(false)" class="btnCancel no-uppercase" v-if="iserror">
                        Từ chối
                    </v-btn>

                    <v-btn @click="btnClickDiaLog(iserror)" class="btnAdd btn no-uppercase">
                        Xác nhận
                    </v-btn>
                </template>
            </v-card>
        </v-dialog>
    </div>
</template>

<script lang="ts" setup>
import { ref, watch, nextTick } from 'vue';

// Khai báo sự kiện
const emit = defineEmits<{
  (event: 'data-sent', data: boolean): void;
}>();

const props = defineProps<{
  Messgase: string;
  Title: string;
  TenButton: string;
  IsError: boolean;
}>();

let dialog = ref(false);
let iserror = ref(props.IsError);

// Hàm kiểm tra điều kiện trước khi mở popup
function handleButtonClick() {
  dialog.value = true;
}

function btnClickDiaLog(status: boolean) {
  dialog.value = false;
  nextTick(() => {
    emit('data-sent', status); // Gửi sự kiện sau khi DOM được cập nhật
  });
}

// Theo dõi props.IsShow
watch(
  () => props.IsError,
  (newValue) => {
    iserror.value = newValue;
  }
);
</script>


<style>
.no-uppercase {
    text-transform: none;
    /* Không viết hoa chữ */
}
</style>