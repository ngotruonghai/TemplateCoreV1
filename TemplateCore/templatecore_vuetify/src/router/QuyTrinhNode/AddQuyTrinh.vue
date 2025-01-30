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
                                <input type="text" v-model="tenquytrinh" class="FontDefault" placeholder="VD: Quy trình duyệt đơn" />
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
                    </v-row>
                </div>

                <div v-show="flang == 1">

                    <v-row>
                        <v-col cols="12" md="12">
                            <label for="" class="FontDefault" style="font-size: 20px; color: #0099FF; font-weight: bold;">
                                {{ title }}
                            </label>
                        </v-col>
                        <AddCauHinhQuyTrinh></AddCauHinhQuyTrinh>
                    </v-row>

                </div>

                <v-row>
                    <v-col cols="12" md="6">
                        <button class="btnAdd btn" v-on:click="btnTraVe" style="float: left;" v-show="flang">
                            <span class="icon-border">
                                <i class="fas fa-arrow-left"></i>
                            </span> Trả về trước
                        </button>
                    </v-col>
                    <v-col cols="12" md="6" v-show="btnHide == false">
                        <button class="btnAdd btn" v-on:click="btnTiepThep(true)" style="float: right;">
                            Bươc tiếp theo s
                            <span class="icon-border">
                                <i class="fas fa-arrow-right"></i>
                            </span>
                        </button>
                    </v-col>
                    <v-col cols="12" md="6" v-show="btnHide == true">
                        <button class="btnAdd btn" style="float: right;">
                            Tạo quy trình  <span class="icon-border">
                                <i class="fa fa-plus"></i>
                            </span>
                        </button>
                    </v-col>
                </v-row>
            </div>
            <div v-show="flang == 0">

                <AddNode  @emit_Node='handleDataSent'></AddNode>

            </div>
        </v-container>

    </div>
</template>

<script lang="ts" setup>
import { callAuthenticationAPI } from "@/providers/data-provider";
let flang = ref(0);
let title= ref("");
let btnHide = ref(false);
let tenquytrinh = "";


interface INodeMap {
    keyId: string | null,
    type: number,
    index: number,
    tenNode: String | null
    x: number,
    y: number
}
interface IDiagram {
    keyId: string | null,
    source: string | null,
    target: string | null
    tenDiagram: string | null
}
interface INextStep {
    nodeIdStart: string | null,
    nodeIdEnd: string | null,
    diagramId: string | null,
    actionName: string | null, // Tên Diagram
    action: number,
    typeNextStep: number
}

interface IDanhSachQuyTrinh {
    ThietLapMaPhieu: string | null;
    TenQuyTrinh: string | null;
    GhiChu: string | null;
    NgayBatDau: string | null;
    NoiDung: string | null;
}

function btnTiepThep(status : boolean) {
    flang.value = flang.value + 1
    LoadTitle();
    btnHide.value = true;
    API_AddQuyTrinh();
}

function btnTraVe() {
    flang.value = flang.value - 1
    LoadTitle();
    btnHide.value = false;
}

function LoadTitle(){
    if(flang.value == 0){
        title.value = "Cấu hình thông tin quy trình"
    }
    else if(flang.value ==  1){
         title.value = "Cấu hình thiết kế quy trình"
    }
}

const INodeMaps = ref<INodeMap[]>([]);
const IDiagrams = ref<IDiagram[]>([]);
const INextSteps = ref<INextStep[]>([]);

// Hàm xử lý sự kiện nhận dữ liệu
function handleDataSent(data: { INodeMap: INodeMap[], IDiagram: IDiagram[], INextStep: INextStep[]}) {
  INodeMaps.value = data.INodeMap; // Cập nhật dữ liệu Nodes
  IDiagrams.value = data.IDiagram; // Cập nhật dữ liệu Diagrams
  INextSteps.value = data.INextStep;
}
async function API_AddQuyTrinh() {
    try {
        let responseData = await callAuthenticationAPI('/api/quanlythongtin/QuyTrinhNode/AddQuyTrinhNode', 'POST', {
            tenQuyTrinh: tenquytrinh,
            userName: localStorage.getItem("UserName"),
            nodeMapModels: INodeMaps.value,
            diagramNodeModels: IDiagrams.value,
            nextStepNodeModels:INextSteps.value
        }, {
            timeout: 15000
        });
    } catch (error) {
       
    }
}
onMounted(() => {
    LoadTitle();
});
</script>