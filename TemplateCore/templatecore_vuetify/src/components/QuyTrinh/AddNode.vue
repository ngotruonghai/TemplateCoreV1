<template>
    <div id="bieudoquytrinh">
        <div ref="paperContainer" class="diagram-container" @contextmenu.prevent="showContextMenu"></div>

    </div>
    <div v-if="isContextMenuVisible" :style="contextMenuStyle" class="context-menu FontDefault">
        <ul>
            <li @click="btnAddNode('bauduc_batdau')" style=" color: #87CEFA;">
                <span class="icon-border2" style=" color: #87CEFA;">
                    <i class="fas fa-circle"></i>
                </span>
                Tạo bước bắt đầu
            </li>
            <li @click="btnAddNode('vuong_buoc')" style=" color: #0099CC;">
                <span class="icon-border2" style=" color: #0099CC;">
                    <i class="fas fa-square"></i>
                </span>
                Tạo bước tiếp theo
            </li>
            <li @click="btnAddNode('thoi_dieukien')" style=" color: #FF9933;">
                <span class="icon-border2" style=" color: #FF9933;">
                    <i class="fas fa-exclamation-triangle"></i>
                </span>
                Tạo bước điều kiện
            </li>
            <li @click="btnAddNode('thoi_trave')" style=" color: #FF9933;">
                <span class="icon-border2" style=" color: #FF9933;">
                    <i class="fas fa-undo-alt"></i>
                </span>
                Trả về hoặc chuyển bước
            </li>
            <li @click="btnAddNode('thoi_ketthuc')" style=" color: #009966;">
                <span class="icon-border2" style=" color: #009966;">
                    <i class="fas fa-check"></i>
                </span>
                Duyệt
            </li>
            <li @click="btnAddNode('thoi_tuchoi')" style=" color: #CC0033;">
                <span class="icon-border2" style=" color: #CC0033;">
                    <i class="fas fa-times"></i>
                </span>
                Từ chối
            </li>
            <li @click="CauHinhBuoc">
                <span class="icon-border2">
                    <i class="fa fa-gear"></i>
                </span>
                Cấu hình bước
            </li>
            <li @click="deleteNode"> <span class="icon-border2">
                    <i class="fas fa-trash-alt"></i>
                </span> Xóa</li>
            <li @click="hideContextMenu">Hủy</li>
        </ul>
    </div>
    <v-dialog v-model="dialog" max-width="400" persistent>
        <template v-slot:activator="{ props: activatorProps }">
        </template>

        <v-card title="Tạo bước quy trình">
            <v-container>
                <v-row>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            <div style="display: inline-block; vertical-align: middle; color: red;">*</div>
                            Tên bước quy trình
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="VD: Bước duyệt quy trình"
                            style="width: 100%;" v-model="tenNode" />
                    </v-col>

                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Tên luồng chuyền (mũi tên)
                        </label>
                        <input type="text" id="" class="FontDefault"
                            placeholder="VD: Chuyển quy trình bước 1 sang bước 2" style="width: 100%;"
                            v-model="_ActionName" />
                    </v-col>
                    <v-col cols="12" md="12" v-show="_isShowListNode">
                        <label class="FontDefault">
                            Trả về hoặc chuyển cho bước:
                        </label>
                        <div class="combobox-container">
                            <select class="combobox" v-model="_selectNodeId">
                                <option value="" key="0"></option>
                                <option v-for="(item, index) in request.INodeMap" :key="item.KeyId || ''"
                                    :value="item.KeyId || ''">
                                    {{ item.TenNode }}
                                </option>
                            </select>
                            <span class="combobox-icon">▼</span>
                        </div>
                    </v-col>
                </v-row>

            </v-container>

            <template v-slot:actions>
                <v-spacer></v-spacer>
                <v-btn @click="btnXacNhanDialog(false)" class="btnCancel no-uppercase">
                    Hủy
                </v-btn>
                <v-btn class="btnAdd btn no-uppercase" @click="btnXacNhanDialog(true)">
                    Xác nhận
                </v-btn>

            </template>
        </v-card>
    </v-dialog>

    <v-dialog v-model="_isShoeMessError" max-width="400" persistent>
        <template v-slot:activator="{ props: activatorProps }">
        </template>

        <v-card title="Thông báo lỗi">
            <v-container>
                <v-row>
                    <v-col cols="12" md="12">
                        <label class="FontDefault" style="font-size: 15px;">
                            {{ _meesageError }} !
                        </label>
                    </v-col>
                </v-row>

            </v-container>

            <template v-slot:actions>
                <v-spacer></v-spacer>
                <v-btn class="btnAdd btn no-uppercase" @click=" _isShoeMessError = false">
                    Xác nhận
                </v-btn>

            </template>
        </v-card>
    </v-dialog>

    <div :class="['sliding-panel FontDefault', { open: isPanelOpen }]">
        <div class="sliding-panel-content">
            <v-container-fluid>
                <v-row>
                    <v-col cols="12" md="12">
                        <h3>Cấu hình bước: ...</h3>
                    </v-col>

                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Người tiếp nhận
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="" style="width: 100%;" />
                    </v-col>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Phòng ban tiếp nhận
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="" style="width: 100%;" />
                    </v-col>

                    <v-col cols="12" md="12">
                        <div class="input-container">
                            <label for="" class="FontDefault">
                                Ghi chú
                            </label>
                            <textarea class="form-control" placeholder="Nội dung ghi chú"></textarea>
                        </div>
                    </v-col>

                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Cấu hình mail nhắc nhở
                        </label>
                        <input type="number" id="" class="FontDefault"
                            placeholder="Số mỗi lần gửi mail cách nhau bao nhiêu phút" style="width: 100%;" min="0"
                            max="1440" />
                    </v-col>


                    <v-col cols="12" md="12">
                        <v-checkbox label="Tạo báo cáo tự động khi hoàn thành." class="FontDefault"></v-checkbox>
                        <v-checkbox label="Gửi mail cho người/phòng ban tiếp nhận." class="FontDefault"
                            style="margin-top: -50px;"></v-checkbox>
                        <v-checkbox label="Gửi mail nhắc nhở nếu có (có cấu hình)." class="FontDefault"
                            style="margin-top: -50px;"></v-checkbox>
                        <v-checkbox label="Tích hợp trình ký." class="FontDefault"
                            style="margin-top: -50px;"></v-checkbox>
                    </v-col>

                    <!-- button xác nhận -->
                    <v-col cols="12" md="12">
                        <v-btn class="btnAdd btn no-uppercase" @click="btncauHinhXacNhan(true)" style="float: right;">
                            Xác nhận
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

import { ref, reactive, onMounted, computed } from 'vue';
import { dia, shapes, util } from 'jointjs';
import { callAuthenticationAPI } from "@/providers/data-provider";
import * as joint from 'jointjs';
let dialog = ref(false);
let typeText = ''; // type node trong popup
let tenNode = ref("");
let _ActionName = ref("");
let isPanelOpen = ref(false);
let _indexNode = ref(0); // Index khi add ndoe
let _selectNodeId = ref(""); // chọn bước để trả về
let _isShowListNode = ref(false); // list control các bước
let _isShoeMessError = ref(false); // ẩn hiên thông báo lỗi
let _meesageError = ref(''); // Mess thông báo lỗi

/* biến ẩn hiển các bước trên màn hình */

/* biến ẩn hiển các bước trên màn hình */


// Tham chiếu tới container của sơ đồ
const paperContainer = ref<HTMLDivElement | null>(null);

// Quản lý trạng thái của context menu
const isContextMenuVisible = ref(false);
const contextMenuPosition = reactive({ x: 0, y: 0 });

const contextMenuStyle = computed(() => ({
    top: `${contextMenuPosition.y}px`,
    left: `${contextMenuPosition.x + 20}px`,
}));

// Khởi tạo các thành phần của JointJS
let paper: dia.Paper;
let graph: dia.Graph;

// Biến lưu trữ nút cuối cùng và nút được chọn
let lastNode: dia.Element | null = null;
let _selectedNode: dia.Element | null = null;

interface INodeMap {
    KeyId: string | null,
    Type: string | null,
    Index: number,
    TenNode: String | null
    X: number,
    Y: number
}
interface IDiagram {
    KeyId: string | null,
    Source: string | null,
    Target: string | null
    TenDiagram: string | null
}
interface INextStep {
    NodeIdStart: string | null,
    NodeIdEnd: string | null,
    DiagramId: string | null,
    ActionName: string | null, // Tên Diagram
    Action: number
}
const request = ref({
    INodeMap: [] as INodeMap[],
    IDiagram: [] as IDiagram[],
    INextStep: [] as INextStep[]
});
let positions: { x: number; y: number }[] = [];


const emit = defineEmits<{
    (event: 'emit_Node', data: {
        INodeMap: INodeMap[],
        IDiagram: IDiagram[]
    }): void;
}>();



onMounted(() => {
    graph = new dia.Graph({}, { cellNamespace: shapes });
    paper = new dia.Paper({
        el: paperContainer.value!,
        model: graph,
        width: window.innerWidth, // Full window width
        height: window.innerHeight - 30, // Full window height
        gridSize: 10,
        drawGrid: true,
        overflow: true,
        cellViewNamespace: shapes
    });

    paper.on('element:contextmenu', (elementView: dia.ElementView, evt: dia.Event, x: number, y: number) => {
        evt.preventDefault(); // Ngăn menu mặc định của trình duyệt
        // Lưu node được chọn
        _selectedNode = elementView.model;
        // Hiển thị menu ngữ cảnh tại vị trí chuột
        contextMenuPosition.x = x + 300;
        contextMenuPosition.y = y + 50;
        isContextMenuVisible.value = true;
    });
});

const showContextMenu = (event: MouseEvent) => {
    event.preventDefault(); // Ngăn menu mặc định của trình duyệt
    //const paperWidth = paper.options.width;
    //const paperHeight = paper.options.height;
    contextMenuPosition.x = event.clientX + 30;
    contextMenuPosition.y = event.clientY - 100;
    isContextMenuVisible.value = true;

};

// Ẩn context menu
const hideContextMenu = () => {
    isContextMenuVisible.value = false;
};

const createNode = (type: string) => {
    let newNode;
    let titleNode = tenNode.value;
    if (type === 'vuong_buoc') {
        // Tạo node hình vuông
        newNode = new shapes.standard.Rectangle();
        newNode.attr({
            body: {
                fill: '#0099CC', // Màu nền
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: titleNode,
                fill: 'black',
                refY: 60, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(50, 50); // Kích thước hình vuông
    } else if (type === 'thoi_dieukien') {
        newNode = new shapes.standard.Polygon();
        newNode.attr({
            body: {

                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi_dieukien
                fill: '#FFE4B5', // Màu nền
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: titleNode,
                fill: 'black',
                refY: 80, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(70, 70);
    } else if (type === 'thoi_ketthuc') {
        newNode = new shapes.standard.Polygon();
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50',
                fill: '#B5EAD7', // Màu nền
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: titleNode,
                fill: 'black',
                refY: 80, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(70, 70);
    }
    else if (type === 'thoi_tuchoi') {
        newNode = new shapes.standard.Polygon();
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi_dieukien
                fill: '#CC0033', // Màu nền
                stroke: 'black', // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: titleNode,
                fill: 'black',
                refY: '100%',
                refY2: 10, // Dịch chuyển tương đối
                textAnchor: 'middle', // Căn giữa theo chiều ngang
                yAlignment: 'middle', // Căn giữa theo chiều dọc
            },
        });
        newNode.resize(70, 70);
    } else if (type === 'bauduc_batdau') {
        // Tạo node hình bầu dục (ellipse)
        newNode = new shapes.standard.Ellipse();
        newNode.attr({
            body: {
                fill: '#87CEFA', // Màu nền hình bầu dục
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: titleNode,
                fill: 'black',
                refY: 60, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(70, 50);
    } else if (type === 'tron') {
        newNode = new shapes.standard.Circle();
        newNode.attr({
            body: {
                fill: 'lightblue', // Màu nền hình bầu dục
            },
            label: {
                text: titleNode,
                fill: 'black',
                refY: 60, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(50, 50);
    }

    if (!newNode) return;

    const x = contextMenuPosition.x - 250;
    const y = contextMenuPosition.y;
    newNode.position(
        x, y
    );
    // newNode.attr({
    //     body: { fill: 'orange' },
    //     label: { text: type === 'tron' ? 'Hình vuông' : 'Hình thoi_dieukien', fill: 'white' },
    // });

    graph.addCell(newNode);

    request.value.INodeMap.push({
        Index: _indexNode.value,
        X: x,
        KeyId: newNode.id as string,
        Type: type,
        TenNode: titleNode,
        Y: y
    });

    _indexNode.value++;

    // Tạo liên kết từ node được chọn hoặc node cuối cùng
    const sourceNode = _selectedNode || lastNode;
    if (sourceNode) {
        const link = new shapes.standard.Link();
        link.source(sourceNode);
        link.target(newNode);
        link.attr({
            line: { stroke: 'black', strokeWidth: 1 },
        });
        link.appendLabel({
            attrs: {
                text: {
                    text: _ActionName.value, // Nội dung nhãn
                    fill: 'black', // Màu chữ
                    fontSize: 14, // Kích thước chữ
                },
            },
            position: {
                distance: 0.5, // Vị trí nhãn nằm giữa đường nối
            },
        });
        link.router('orthogonal');
        link.connector('straight', { cornerType: 'line' });
        graph.addCell(link);

        // Tạo Diagram
        request.value.IDiagram.push({
            KeyId: link.id as string,
            Source: newNode.id as string,
            Target: link.id as string,
            TenDiagram: _ActionName.value
        });

        // Phân tích tạo NextStep
        switch (type) {
            case 'vuong_buoc':
                {
                    request.value.INextStep.push({
                        NodeIdStart: _selectedNode?.id as string,
                        NodeIdEnd: newNode.id as string,
                        DiagramId: link.id as string,
                        ActionName: _ActionName.value,
                        Action: 0
                    });
                    break;
                }
                case 'thoi_dieukien':{
                    request.value.INextStep.push({
                        NodeIdStart: _selectedNode?.id as string,
                        NodeIdEnd: newNode.id as string,
                        DiagramId: link.id as string,
                        ActionName: _ActionName.value,
                        Action: 0
                    });
                    break;
                }
            default:
                break;
        }

        console.log(request.value.INextStep);


    }

    lastNode = newNode;
    _selectedNode = null;

    hideContextMenu();
};

const deleteNode = () => {
    if (_selectedNode) {
        _selectedNode.remove();
        request.value.INodeMap = request.value.INodeMap.filter(x => x.KeyId != _selectedNode?.id);
        request.value.IDiagram = request.value.IDiagram.filter(x => x.Source != _selectedNode?.id);
        _selectedNode = null;
    } else {
        alert('Vui lòng chọn node cần xóa');
    }
    hideContextMenu();
};

function btnTraVe() {
    if (_selectedNode) {
        //const nodes = graph.getElements();

        if (_selectNodeId.value == _selectedNode.id) {
            _isShoeMessError.value = true;
            _meesageError.value = "Không được chọn cùng bước để trả về";
            return;
        }

        const link = new joint.shapes.standard.Link();
        link.source({ id: _selectedNode.id }); // Từ node
        link.target({ id: _selectNodeId.value }); // đến node
        link.attr({
            line: { stroke: 'black', strokeWidth: 1 },
        });
        link.appendLabel({
            attrs: {
                text: {
                    text: _ActionName.value, // Nội dung nhãn
                    fill: 'black', // Màu chữ
                    fontSize: 14, // Kích thước chữ
                },
            },
            position: {
                distance: 0.5, // Vị trí nhãn nằm giữa đường nối
            },
        });
        link.router('orthogonal');
        link.connector('straight', { cornerType: 'line' });
        graph.addCell(link);

    } else {
        alert('Vui lòng chọn click chuột phải để chọn bước thực hiện');
    }
    hideContextMenu();
}

function CauHinhBuoc() {
    isPanelOpen.value = !isPanelOpen.value;
    hideContextMenu();
}

async function API_AddQuyTrinh() {
    try {
        let responseData = await callAuthenticationAPI('/api/quanlythongtin/QuyTrinhNode/AddQuyTrinhNode', 'POST', {
            lsdiagram: request.value.IDiagram,
            lsnodes: request.value.INodeMap,
            MaQuyTrinh: "",
            tenNode: "",
            UserId: "",
            UserName: localStorage.getItem("UserName")
        }, {
            timeout: 15000
        });
    } catch (error) {
        console.log(error);
    }
}


function btnXacNhanDialog(status: boolean) {
    if (status == false) {
        dialog.value = false;

    }
    else {
        if (tenNode.value === '') {
            _isShoeMessError.value = true;
            _meesageError.value = "Vui lòng điền tên bước quyt trình";
            dialog.value = false;

            tenNode.value = "";
            _ActionName.value = "";
            return;
        }

        if (_isShowListNode.value == true) {
            btnTraVe();
            _selectNodeId.value = '';
        }
        else {
            createNode(typeText);
        }

        dialog.value = false;
    }
    tenNode.value = "";
    _ActionName.value = "";
    emit('emit_Node', request.value);
}

function btnAddNode(type: string) {
    dialog.value = true;
    typeText = type;
    if (type == "thoi_trave") {
        _isShowListNode.value = true;
    }
    else {
        _isShowListNode.value = false;
    }
    hideContextMenu();
}

function btncauHinhXacNhan(status: boolean) {
    isPanelOpen.value = false;
}

</script>

<style scoped>
.diagram-container {
    background: linear-gradient(0deg, transparent 9%, #ddd 10%) 0% 0%,
        linear-gradient(90deg, transparent 9%, #ddd 10%) 0% 0%;
    background-size: 10px 10px;
    /* position: relative; */
    /* overflow: auto; */
}

#bieudoquytrinh {
    position: relative;
    overflow: auto;
    height: 100vh;
    box-shadow: 0 0 0 1.5px rgba(0, 0, 0, 0.3);
    padding: 0px;
}

.context-menu {
    position: absolute;
    z-index: 100;
    background: #fff;
    border: 1px solid #ccc;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.2);
    padding: 0;
}

.context-menu ul {
    list-style: none;
    margin: 0;
    padding: 0;
}

.context-menu li {
    padding: 4px 15px;
    cursor: context-menu;
}

.context-menu li:hover {
    background-color: #eee;
}

.icon-border2 {
    align-items: right;
    justify-content: right;
    border-radius: 4px;
    /* Bo góc (nếu cần vuông thì đặt 0) */
    color: rgb(0, 0, 0);
    /* Màu của icon */
    background-color: transparent;
    /* Nền bên trong icon */

}

/* Đặt kích thước của icon */
.icon-border2 i {
    font-size: 15px;
    /* Kích thước icon */
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

select {
    appearance: auto;
    -webkit-appearance: auto;
    -moz-appearance: auto;
}

.combobox-container {
    position: relative;
    width: 100%;
}

.combobox {
    width: 100%;
    padding: 8px;
    font-size: 14px;
    border: 1px solid #ccc;
    border-radius: 4px;
    appearance: none;
    -webkit-appearance: none;
    -moz-appearance: none;
    background-color: white;
}

.combobox:focus {
    border-color: #007bff;
    outline: none;
    box-shadow: 0 0 3px #007bff;
}

.combobox-icon {
    position: absolute;
    top: 50%;
    right: 12px;
    transform: translateY(-50%);
    pointer-events: none;
    font-size: 14px;
    color: #aaa;
}
</style>
