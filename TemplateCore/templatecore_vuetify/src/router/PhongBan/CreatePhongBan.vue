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
                                placeholder="VD: CSKH (không khoảng cách, không viết dấu)" />
                        </div>

                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12" md="12">
                        <div class="input-container">
                            <label for="" class="FontDefault">Tên phòng ban</label>
                            <input type="text" id="" class="FontDefault" placeholder="VD: Chăm sóc khách hàng" />
                        </div>

                    </v-col>
                </v-row>

                <v-row>
                    <v-col cols="12" md="12">
                        <div class="input-container">
                            <label for="" class="FontDefault">Ghi chú</label>
                            <textarea class="form-control" placeholder="Nội dung ghi chú"></textarea>
                        </div>

                    </v-col>
                </v-row>
                <div style="display: flex; justify-content: flex-end;">
                    <BtnDiaLogYesNo :IsError="isshow" -ten-button="Tạo tài khoản" -messgase="123" -title="Thông báo"
                        @data-sent="handleDataFromChild" @click="triggerValidation" />
                </div>
            </div>

        </v-container>
    </div>
</template>

<script lang="ts" setup>
let isshow = ref(false);
let message = ref("123");
const handleDataFromChild = (data: boolean) => {

};
const triggerValidation = () => {

};

</script>

<style>

</style>