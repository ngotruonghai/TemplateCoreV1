<template>
    <v-container>
        <v-row>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>1. Nội dung <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                    </h3>
                </div>
                <div class="input-container">
                    <input type="text" id="" class="FontDefault" placeholder="VD: Cấu hình hco quy trình vận hành" />
                </div>
            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>2. Thiết lập mã phiếu (Ký tự đầu tiên, tối đa 4 ký tự) <div
                            style="display: inline-block; vertical-align: middle; color: red;">*</div>
                    </h3>
                </div>
                <div class="input-container">
                    <input type="text" id="" class="FontDefault" placeholder="VD: Cấu hình hco quy trình vận hành" />
                </div>
            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>3. Phòng ban nhân sự
                    </h3>
                </div>
                <CBCheckPhongBan @emit_phonganId="handlePhongBan" />

            </v-col>
            <v-col cols="12" md="12">
                <CBCheckNhanSu :ListPhongBanId="_phongbanId" @emit_nhansuId="handleNhanSu"></CBCheckNhanSu>

            </v-col>

            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>4. Phân quyền cho nhân sự nào được tạo mã phiếu của quy trình
                    </h3>
                </div>
                <div class="input-container">
                    <input type="text" id="" class="FontDefault"
                        placeholder="Nhân sự sẽ được nhìn thấy tất cả mã thuộc quy trình này ở tất cả các bước" />
                </div>
            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>5. Phân quyền cho phòng nào được tạo mã phiếu của quy trình
                    </h3>
                </div>
                <div class="input-container">
                    <input type="text" id="" class="FontDefault"
                        placeholder="Nhân sự sẽ được nhìn thấy tất cả mã thuộc quy trình này ở tất cả các bước" />
                </div>
            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>6. Ghi chú</h3>
                </div>
                <div class="input-container">
                    <textarea class="form-control" placeholder="Nội dung ghi chú"></textarea>
                </div>
            </v-col>
            <v-col cols="12" md="12">
                <div class="FontDefault">
                    <h3>7. Tạo nội dung thông tin</h3>
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
                            <th width="100px">Cấu hình</th>
                        </tr>
                    </thead>
                    <tbody>
                        <tr v-for="(item, index) in _dsThongTin" :key="item.Id ?? 0">
                            <td> {{ index + 1 }} </td>
                            <td>{{ item.TenThongTin }}</td>
                            <td style="text-align: center;">
                                <div class="tooltip">
                                    <span class="tooltip" @click="btnCauHinhThongTin()">
                                        <i class="fa fa-gear"></i>
                                    </span>
                                    <span class="tooltiptext">Cấu hình thông tin</span>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3" style="text-align: center;cursor: default;">
                                <div @click="TaoQuyTrinh">
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
                        <h3>Cấu hình thông tin: số 1</h3>
                    </v-col>

                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Tên thông tin
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="" style="width: 100%;" />
                    </v-col>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Nôi dung
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="" style="width: 100%;" />
                    </v-col>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Cấu hình hiển thị ở bước
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="" style="width: 100%;" />
                    </v-col>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Cấu hình hiển thị cho nhân sự
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="" style="width: 100%;" />
                    </v-col>


                    <v-col cols="12" md="12">
                        <v-checkbox label="Bắt buộc nhập." class="FontDefault"></v-checkbox>
                        <v-checkbox label="Có File đính kèm." class="FontDefault"
                            style="margin-top: -50px;"></v-checkbox>
                    </v-col>

                    <!-- button xác nhận -->
                    <v-col cols="12" md="12">
                        <v-btn class="btnAdd btn no-uppercase" @click="btncauHinhXacNhan(true)" style="float: right;">
                            Xác nhận
                        </v-btn>
                        <v-btn @click="btncauHinhXacNhan(false)" class="btnCancel no-uppercase"
                            style="float: right; margin-right: 10px;">
                            Xóa
                        </v-btn>
                        <v-btn @click="btncauHinhXacNhan(false)" class="btnCancel no-uppercase"
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

let isPanelOpen = ref(false);
let _phongbanId = ref([""]);


interface IDataThongTin {
    Id: number | null;
    TenThongTin: string | null;
}


const _dsThongTin = ref<IDataThongTin[]>([]);

// Thêm dữ liệu mới
const addDataToArray = (data: IDataThongTin) => {
    _dsThongTin.value.push(data);
};

function TaoQuyTrinh() {
    addDataToArray({ Id: 1, TenThongTin: 'Thông tin 1' });
}

function btncauHinhXacNhan(status: boolean) {
    isPanelOpen.value = false;
}

function btnCauHinhThongTin() {
    isPanelOpen.value = true;
}

const handlePhongBan = (phongbanId: string[]) => {
     _phongbanId.value = phongbanId;
    //_phongbanId.value.push("123");
};
const handleNhanSu = (nhansuId: string[]) => {

};

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
    padding: 20px;
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
</style>