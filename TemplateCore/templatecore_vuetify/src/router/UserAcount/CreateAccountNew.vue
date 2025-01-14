<template>
    <div class="ChillColor">
        <v-container>
            <v-row>
                <v-col cols="12" md="6">
                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">Mã nhân viên</label>
                                <input type="text" id="" class="FontDefault"
                                    placeholder="Mã nhân viên được tạo tự động khi xác nhận tạo thành công" readonly />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Tài khoản (user đăng nhập không dấu không khoảng cách)
                                </label>
                                <input type="text" id="myInputId" class="FontDefault" placeholder="VD: nguyenvantuan"
                                    v-model="taikhoan" ref="taiKhoanRef">
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    Mật khẩu (mặc định)
                                    <div style="display: inline-block; vertical-align: middle; color: red;">Người dụng
                                        tự đổi mật khẩu khi vào phần mềm</div>
                                </label>
                                <input type="text" id="myInputId" class="FontDefault" placeholder=""
                                    value="123Pa$$word!" ref="taiKhoanRef" readonly />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">Quyền</label>
                                <CbPhanQuyen :RoleId="selectedRoleId" @update:RoleId="updateRoleId" />
                            </div>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">Phòng ban</label>
                                <CBPhongBan :Id="selectedRoleId" @update:Id="GetIdPhongBan" />
                            </div>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">Email</label>
                                <input type="email" id="" class="FontDefault" v-model="email"
                                    placeholder="VD: NguyenVanAn@gmail.com" />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Họ
                                </label>
                                <input type="text" id="" class="FontDefault" v-model="ho" placeholder="VD: Nguyễn" />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Tên
                                </label>
                                <input type="text" id="" class="FontDefault" ref="tenRef" v-model="ten"
                                    placeholder="VD: Văn An" />
                            </div>
                        </v-col>
                    </v-row>

                    <v-row>
                        <v-col cols="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">Số điện thoại</label>
                                <input type="text" id="" class="FontDefault" placeholder="VD: 0968103653"
                                    v-model="phoneNumber" />
                            </div>
                        </v-col>
                    </v-row>
                </v-col>
                <v-col cols="12" style="text-align: center; margin-top: 50px;" md="6">
                    <v-avatar size="150" class="me-4">
                        <img src="https://via.placeholder.com/150" alt="User Avatar" />
                    </v-avatar>
                </v-col>
            </v-row>

            <div style="float: right;">
                <BtnDiaLogYesNo :IsError="isshow" -ten-button="Tạo tài khoản" :Messgase="message" -title="Thông báo"
                    @data-sent="handleDataFromChild" @click="triggerValidation" />
            </div>
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
        </v-container>
    </div>
</template>

<script lang="ts" setup>
import { callAuthenticationAPI } from "@/providers/data-provider";
import { ref, nextTick } from "vue";

let alert = ref({
    visible: false, // Trạng thái hiển thị snackbar
    color: "#CD3333", // Màu snackbar khi lỗi
    icon: "mdi-cancel", // Icon thông báo lỗi
    title: "Login Failed", // Tiêu đề thông báo (không dùng trong ví dụ này)
    text: "Invalid username or password. Please try again.", // Nội dung thông báo
});

//let taikhoan = ref<string>("");
let ten = ref<string>("");
let ho = ref<string>("");
let email = ref<string>("");
let selectedRoleId = ref<string>("");
let selectPhongBanId = ref<string>("");
let isshow = ref(false);
let message = ref('');
let phoneNumber = ref('');
const rawInput = ref<string>('');

interface requestData {
    roleId: string | null,
    firstName: string | null,
    lastName: string | null,
    email: string | null,
    userName: string | null,
    phoneNumber: string | null,
}
const taikhoan = computed({
    get: () => rawInput.value,
    set: (value: string) => {
        // Chỉ cho phép chữ cái và số (a-z, A-Z, 0-9)
        rawInput.value = value.replace(/[^a-zA-Z0-9]/g, '');
    }
});

const childData = ref<boolean>(false);
// Hàm xử lý dữ liệu từ component con
const handleDataFromChild = (data: boolean) => {
    childData.value = data;
    if (childData.value == true) {
        CreateUserDataAPI();
    }
};
// Hàm xử lý sự kiện update:RoleId từ component con
function updateRoleId(newRoleId: string) {
    selectedRoleId.value = newRoleId;
}
function GetIdPhongBan(NewId: string) {
    selectPhongBanId.value = NewId;
}
// Hàm kiểm tra trước khi hiển thị dialog
const triggerValidation = () => {

    if (taikhoan.value === "") {

        message.value = 'Vui lòng nhập "TÀI KHOẢN"!';
        isshow.value = false;
    }
    else if (ho.value === "") {

        message.value = 'Vui lòng nhập "HỌ"!';
        isshow.value = false;
    }
    else if (ten.value === "") {

        message.value = 'Vui lòng nhập "TÊN"!';
        isshow.value = false;
    }
    else {
        message.value = 'Bạn có muốn tạo tài khoản không?'
        isshow.value = true;
    }


};

async function CreateUserDataAPI() {
    try {
        let responseData = await callAuthenticationAPI('/api/account/CreateAcount', 'POST', {
            roleId: selectedRoleId.value ?? "",
            firstName: ho.value ?? "",
            lastName: ten.value ?? "",
            email: email.value ?? "",
            userName: taikhoan.value ?? "",
            phoneNumber: phoneNumber.value ?? "",
            phongBanId: selectPhongBanId.value
        }, {
            timeout: 15000
        });
        alert.value.text = "Tạo thành công";
        alert.value.visible = true;
        alert.value.color = "#109011";
    } catch (error) {
        alert.value.text = error as string;
        alert.value.visible = true;
        alert.value.color = "#CD3333";
    }
};
</script>

<style>
/* Tuỳ chỉnh CSS */
</style>