<template>
    <div class="ChillColor">
        <v-container>
            <div class="layoutChill">
                <div v-show="flang == 0">
                    <v-row>
                        <v-col cols="12" md="6">
                            <label for="" class="FontDefault" style="font-size: 20px; color: #0099FF; font-weight: bold;">
                                {{ title }}
                            </label>
                        </v-col>
                        <v-col cols="12" md="6">
                            <label for="" class="FontDefault" style="float: right;">
                                <i class="fa-solid fa-book"></i> Hướng dẫn
                            </label>
                        </v-col>
                        <v-col cols="12" md="6">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Tên quy trình
                                </label>
                                <input type="text" id="" class="FontDefault" placeholder="VD: Quy trình duyệt đơn" />
                            </div>
                        </v-col>
                        <v-col cols="12" md="6">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Ngày bắt đầu
                                </label>
                                <!-- <input type="text" id="" class="FontDefault" placeholder="VD: Quy trình duyệt đơn" /> -->
                                <Datetimepicker></Datetimepicker>
                            </div>
                        </v-col>
                        <v-col cols="12" md="12">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Ghi chú
                                </label>
                                <textarea class="form-control" placeholder="Nội dung ghi chú"></textarea>
                            </div>
                        </v-col>
                    </v-row>
                </div>

                <div v-show="flang == 1">

                    <v-row>
                        <v-col cols="12" md="12">
                            <label for="" class="FontDefault" style="font-size: 20px; color: #0099FF; font-weight: bold;">
                                {{ title }}
                            </label>
                        </v-col>
                        <v-col cols="12" md="6">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Tên quy trình
                                </label>
                                <input type="text" id="" class="FontDefault" placeholder="VD: Quy trình duyệt đơn" />
                            </div>
                        </v-col>
                        <v-col cols="12" md="6">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Ngày bắt đầu
                                </label>
                                <input type="text" id="" class="FontDefault" placeholder="VD: Quy trình duyệt đơn" />
                            </div>
                        </v-col>
                        <v-col cols="12" md="6">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Ghi chú
                                </label>
                                <input type="text" id="" class="FontDefault" placeholder="VD: Quy trình duyệt đơn" />
                            </div>
                        </v-col>
                    </v-row>

                </div>

                <div v-show="flang == 2">
                    test 2
                </div>

                <v-row>
                    <v-col cols="12" md="6">
                        <button class="btnAdd btn" v-on:click="btnTraVe" style="float: left;" v-show="flang">
                            <span class="icon-border">
                                <i class="fas fa-arrow-left"></i>
                            </span> Trả về trước
                        </button>
                    </v-col>
                    <v-col cols="12" md="6">
                        <button class="btnAdd btn" v-on:click="btnTiepThep" style="float: right;">
                            Bươc tiếp theo
                            <span class="icon-border">
                                <i class="fas fa-arrow-right"></i>
                            </span>
                        </button>
                    </v-col>
                </v-row>
            </div>
            <div v-show="flang == 0">

                <AddNode></AddNode>

            </div>
        </v-container>

    </div>
</template>

<script lang="ts" setup>


let flang = ref(0);
let title= ref("");

function btnTiepThep() {
    flang.value = flang.value + 1
    LoadTitle();
}

function btnTraVe() {
    flang.value = flang.value - 1
    LoadTitle();
}

function LoadTitle(){
    if(flang.value == 0){
        title.value = "Cấu hình thông tin quy trình"
    }
    else if(flang.value ==  1){
         title.value = "Cấu hình thiết kế quy trình"
    }
}

onMounted(() => {
    LoadTitle();
});
</script>