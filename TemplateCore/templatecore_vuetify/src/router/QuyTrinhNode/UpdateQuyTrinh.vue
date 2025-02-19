<template>
    <div class="ChillColor">
        <v-container>
            <div class="layoutChill">
                <div v-show="flang == 0">
                    <v-row>
                        <v-col cols="12" md="6">
                            <label for="" class="FontDefault"
                                style="font-size: 20px; color: #0099FF; font-weight: bold;">
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
                                <input type="text" v-model="_txttenquytrinh" class="FontDefault"
                                    placeholder="VD: Quy trình duyệt đơn" />
                            </div>
                        </v-col>
                        <v-col cols="12" md="6">
                            <div class="input-container">
                                <label for="" class="FontDefault">
                                    <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                                    Ngày bắt đầu
                                </label>
                                <!-- <input type="text" id="" class="FontDefault" placeholder="VD: Quy trình duyệt đơn" /> -->
                                <Datetimepicker :datetime="_datetime" @emit_datetime="handDatTime"></Datetimepicker>
                            </div>
                        </v-col>
                    </v-row>
                </div>

                <div v-show="flang == 1">

                    <v-row>
                        <v-col cols="12" md="12">
                            <label for="" class="FontDefault"
                                style="font-size: 20px; color: #0099FF; font-weight: bold;">
                                {{ title }}
                            </label>
                        </v-col>
                        <UpdateCauHinhQuyTrinh :ThietLapMaPhieu="_txtthietlapmaphieu" 
                        :NoiDung="_txtnoidung" 
                        :DanhSachNode="_dsNode"
                        :GhiChu="_txtghichu"
                        @emit_DanhSachCauHinh="handleDanhSachCauHinh">
                        </UpdateCauHinhQuyTrinh>
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
                        <button class="btnAdd btn" style="float: right;" @click="ClickAddQuyTrinh()">
                            Tạo quy trình <span class="icon-border">
                                <i class="fa fa-plus"></i>
                            </span>
                        </button>
                    </v-col>
                </v-row>
            </div>
            <div v-show="flang == 0">

                <UpdateNode @emit_Node='handleDataSent' :INodeMap="INodeMaps" :IDiagram="IDiagrams"
                    :INextStep="INextSteps"
                    :InodeSttings = "InodeSttings"></UpdateNode>

            </div>
        </v-container>

    </div>
</template>

<script lang="ts" setup>
import { callApi, callAuthenticationAPI, convertToDate } from '@/providers/data-provider';
import type { ApiResponse, NodeMapModel, QuyTrinhData,NextStepNodeModels} from "@/interface/QuyTrinh/IDanhSachQuyTrinhC2";
import type { IDataThongTin } from '@/interface/QuyTrinh/IAddCauHinhQuyTrinh';
import type { WorkflowData } from '@/interface/QuyTrinh/IDanhSachQuyTrinhById';

let flang = ref(0);
let title = ref("");
let btnHide = ref(false);
let _txttenquytrinh = ref("");
let _nhansuId = ref<string[]>([]);
let _phongbanId = ref<number[]>([]);
let _txtnoidung = ref("");
let _txtthietlapmaphieu = ref("");
let _txtghichu = ref("");
let _datetime = ref<Date>(new Date());
const props = defineProps<{
    id: string;
}>();


interface INodeMap {
    keyId: string | null,
    type: number,
    index: number,
    tenNode: string | null
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
    _txttenquytrinh: string | null;
    GhiChu: string | null;
    NgayBatDau: string | null;
    NoiDung: string | null;
}
interface InodeSttings {
    keyNode: string,
    ghiChu: string | null,
    isTaoTaskBaoCao: boolean,
    isGuiMailPhongBanTiepNhan: boolean,
    isGuiMailNhacNho: boolean,
    nhanSuNodeModels: string[],
    phongBanNodeModels: number[],
    cauHinhMailNhacNho: number
}

const INodeMaps = ref<INodeMap[]>([]);
const IDiagrams = ref<IDiagram[]>([]);
const INextSteps = ref<INextStep[]>([]);
const InodeSttings = ref<InodeSttings[]>([]);
const _dsNode = ref<NodeMapModel[]>([]);
const _dsThongTin = ref<IDataThongTin[]>([]);
let response = ref<QuyTrinhData>();

function btnTiepThep(status: boolean) {
    flang.value = flang.value + 1
    LoadTitle();
    btnHide.value = true;
    // API_AddQuyTrinh();
}

function btnTraVe() {
    flang.value = flang.value - 1
    LoadTitle();
    btnHide.value = false;
}

function LoadTitle() {
    if (flang.value == 0) {
        title.value = "Cấu hình thông tin quy trình"
    }
    else if (flang.value == 1) {
        title.value = "Cấu hình thiết kế quy trình"
    }
}

// Hàm xử lý sự kiện nhận dữ liệu
function handleDataSent(data: {
    INodeMap: INodeMap[], IDiagram: IDiagram[], INextStep: INextStep[],
    InodeSttings: InodeSttings[]
}) {
    INodeMaps.value = data.INodeMap;
    IDiagrams.value = data.IDiagram;
    INextSteps.value = data.INextStep;
    InodeSttings.value = data.InodeSttings;
    INextSteps.value = data.INextStep;
    _dsNode.value = data.INodeMap
}
async function API_AddQuyTrinh() {
    return;
    try {
        let responseData = await callAuthenticationAPI('/api/quanlythongtin/QuyTrinhNode/AddQuyTrinhNode', 'POST', {
            _txttenquytrinh: _txttenquytrinh,
            userName: localStorage.getItem("UserName"),
            nodeMapModels: INodeMaps.value,
            diagramNodeModels: IDiagrams.value,
            nextStepNodeModels: INextSteps.value,
            nhanSuIds: _nhansuId.value,
            phongBanIds: _phongbanId.value,
            ghiChu: _txtghichu,
            thietLapMaPhieu: _txtthietlapmaphieu,
            noiDung: _txtnoidung,
            ngayBatDau: _datetime,
            nodeSttings: InodeSttings.value,
            cauHinhThongTins: _dsThongTin.value
        }, {
            timeout: 15000
        });
    } catch (error) {

    }
}

async function LoadDanhSachQuyTrinh() {
    try {
        const result = await callAuthenticationAPI('/api/quanlythongtin/QuyTrinhNode/GetQuyTrinhById_C2?quytrinhId=' + props.id, 'GET', {}, { timeout: 15000 });
        const data = typeof result === "string" ? JSON.parse(result) : result;
        response.value = data.data ? (data.data as QuyTrinhData) : (data as QuyTrinhData);

    } catch (error) {
        console.log(error)
    }
}

function ClickAddQuyTrinh() {
    API_AddQuyTrinh();
}

const handleDanhSachCauHinh = (data: IDataThongTin[], NhanSuId: string[], PhongBanId: number[],NoiDung:string,ThietLapMaPhieu: string,GhiChu:string) => {
    _dsThongTin.value = data;
    _phongbanId.value = PhongBanId;
    _nhansuId.value = NhanSuId;
    _txtnoidung.value = NoiDung
    _txtthietlapmaphieu.value = ThietLapMaPhieu;
    _txtghichu.value = GhiChu;
};

const handDatTime = (datetime: Date) => {
    _datetime.value = datetime
}

function LoadDataDanhSach() {
    _txttenquytrinh.value = response.value?.tenQuyTrinh ?? "";
    INodeMaps.value = response.value?.nodeMapModels ?? []
    IDiagrams.value = response.value?.diagramNodeModels ?? [];
    INextSteps.value = response.value?.nextStepNodeModels ?? [];
    InodeSttings.value = response.value?.nodeSttings??[];
    _datetime.value = response.value?.ngayBatDau??new Date;
    _txtnoidung.value = response.value?.noiDung??"";
    _txtthietlapmaphieu.value = response.value?.thietLapMaPhieu??"";
    _txtghichu.value = response.value?.ghiChu??"";
    _nhansuId.value = response.value?.nhanSuIds??[];
    _phongbanId.value = response.value?.phongBanIds??[];
    _dsThongTin.value = response.value?.cauHinhThongTins??[];
  

    // console.log("----------------------------------");
    // console.log(INextSteps.value);
    // console.log("----------------------------------");
}
onMounted(async () => {
    await LoadDanhSachQuyTrinh();
    LoadTitle();
    LoadDataDanhSach();
});
</script>

<style></style>