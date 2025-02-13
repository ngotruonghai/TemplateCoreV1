<template>
    <v-container>
        <v-row>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>1. Nội dung <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                    </h3>
                </div>
                <div class="input-container">
                    <input type="text" v-model="_txtnoidung" class="FontDefault"
                        placeholder="VD: Cấu hình cho quy trình vận hành" />
                </div>
            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>2. Thiết lập mã phiếu (Ký tự đầu tiên, tối đa 4 ký tự) <div
                            style="display: inline-block; vertical-align: middle; color: red;">*</div>
                    </h3>
                </div>
                <div class="input-container">
                    <input type="text" v-model="_txtthietlapmaphieu" class="FontDefault"
                        placeholder="VD: QT, quy trình sẽ phát sinh là 'QT' + 'mã phát sinh tự động', mã phiếu tạo là OT00001" />
                </div>
            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>3. Phân quyền cho phòng ban nào được tạo mã phiếu của quy trình
                    </h3>
                </div>
                <CBCheckPhongBan @emit_phonganId="handlePhongBan" />

            </v-col>
            <v-col cols="12" md="12">
                <h4>4. Phân quyền cho nhân sự nào được tạo mã phiếu của quy trình</h4>
                <CBCheckNhanSu :ListPhongBanId="_phongbanId" @emit_nhansuId="handleNhanSu"></CBCheckNhanSu>

            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>5. Ghi chú</h3>
                </div>
                <div class="input-container">
                    <textarea class="form-control" placeholder="Nội dung ghi chú" v-model="_txtghichu"></textarea>
                </div>
            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>6. Tạo nội dung thông tin</h3>
                    <div style="display: inline-block; vertical-align: middle; color: red;">* Ở mỗi quy trình, sẽ hiển
                        thị theo số lương thông tin, để người dùng nhập vào</div>
                    <br>
                    <div style="display: inline-block; vertical-align: middle; color: red;">* Khi quy trình màn hình
                        trước bị chỉnh sửa thì sẽ phải cấu hình lại thông tin</div>
                </div>
                <table class="FontDefault">
                    <thead>
                        <tr>
                            <th width="50px">STT</th>
                            <th>Tên thông tin</th>
                            <th>Tình trạng</th>
                            <th width="100px">Cấu hình</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in _dsThongTin" :key="item.index ?? 0">
                            <td> {{ index + 1 }} </td>
                            <td>{{ item.tenThonTin }}</td>
                            <td></td>
                            <td style="text-align: center;">
                                <div class="tooltip">
                                    <span class="tooltip" @click="btnCauHinhThongTin(item.index)">
                                        <i class="fa fa-gear"></i>
                                    </span>
                                    <span class="tooltiptext">Cấu hình thông tin</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="4" style="text-align: center;cursor: default;">
                                <div @click="TaoThongTIn">
                                    <span class="tooltip">
                                        <i class="fa fa-plus"></i>
                                    </span>
                                    Tạo nội dung
                                </div>
                            </td>
                        </tr>
                        <!-- Add more rows as needed -->
                    </tbody>
                </table>
            </v-col>
        </v-row>
    </v-container>

    <div :class="['sliding-panel FontDefault', { open: isPanelOpen }]">
        <div class="sliding-panel-content">
            <v-container-fluid>
                <v-row>
                    <v-col cols="12" md="12">
                        <h3>Cấu hình thông tin: {{ _txttenthongtinDL }}</h3>
                    </v-col>

                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Tên thông tin
                        </label>
                        <input type="text" v-model="_txttenthongtinDL" class="FontDefault" placeholder=""
                            style="width: 100%;" />
                    </v-col>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Nôi dung
                        </label>
                        <textarea class="form-control" placeholder="Nội dung ghi chú"
                            v-model="_txtnoidungDL"></textarea>
                    </v-col>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Cấu hình hiển thị ở bước
                        </label>
                        <CBCheckDanhSachNode :LoadNode="_LoadNode" :DanhSachNode="_dsNode" @emit_dansachNode="handleDanhSachNode">
                        </CBCheckDanhSachNode>

                    </v-col>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Thông báo khi không nhập
                            <div></div>
                            <div style="display: inline-block; color: red;"> * Sử dụng khi chọn bắt buộc nhập</div>
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="" style="width: 100%;"
                            v-model="_thongBao" />
                    </v-col>
                    <v-col cols="12" md="6">
                        <label class="FontDefault">
                            Loại
                        </label>
                        <div class="combobox-container">
                            <select id="email" class="combobox" v-model="_loaiThongTin">
                                <option value="0">
                                    Nhập tự do
                                </option>
                                <option value="1">
                                    Nhập số
                                </option>
                            </select>
                            <span class="combobox-icon">▼</span>
                        </div>

                    </v-col>
                    <v-col cols="12" md="6">
                        <label class="FontDefault">
                            Kích thước ký tự
                        </label>
                        <input type="number" v-model="_txtkichthuockytuDL" class="FontDefault" placeholder="" style="width: 100%;" />
                    </v-col>


                    <v-col cols="12" md="12">
                        <v-checkbox label="Bắt buộc nhập." class="FontDefault" v-model="_isBatBuocnhap"></v-checkbox>
                        <v-checkbox label="Có File đính kèm." class="FontDefault" style="margin-top: -50px;"
                            v-model="_isFileDinhKem"></v-checkbox>
                    </v-col>

                    <!-- button xác nhận -->
                    <v-col cols="12" md="12">
                        <v-btn class="btnAdd btn no-uppercase" @click="btncauHinhXacNhan" style="float: right;">
                            Xác nhận
                        </v-btn>
                        <v-btn @click="btnXoaThongTin" class="btnCancel no-uppercase"
                            style="float: right; margin-right: 10px;">
                            Xóa
                        </v-btn>
                        <v-btn @click="btncauHinhXacNhan" class="btnCancel no-uppercase"
                            style="float: right; margin-right: 10px;">
                            Hủy
                        </v-btn>

                    </v-col>
                </v-row>

            </v-container-fluid>

        </div>
    </div>
    <div class="overlay" :class="{ show: isPanelOpen }"></div>
</template>

<script lang="ts" setup>
import type { IDataThongTin, IDanhSachNode } from "@/interface/QuyTrinh/IAddCauHinhQuyTrinh";

let isPanelOpen = ref(false);
let _phongbanId = ref<number[]>([]);
let _txtnoidung = ref("");
let _txtthietlapmaphieu = ref("");
let _txtghichu = ref("");
let _txttenthongtinDL = ref("");
let _txtnoidungDL = ref("");
const _selectedThongTinId = ref<number | null>();
let _selectThongTinCauHinhBuocDL = ref<string[] | null>([]);
let _loaiThongTin = ref<number>(0);
let _isBatBuocnhap = ref<boolean>(false);
let _isFileDinhKem = ref<boolean>(false);
let _thongBao = ref<string>("");
let _txtkichthuockytuDL = ref<string>("");
let _LoadNode = ref<string[]>([]);

const emit = defineEmits<{
    (event: 'emit_DanhSachCauHinh', data: IDataThongTin[],NhanSuId: string[],PhongBanId: number[]
    ): void;
}>();

const props = defineProps<{
    DanhSachNode: IDanhSachNode[],
}>();

const _dsThongTin = ref<IDataThongTin[]>([]);
const _dsNode = ref<IDanhSachNode[]>([]);

// Thêm dữ liệu mới
const addDataToArray = (data: IDataThongTin) => {
    _dsThongTin.value.push(data);
};

function TaoThongTIn() {
    addDataToArray({
        index: (_dsThongTin.value.length), tenThonTin: 'Chưa cấu hình', noiDung: "",
        thongBao: null,
        loaiThongTin: 0,
        kichThuocKyTu: 0,
        isBatBuocnhap: false,
        isFileDinhKem: false,
        danhSachCauHinhBuoc: []
    });
}

// btn xác nhận trong cấu hình
function btncauHinhXacNhan() {
    const index = _dsThongTin.value.findIndex(x => x.index == _selectedThongTinId.value);
    _dsThongTin.value[index].tenThonTin = _txttenthongtinDL.value;
    _dsThongTin.value[index].noiDung = _txtnoidungDL.value;
    _dsThongTin.value[index].danhSachCauHinhBuoc = _selectThongTinCauHinhBuocDL.value ?? [];
    _dsThongTin.value[index].loaiThongTin = parseInt(_loaiThongTin.value.toString());
    _dsThongTin.value[index].isBatBuocnhap = _isBatBuocnhap.value;
    _dsThongTin.value[index].isFileDinhKem = _isFileDinhKem.value;
    _dsThongTin.value[index].thongBao = _thongBao.value;
    _dsThongTin.value[index].kichThuocKyTu = parseInt(_txtkichthuockytuDL.value);

    isPanelOpen.value = false;
    _selectedThongTinId.value = null;
    _txttenthongtinDL.value = "";
    _txtnoidung.value = "";
    _selectThongTinCauHinhBuocDL.value = null;
}
// btn xóa trong cấu hình
function btnXoaThongTin() {
    _dsThongTin.value = _dsThongTin.value.filter(x => x.index != _selectedThongTinId.value);
    isPanelOpen.value = false;
    _selectedThongTinId.value = null;
}

// Cấu hình trong danh sách thông tin
function btnCauHinhThongTin(Id: number) {
    isPanelOpen.value = true;
    _selectedThongTinId.value = Id;

    const data_thongtin = _dsThongTin.value.find(x => x.index == Id);

    if (data_thongtin?.danhSachCauHinhBuoc.length == 0) {
        _txttenthongtinDL.value = "";
        _isBatBuocnhap.value = false;
        _loaiThongTin.value = 0;
        _isFileDinhKem.value = false;
        _thongBao.value = "";
        _txtnoidungDL.value="";
        _txtkichthuockytuDL.value = "0";
        _selectThongTinCauHinhBuocDL.value = [];
        _LoadNode.value = [];
    }
    else {
        _txttenthongtinDL.value = data_thongtin?.tenThonTin ?? "";
        _txtnoidungDL.value = data_thongtin?.noiDung ?? "";
        _isBatBuocnhap.value = data_thongtin?.isBatBuocnhap ?? false;
        _loaiThongTin.value = data_thongtin?.loaiThongTin ?? 0;
        _isFileDinhKem.value = data_thongtin?.isFileDinhKem ?? false;
        _thongBao.value = data_thongtin?.thongBao ?? "";
        _txtnoidung.value = data_thongtin?.noiDung??"";
        _txtkichthuockytuDL.value =  data_thongtin?.kichThuocKyTu.toString()??""
        _selectThongTinCauHinhBuocDL.value = data_thongtin?.danhSachCauHinhBuoc??[];
        _LoadNode.value = data_thongtin?.danhSachCauHinhBuoc??[];
    }


}

const handlePhongBan = (phongbanId: number[]) => {
    _phongbanId.value = phongbanId;
};
const handleNhanSu = (nhansuId: string[]) => {
    emit("emit_DanhSachCauHinh", _dsThongTin.value,nhansuId,_phongbanId.value);
};

const handleDanhSachNode = (DanhsachId: string[]) => {
    _selectThongTinCauHinhBuocDL.value = DanhsachId;
};

watch(() => props.DanhSachNode, (newVal, oldVal) => {
    _dsNode.value = newVal;
}, { deep: true });
</script>

<style>
table {
    width: 100%;
    border-collapse: collapse;
    margin-bottom: 20px;
}

th,
td {
    border: 1px solid #dddddd;
    text-align: left;
    padding: 8px;
}

th {
    background-color: #f2f2f2;
    color: #333333;
}

tr:nth-child(even) {
    background-color: #f9f9f9;
}

tr:hover {
    background-color: #e9e9e9;
}

.tooltip {
    position: relative;
    display: inline-block;
}

.tooltip .tooltiptext {
    visibility: hidden;
    width: 120px;
    background-color: rgb(121, 120, 120);
    color: #fff;
    text-align: center;
    border-radius: 6px;
    padding: 10px 0;
    margin-top: 20px;
    margin-left: -30px;

    /* Position the tooltip */
    position: absolute;
    z-index: 1;
}

.tooltip:hover .tooltiptext {
    visibility: visible;
}

.sliding-panel {
    position: fixed;
    top: 0;
    right: 0;
    height: 100%;
    width: 400px;
    background-color: #333;
    box-shadow: 15px 0 30px black;
    transform: translateX(100%);
    transition: transform 0.5s ease-in-out;
    background-color: white;
    z-index: 10;
}

.sliding-panel.open {
    transform: translateX(0);
    overflow: auto;

}

.sliding-panel .close-button {
    padding: 10px;
    background-color: white;
    border: none;
    color: white;
    cursor: pointer;
    font-size: 16px;
    width: 100%;
    text-align: center;
}

.sliding-panel-content {
    padding: 15px;
    margin-top: 60px;
}

.overlay {
    position: fixed;
    top: 0;
    left: 0;
    width: 100%;
    height: 100%;
    background-color: rgba(235, 235, 235, 0.5);
    z-index: 0;
    display: none;
}

.overlay.show {
    display: block;
}

.placeholder {
    color: #aaa;
}

/* CSS tùy chỉnh */
.multi-select-danhsachnode {
    width: 100%;
    position: relative;
    font-family: Arial, sans-serif;
}

.dropdown-header-danhsachnode {
    border: 1px solid #ccc;
    padding: 8px;
    border-radius: 5px;
    cursor: pointer;
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: #fff;
}

.arrow {
    font-size: 12px;
}

.placeholder {
    color: #aaa;
}

/* CSS tùy chỉnh */
.multi-select-danhsachNode {
    width: 100%;
    position: relative;
    font-family: Arial, sans-serif;
}

.dropdown-header-danhsachNode {
    border: 1px solid #ccc;
    padding: 8px;
    border-radius: 5px;
    cursor: pointer;
    display: flex;
    justify-content: space-between;
    align-items: center;
    background-color: #fff;
}

.arrow {
    font-size: 12px;
}
</style>