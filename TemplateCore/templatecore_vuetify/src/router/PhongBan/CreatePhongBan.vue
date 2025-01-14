<template>
    <div class="ChillColor">
        <v-container>
            <div class="layoutChill">
                <v-row>
                    <v-col cols="12" md="12">
                        <div class="input-container">
                            <label for="" class="FontDefault">
                                <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                Mã phòng ban
                            </label>
                            <input type="text" id="" class="FontDefault"
                                placeholder="VD: CSKH (không khoảng cách, không viết dấu)" v-model="maphongban" />
                        </div>

                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12" md="12">
                        <div class="input-container">
                            <label for="" class="FontDefault">Tên phòng ban</label>
                            <input type="text" id="" class="FontDefault" placeholder="VD: Chăm sóc khách hàng"
                                v-model="tenphongban" />
                        </div>

                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12" md="12">
                        <div class="input-container">
                            <label for="" class="FontDefault">Ghi chú</label>
                            <textarea class="form-control" placeholder="Nội dung ghi chú" v-model="ghichu"></textarea>
                        </div>

                    </v-col>
                </v-row>
                <div style="display: flex; justify-content: flex-end;">
                    <BtnDiaLogYesNo :IsError="isshow" -ten-button="Tạo tài khoản" :Messgase="message" -title="Thông báo"
                        @data-sent="handleDataFromChild" @click="triggerValidation" />
                </div>
            </div>

        </v-container>
        <v-snackbar v-model="alert.visible" :timeout="2000" :color="alert.color">
            <v-container>
                <v-row>
                    <v-col cols="1">
                        <v-icon color="white">{{ alert.icon }}</v-icon>
                    </v-col>
                    <v-col cols="11" style="padding-top: 14px;">{{ alert.text }}</v-col>
                </v-row>
            </v-container>
        </v-snackbar>
    </div>
</template>

<script lang="ts" setup>
import { callAuthenticationAPI } from "@/providers/data-provider";

let alert = ref({
    visible: false, // Trạng thái hiển thị snackbar
    color: "#CD3333", // Màu snackbar khi lỗi
    icon: "mdi-cancel", // Icon thông báo lỗi
    title: "Login Failed", // Tiêu đề thông báo (không dùng trong ví dụ này)
    text: "Invalid username or password. Please try again.", // Nội dung thông báo
});
let isshow = ref(true);
let message = ref("Bạn có muốn tạo phòng ban này không?");
let tenphongban = ref("");
let ghichu = ref("");
const rawInput = ref<string>('');

let maphongban = computed({
    get: () => rawInput.value,
    set: (value: string) => {
        // Chỉ cho phép chữ cái và số (a-z, A-Z, 0-9)
        rawInput.value = value.replace(/[^a-zA-Z0-9]/g, '');
    }
});

const handleDataFromChild = (data: boolean) => {
    if (data == true) {
        AddPhongBan();
    }
};
const triggerValidation = () => {
    if (maphongban.value === "") {

        message.value = 'Vui lòng nhập "MÃ PHÒNG BAN"!';
        isshow.value = false;
    }
};

async function AddPhongBan() {
    try {
        let responseData = await callAuthenticationAPI('/api/quanlythongtin/PhongBan/AddPhongBan', 'POST', {
            maPhongBan: maphongban.value,
            tenPhongBan: tenphongban.value,
            ghiChu: ghichu.value
        }, {
            timeout: 15000
        });

        maphongban.value = "";
        tenphongban.value = "";
        ghichu.value="";


        alert.value.text = "Tạo thành công";
        alert.value.visible = true;
        alert.value.color = "#109011";
    } catch (error) {
        alert.value.text = error as string;
        alert.value.visible = true;
        alert.value.color = "#CD3333";
    }
}

</script>

<style></style>