<template>
    <div id="bieudoquytrinh">
        <div ref="paperContainer" class="diagram-container" @contextmenu.prevent="showContextMenu"></div>

    </div>
    <div v-if="isContextMenuVisible" :style="contextMenuStyle" class="context-menu FontDefault">
        <ul>
            <li @click="btnAddNode('5')" style=" color: #87CEFA;" v-if="_xbatdau">
                <span class="icon-border2" style=" color: #87CEFA;">
                    <i class="fas fa-circle"></i>
                </span>
                Tạo bước bắt đầu
            </li>
            <li @click="btnAddNode('1')" style=" color: #0099CC;" v-if="_xbuoc">
                <span class="icon-border2" style=" color: #0099CC;">
                    <i class="fas fa-square"></i>
                </span>
                Tạo bước tiếp theo
            </li>
            <li @click="btnAddNode('2')" style=" color: #FF9933;" v-if="_xdieukien">
                <span class="icon-border2" style=" color: #FF9933;">
                    <i class="fas fa-exclamation-triangle"></i>
                </span>
                Tạo bước điều kiện
            </li>
            <li @click="btnAddNode('thoi_trave')" style=" color: #FF9933;" v-if="_xtrave">
                <span class="icon-border2" style=" color: #FF9933;">
                    <i class="fas fa-undo-alt"></i>
                </span>
                Trả về hoặc chuyển bước
            </li>
            <li @click="btnAddNode('3')" style=" color: #009966;" v-if="_xduyet">
                <span class="icon-border2" style=" color: #009966;">
                    <i class="fas fa-check"></i>
                </span>
                Duyệt
            </li>
            <li @click="btnAddNode('4')" style=" color: #CC0033;" v-if="_xketthuc">
                <span class="icon-border2" style=" color: #CC0033;">
                    <i class="fas fa-times"></i>
                </span>
                Từ chối
            </li>
            <li @click="CauHinhBuoc" v-if="_xCauHinhBuoc">
                <span class="icon-border2">
                    <i class="fa fa-gear"></i>
                </span>
                Cấu hình bước
            </li>
            <li @click="deleteNode" v-if="_xXoaNode">
                <span class="icon-border2">
                    <i class="fas fa-trash-alt"></i>
                </span> Xóa
            </li>
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
                                <option v-for="(item, index) in _dataTraVe.INodeMap" :key="item.keyId || ''"
                                    :value="item.keyId || ''">
                                    {{ item.tenNode }}
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
                        <h3>Cấu hình bước: {{ _txttitlecauhinh }}</h3>
                    </v-col>

                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Người tiếp nhận
                        </label>
                        <CBCheckPhongBanNode :phognbanId="_phongbanIdMap" @emit_phonganId="handlePhongBan"
                            @change="changeSelectPhongBan" />
                    </v-col>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Phòng ban tiếp nhận
                        </label>
                        <CBCheckNhanSuNode :nhansuIdMap="_nhansuIdMap" :ListPhongBanId="_phongbanId"
                            @emit_nhansuId="handleNhanSu">
                        </CBCheckNhanSuNode>
                    </v-col>

                    <v-col cols="12" md="12">
                        <div class="input-container">
                            <label for="" class="FontDefault">
                                Ghi chú
                            </label>
                            <textarea v-model="_txtghichuNodeSetting" class="form-control"
                                placeholder="Nội dung ghi chú"></textarea>
                        </div>
                    </v-col>

                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Cấu hình mail nhắc nhở
                        </label>
                        <input type="number" id="" class="FontDefault"
                            placeholder="Số mỗi lần gửi mail cách nhau bao nhiêu phút" style="width: 100%;" min="0"
                            max="1440" v-model="_txtCauhinhmailnhacnhonodesetting" />
                    </v-col>


                    <v-col cols="12" md="12">
                        <v-checkbox label="Tạo báo cáo tự động khi hoàn thành." class="FontDefault"
                            v-model="_isTaoTaskBaoCao"></v-checkbox>
                        <v-checkbox label="Gửi mail cho người/phòng ban tiếp nhận." class="FontDefault"
                            style="margin-top: -50px;" v-model="_isGuiMailPhongBanTiepNhan"></v-checkbox>
                        <v-checkbox label="Gửi mail nhắc nhở nếu có (có cấu hình)." class="FontDefault"
                            style="margin-top: -50px;" v-model="_isGuiMailNhacNho"></v-checkbox>
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
let _indexNode = ref(0); // index khi add ndoe
let _selectNodeId = ref(""); // chọn bước trên combobox để trả về
let _isShowListNode = ref(false); // list control các bước
let _isShoeMessError = ref(false); // ẩn hiên thông báo lỗi
let _meesageError = ref(''); // Mess thông báo lỗi
let _phongbanId = ref<number[]>([]); // phòng ban Id select
let _phongbanIdMap = ref<number[]>([]);// phòng ban Id để map data
let _txttitlecauhinh = ref("");
let _nhansuId = ref<string[]>([]);
let _nhansuIdMap = ref<string[]>([]);
let _txtghichuNodeSetting = ref("");
let _txtCauhinhmailnhacnhonodesetting = ref("");
let _isTaoTaskBaoCao = ref(false);
let _isGuiMailPhongBanTiepNhan = ref(false);
let _isGuiMailNhacNho = ref(false);


/* biến ẩn hiển các bước trên màn hình */
let _xbatdau = ref(true);
let _xbuoc = ref(false);
let _xdieukien = ref(false);
let _xketthuc = ref(false);
let _xduyet = ref(false);
let _xtrave = ref(false);
let _xXoaNode = ref(false);
let _xCauHinhBuoc = ref(false);
/* biến ẩn hiển các bước trên màn hình */

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
const paperContainer = ref<HTMLDivElement | null>(null);

// Biến lưu trữ nút cuối cùng và nút được chọn
let lastNode: dia.Element | null = null;
let _selectedNode: dia.Element | null = null;

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
const request = ref({
    INodeMap: [] as INodeMap[],
    IDiagram: [] as IDiagram[],
    INextStep: [] as INextStep[],
    InodeSttings: [] as InodeSttings[]
});
let positions: { x: number; y: number }[] = [];
let _dataTraVe = ref({
    INodeMap: [] as INodeMap[]
});

const emit = defineEmits<{
    (event: 'emit_Node', data: {
        INodeMap: INodeMap[],
        IDiagram: IDiagram[],
        INextStep: INextStep[];
        InodeSttings: InodeSttings[]
    }): void;
}>();

const props = defineProps<{
    INodeMap: INodeMap[],
    IDiagram: IDiagram[],
    INextStep: INextStep[],
    InodeSttings: InodeSttings[]
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
    let isNodeContextMenuTriggered = false; // Cờ kiểm tra sự kiện trong node

    // Xử lý chuột phải trên node
    paper.on('element:contextmenu', (elementView: dia.ElementView, evt: dia.Event, x: number, y: number) => {
        evt.preventDefault(); // Ngăn menu mặc định của trình duyệt
        isNodeContextMenuTriggered = true; // Đánh dấu rằng sự kiện trong node đã được gọi
        // Lưu node được chọn
        _selectedNode = elementView.model;

        // Hiển thị menu ngữ cảnh tại vị trí chuột
        contextMenuPosition.x = x + 300;
        contextMenuPosition.y = y + 50;
        isContextMenuVisible.value = true;

        // Gọi hàm với id của node được chọn
        _xCauHinhBuoc.value = true;
        CheckLogicAddNode(_selectedNode?.id as string);
    });

    // Xử lý chuột phải ra ngoài node
    paperContainer.value?.addEventListener("contextmenu", (evt) => {
        evt.preventDefault(); // Ngăn menu mặc định của trình duyệt
        // Kiểm tra nếu sự kiện trong node đã được xử lý
        if (isNodeContextMenuTriggered) {
            isNodeContextMenuTriggered = false; // Reset lại cờ cho lần tiếp theo
            return; // Không xử lý chuột phải ngoài node
        }
        const target = evt.target as HTMLElement;
        _selectedNode = null;

        // Kiểm tra nếu không click vào một node
        if (!target.closest(".element")) {
            isContextMenuVisible.value = true;

            // Gọi hàm với chuỗi rỗng
            _xCauHinhBuoc.value = false;
            CheckLogicAddNode("");
        }
    });
     /* Sự kiện khi thay đổi vị trí node */
     graph!.on("change:position", (cell: dia.Cell) => {
        if (cell.isElement()) {
            const ndoeUpdate = request.value.INodeMap?.find(x => x.keyId == cell.id);
            if (ndoeUpdate) {
                ndoeUpdate.x = cell.position().x;
                ndoeUpdate.y = cell.position().y;
            }
        }
    });

});

const showContextMenu = (event: MouseEvent) => {
    event.preventDefault();
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
    if (type === '1') {
        // Tạo node hình vuông bước
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
    } else if (type === '2') {
        // Điều kiện 
        newNode = new shapes.standard.Polygon();
        newNode.attr({
            body: {

                refPoints: '50,0 100,50 50,100 0,50',
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
    } else if (type === '3') {
        // kết thúc
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
    else if (type === '4') {
        // từ chối
        newNode = new shapes.standard.Polygon();
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng 2
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
    } else if (type === '5') {
        // bắt đầu
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
    //     label: { text: type === 'tron' ? 'Hình vuông' : 'Hình 2', fill: 'white' },
    // });

    graph.addCell(newNode);

    request.value.INodeMap.push({
        index: _indexNode.value,
        x: x,
        keyId: newNode.id as string,
        type: parseInt(type),
        tenNode: titleNode,
        y: y
    });

    _indexNode.value++;
    // Tạo liên kết từ node được chọn hoặc node cuối cùng
    let sourceNode = _selectedNode || lastNode;
    const link = new shapes.standard.Link();

    if (request.value.INextStep.length == 0) {
        sourceNode = null;
    }
    if (sourceNode) {
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
            keyId: link.id as string,
            source: newNode.id as string,
            target: link.id as string,
            tenDiagram: _ActionName.value
        });
    }

    // Phân tích tạo NextStep
    switch (type) {
        case '5': {
            request.value.INextStep.push({
                nodeIdStart: newNode.id as string,
                nodeIdEnd: '',
                diagramId: '',
                actionName: _ActionName.value,
                action: 0,
                typeNextStep: parseInt(type)
            })
            break;
        }
        default: {
            request.value.INextStep.push({
                nodeIdStart: sourceNode?.id as string,
                nodeIdEnd: newNode.id as string,
                diagramId: link.id as string,
                actionName: _ActionName.value,
                action: 0,
                typeNextStep: parseInt(type)
            })
            break;
        }

    }
    lastNode = newNode;
    _selectedNode = null;

    hideContextMenu();
};

const createMapNode = (type: string, nx: number, ny: number, ntennode: string, ntendigram: string, nodeId: string) => {
    let newNode;
    let titleNode = ntennode;
    if (type === '1') {
        // Tạo node hình vuông bước
        newNode = new shapes.standard.Rectangle({id: nodeId });
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
    } else if (type === '2') {
        // Điều kiện 
        newNode = new shapes.standard.Polygon({id: nodeId });
        newNode.attr({
            body: {

                refPoints: '50,0 100,50 50,100 0,50',
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
    } else if (type === '3') {
        // kết thúc
        newNode = new shapes.standard.Polygon({id: nodeId });
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
    else if (type === '4') {
        // từ chối
        newNode = new shapes.standard.Polygon({id: nodeId });
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng 2
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
    } else if (type === '5') {
        // bắt đầu
        newNode = new shapes.standard.Ellipse({id: nodeId });
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
        newNode = new shapes.standard.Circle({id: nodeId });
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
    newNode.position(
        nx, ny
    );

    graph.addCell(newNode);

    request.value.INodeMap.push({
        index: _indexNode.value,
        x: nx,
        keyId: newNode.id as string,
        type: parseInt(type),
        tenNode: titleNode,
        y: ny
    });
    _indexNode.value++;
};

function createMapDiagram(startnodeiD: string, endnodeId: string, actionname: string, type: number, Id:string) {
    const link = new joint.shapes.standard.Link({
        id: Id
    });
    link.source({ id: startnodeiD }); // Từ node
    link.target({ id: endnodeId }); // đến node
    link.attr({
        line: { stroke: 'black', strokeWidth: 1 },
    });
    link.appendLabel({
        attrs: {
            text: {
                text: actionname, // Nội dung nhãn
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

    request.value.INextStep.push({
        nodeIdStart: startnodeiD,
        nodeIdEnd: endnodeId,
        diagramId: link.id as string,
        actionName: actionname,
        action: 0,
        typeNextStep: type
    })
}

const deleteNode = () => {
    if (_selectedNode) {
        _selectedNode.remove();
        request.value.INodeMap = request.value.INodeMap.filter(x => x.keyId != _selectedNode?.id);
        request.value.IDiagram = request.value.IDiagram.filter(x => x.source != _selectedNode?.id);
        request.value.INextStep = request.value.INextStep.filter(x => x.nodeIdStart != _selectedNode?.id);
        request.value.INextStep = request.value.INextStep.filter(x => x.nodeIdEnd != _selectedNode?.id);
        request.value.InodeSttings = request.value.InodeSttings.filter(x => x.keyNode != _selectedNode?.id);
        _selectedNode = null;
        emit('emit_Node', request.value);
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

        request.value.INextStep.push({
            nodeIdStart: _selectedNode?.id as string,
            nodeIdEnd: _selectNodeId.value as string,
            diagramId: link.id as string,
            actionName: _ActionName.value,
            action: 0,
            typeNextStep: 6
        })

    } else {
        alert('Vui lòng chọn click chuột phải để chọn bước thực hiện');
    }
    hideContextMenu();
}

/* btn popup cấu hình từng node dev01*/
function CauHinhBuoc() {
    debugger
    isPanelOpen.value = !isPanelOpen.value;
    hideContextMenu();
    const data_Node = request.value.INodeMap.find(x => x.keyId == _selectedNode?.id);
    if (data_Node == null) return;
    _txttitlecauhinh.value = data_Node?.tenNode?.toString() ?? "";

    const data_nodesetting = request.value.InodeSttings.find(x => x.keyNode == _selectedNode?.id);
    console.log("dsds");
    console.log(data_nodesetting?.phongBanNodeModels);
    console.log("dsds");
    if (data_nodesetting == null) {
        _phongbanIdMap.value = [];
        _nhansuIdMap.value = [];
        _txtghichuNodeSetting.value = "";
        _txtCauhinhmailnhacnhonodesetting.value = "";
        _isTaoTaskBaoCao.value = false;
        _isGuiMailPhongBanTiepNhan.value = false;
        _isGuiMailNhacNho.value = false;
    }
    else {
        _phongbanIdMap.value = data_nodesetting.phongBanNodeModels;
        _nhansuIdMap.value = data_nodesetting.nhanSuNodeModels;
        _txtghichuNodeSetting.value = data_nodesetting.ghiChu ?? "";
        _txtCauhinhmailnhacnhonodesetting.value = (data_nodesetting.cauHinhMailNhacNho??0).toString();
        _isTaoTaskBaoCao.value = data_nodesetting.isTaoTaskBaoCao;
        _isGuiMailPhongBanTiepNhan.value = data_nodesetting.isGuiMailPhongBanTiepNhan;
        _isGuiMailNhacNho.value = data_nodesetting.isGuiMailNhacNho;
    }

}

/* btn xác nhận dialog tạo node */
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
    emit('emit_Node', request.value);
    tenNode.value = "";
    _ActionName.value = "";
}

function btnAddNode(type: string) { // Add node  khi xác nhận trong popup
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

/* xác nhận cài đặt  cấu hình từng node */
function btncauHinhXacNhan(status: boolean) {
    isPanelOpen.value = false;
    if (status) {
        const dataNode = request.value.InodeSttings.find(x => x.keyNode == _selectedNode?.id);
        if (dataNode != null) {
            request.value.InodeSttings = request.value.InodeSttings.filter(x => x.keyNode != _selectedNode?.id);
        }

        request.value.InodeSttings.push({
            keyNode: _selectedNode?.id?.toString() ?? "",
            nhanSuNodeModels: _nhansuId.value,
            phongBanNodeModels: _phongbanId.value,
            ghiChu: _txtghichuNodeSetting.value,
            isTaoTaskBaoCao: _isTaoTaskBaoCao.value,
            isGuiMailPhongBanTiepNhan: _isGuiMailPhongBanTiepNhan.value,
            isGuiMailNhacNho: _isGuiMailNhacNho.value,
            cauHinhMailNhacNho: parseInt(_txtCauhinhmailnhacnhonodesetting.value)
        });

        emit('emit_Node', request.value);
    }
    else {
        _nhansuIdMap.value = [];
        _phongbanIdMap.value = [];
    }


}

function CheckLogicAddNode(SelectNode: string) {
    const data_Node = request.value.INodeMap;
    const data_Diagram = request.value.IDiagram;
    const data_nextStep = request.value.INextStep;

    const position = _selectedNode?.position(); // Lấy tọa độ


    if (SelectNode.length > 0) { // sự kiện click chọn node
        let typeNode = data_Node.find(x => x.keyId == SelectNode);
        switch (typeNode?.type) {
            case 5: { // bắt đầu
                if (data_nextStep.length == 1) { // có data
                    _xbatdau.value = _xdieukien.value = _xketthuc.value = _xduyet.value = _xtrave.value = false;
                    _xbuoc.value = true;
                }
                else {
                    _xbuoc.value = _xbatdau.value = _xdieukien.value = _xketthuc.value = _xduyet.value = _xtrave.value = false;
                }

                break;
            }
            case 1: { // bước
                if (data_nextStep.filter(x => x.nodeIdStart == SelectNode).length > 0) { // bước đã có diagram start
                    _xbuoc.value = _xbatdau.value = _xtrave.value = false;
                    _xdieukien.value = _xketthuc.value = _xduyet.value = false;
                }
                else { // bước chưa có diagram
                    _xbatdau.value = _xtrave.value = false;
                    _xbuoc.value = _xdieukien.value = _xketthuc.value = _xduyet.value = true;
                }

                break;
            }
            case 2: { // điều kiện
                /* Điều kiện chưa có gì hết */
                _xbuoc.value = _xketthuc.value = _xduyet.value = _xtrave.value = true;
                if (data_nextStep.filter(x => x.nodeIdStart == SelectNode).length == 0) {
                    _xbatdau.value = false;
                }
                else {
                    const stepNode = data_nextStep.filter(x => x.nodeIdStart == SelectNode);

                    if (stepNode.length >= 3) { // điều kiện chỉ được chọn ít nhất 2 lần
                        _xbuoc.value = _xdieukien.value = _xketthuc.value = _xduyet.value = _xtrave.value = false;
                        break;
                    };
                    stepNode.forEach((item) => {
                        /* đang lỗi ở đây, chỉ dc chọn 3 bước trong điề kiện */
                        switch (item?.typeNextStep) {
                            case 1: {
                                _xbuoc.value = false;
                                break;
                            }
                            case 3: {
                                _xduyet.value = false;
                                break;
                            }
                            case 4: {
                                _xketthuc.value = false;
                                break;
                            }
                            case 6: {
                                _xtrave.value = false;
                                break;
                            }
                            default:
                                _xbuoc.value = _xdieukien.value = _xketthuc.value = _xduyet.value = _xtrave.value = false;
                                break;
                        }

                    });
                }
                _xdieukien.value = false;

                /* Cấu hinh lại sanh sách trả về
                    ko trả về bước tạo
                    ko trả về cùng bước
                    ko trả về cho bước tiếp theo
                */
                const buoctieptheo = data_nextStep.find(x => x.nodeIdStart == SelectNode);
                _dataTraVe.value.INodeMap = data_Node.filter(x => x.type != 2 && x.type != 5 && x.keyId != buoctieptheo?.nodeIdEnd);

                break;
            }
            case 3: {
                _xbuoc.value = _xdieukien.value = _xketthuc.value = _xduyet.value = _xtrave.value = false;
                _xXoaNode.value = true;
                break;
            }
            case 4: {
                // Từ chối
                _xbuoc.value = _xdieukien.value = _xketthuc.value = _xduyet.value = _xtrave.value = false;
                _xXoaNode.value = true;
            }
            default: {
                break;
            }
        }

        /* kiểm tra nếu là ước cuối thì cho xóa */
        if (data_Node.length == 1) {
            _xXoaNode.value = true;
        }
        else if (data_nextStep.filter(x => x.nodeIdStart == SelectNode).length == 1 && data_nextStep.filter(x => x.typeNextStep == 6).length == 1) {
            _xXoaNode.value = true;
        }
        else if (data_nextStep.filter(x => x.nodeIdStart == SelectNode).length >= 1) {
            _xXoaNode.value = false;
        }
        else {
            _xXoaNode.value = true;
        }

    }
    else { // sự kiện khi click ra ngoài
        if (data_nextStep.length <= 0) {
            _xbuoc.value = _xdieukien.value = _xketthuc.value = _xduyet.value = _xtrave.value = false;
            _xbatdau.value = true;
            _xXoaNode.value = false;
        }
        else if (data_nextStep.length > 0) {
            _xbuoc.value = true;
            _xbatdau.value = _xdieukien.value = _xketthuc.value = _xduyet.value = _xtrave.value = false;
            _xXoaNode.value = false;
        }
    }
}

const handlePhongBan = (phongbanId: number[]) => {
    _phongbanId.value = phongbanId;
};
const handleNhanSu = (nhansuId: string[]) => {
    _nhansuId.value = nhansuId;
};

function changeSelectPhongBan() {
    _nhansuIdMap.value = [];
}


watch([() => props.INodeMap, () => props.IDiagram, () => props.INextStep, () => props.InodeSttings], 
    ([newNodeMap, newDiagram, newNExtStep, newNodeSttings], 
     [oldNodeMap, oldDiagramm, oldNExtStep, oldNodeSttings]) => {
    newNodeMap.forEach((node, index) => {
        const diagram = newDiagram?.find(x => x.keyId == node.keyId)
        createMapNode(node.type.toString(), node.x, node.y, node.tenNode ?? "", diagram?.tenDiagram ?? "", node.keyId ?? "");
    });
    newDiagram.forEach((diagram, index) => {
        createMapDiagram(diagram.source ?? "", diagram.target ?? "", diagram.tenDiagram ?? "",1,diagram.keyId??"")
    });

    request.value.InodeSttings = newNodeSttings
    request.value.INextStep = newNExtStep;
});

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
    z-index: 500;
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
    background-color: white;
    z-index: 200;
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
