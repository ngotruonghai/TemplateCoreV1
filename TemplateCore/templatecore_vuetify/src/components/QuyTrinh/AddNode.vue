<template>
    <div id="bieudoquytrinh">
        <div ref="paperContainer" class="diagram-container" @contextmenu.prevent="showContextMenu"></div>
    </div>

    <v-dialog v-model="dialog" max-width="400" persistent>
        <template v-slot:activator="{ props: activatorProps }">
        </template>

        <v-card title="Tạo bước quy trình">
            <v-container>
                <v-row>
                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Tên bước thực hiện
                        </label>
                        <input type="text" id="" class="FontDefault" placeholder="VD: Bước duyệt quy trình"
                            style="width: 100%;" v-model="tenNode" />
                    </v-col>

                    <v-col cols="12" md="12">
                        <label class="FontDefault">
                            Ghi chú mũi tên
                        </label>
                        <input type="text" id="" class="FontDefault"
                            placeholder="VD: Chuyển quy trình bước 1 sang bước 2" style="width: 100%;"
                            v-model="ghichuNode" />
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

    <div v-if="isContextMenuVisible" :style="contextMenuStyle" class="context-menu FontDefault">
        <ul>
            <li @click="btnAddNode('bauduc')" style=" color: #87CEFA;">
                <span class="icon-border2" style=" color: #87CEFA;">
                    <i class="fas fa-circle"></i>
                </span>
                Bắt đầu
            </li>
            <li @click="btnAddNode('thoi')" style=" color: #FF9933;">
                <span class="icon-border2" style=" color: #FF9933;">
                    <i class="fas fa-exclamation-triangle"></i>
                </span>
                Tạo điều kiện
            </li>
            <li @click="btnAddNode('vuong')" style=" color: #0099CC;">
                <span class="icon-border2" style=" color: #0099CC;">
                    <i class="fas fa-square"></i>
                </span>
                Tạo bước
            </li>
            <li @click="btnAddNode('thoi_ketthuc')" style=" color: #009966;">
                <span class="icon-border2" style=" color: #009966;">
                    <i class="fas fa-check"></i>
                </span>
                Tạo bước duyệt
            </li>
            <li @click="btnAddNode('thoi_tuchoi')" style=" color: #FF9AA2;">
                <span class="icon-border2" style=" color: #FF9AA2;">
                    <i class="fas fa-times"></i>
                </span>
                Tạo bước từ chối
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
                        <input type="number" id="" class="FontDefault" placeholder="Số mỗi lần gửi mail cách nhau bao nhiêu phút" style="width: 100%;" min="0" max="1440"/>
                    </v-col>

                    
                    <v-col cols="12" md="12">
                        <v-checkbox label="Tạo báo cáo tự động khi hoàn thành." class="FontDefault"></v-checkbox>
                        <v-checkbox label="Gửi mail cho người/phòng ban tiếp nhận." class="FontDefault" style="margin-top: -50px;"></v-checkbox>
                        <v-checkbox label="Gửi mail nhắc nhở nếu có (có cấu hình)." class="FontDefault" style="margin-top: -50px;"></v-checkbox>
                        <v-checkbox label="Tích hợp trình ký." class="FontDefault" style="margin-top: -50px;"></v-checkbox>
                    </v-col>

                    <!-- button xác nhận -->
                    <v-col cols="12" md="12">
                        <v-btn class="btnAdd btn no-uppercase" @click="btncauHinhXacNhan(true)" style="float: right;">
                            Xác nhận
                        </v-btn>
                        <v-btn @click="btncauHinhXacNhan(false)" class="btnCancel no-uppercase" style="float: right; margin-right: 10px;">
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
let dialog = ref(false);
let typeText = '';
let tenNode = ref("");
let ghichuNode = ref("");
let isPanelOpen = ref(false);;

// Tham chiếu tới container của sơ đồ
const paperContainer = ref<HTMLDivElement | null>(null);

// Quản lý trạng thái của context menu
const isContextMenuVisible = ref(false);
const contextMenuPosition = reactive({ x: 0, y: 0 });

const contextMenuStyle = computed(() => ({
    top: `${contextMenuPosition.y}px`,
    left: `${contextMenuPosition.x}px`,
}));

// Khởi tạo các thành phần của JointJS
let paper: dia.Paper;
let graph: dia.Graph;

// Biến lưu trữ nút cuối cùng và nút được chọn
let lastNode: dia.Element | null = null;
let selectedNode: dia.Element | null = null;

interface INodeMap {
    KeyId: string | null,
    type: string | null,
    position: string | null
    label: string | null,
}
interface IDiagram {
    KeyId: string | null,
    source: string | null,
    lineAttributes: string | null,
    target: string | null
}
const request = ref({
    INodeMap: [] as INodeMap[],
    IDiagram: [] as IDiagram[]
});
let positions: { x: number; y: number }[] = [];


const emit = defineEmits<{
    (event: 'data-sent', data: boolean): void;
}>();


onMounted(() => {
    // Tạo Graph (model)
    graph = new dia.Graph({}, { cellNamespace: shapes });

    // Tạo Paper (hiển thị)
    paper = new dia.Paper({
        el: paperContainer.value!,
        model: graph,
        width: window.innerWidth, // Full window width
        height: window.innerHeight - 25, // Full window height
        gridSize: 10,
        drawGrid: true,
        overflow: true,
        cellViewNamespace: shapes
    });

    // Sự kiện chuột phải vào node
    paper.on('element:contextmenu', (elementView: dia.ElementView, evt: dia.Event, x: number, y: number) => {
        evt.preventDefault(); // Ngăn menu mặc định của trình duyệt
        // Lưu node được chọn
        selectedNode = elementView.model;
        // Hiển thị menu ngữ cảnh tại vị trí chuột
        contextMenuPosition.x = x + 300;
        contextMenuPosition.y = y + 50;
        isContextMenuVisible.value = true;
    });
});

// Hiển thị context menu tại vị trí click
const showContextMenu = (event: MouseEvent) => {
    event.preventDefault(); // Ngăn menu mặc định của trình duyệt
    //const paperWidth = paper.options.width;
    //const paperHeight = paper.options.height;

    contextMenuPosition.x = event.clientX;
    contextMenuPosition.y = event.clientY + 20;
    isContextMenuVisible.value = true;

};

// Ẩn context menu
const hideContextMenu = () => {
    isContextMenuVisible.value = false;
};

// Tạo nút (node) mới với loại hình vuông, hình thoi, hoặc hình bầu dục
const createNode = (type: string) => {
    let newNode;
    let titleNode = tenNode.value;
    if (type === 'vuong') {
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
    } else if (type === 'thoi') {
        // Tạo node hình thoi
        newNode = new shapes.standard.Polygon();
        newNode.attr({
            body: {

                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi
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
        // Tạo node hình thoi
        newNode = new shapes.standard.Polygon();
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi
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
        // Tạo node hình thoi
        newNode = new shapes.standard.Polygon();
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi
                fill: '#FF9AA2', // Màu nền
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
    } else if (type === 'bauduc') {
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

    newNode.attr({
        //body: { fill: 'orange' },
        //label: { text: type === 'tron' ? 'Hình vuông' : 'Hình thoi', fill: 'white' },
    });

    graph.addCell(newNode);

    // Tạo liên kết từ node được chọn hoặc node cuối cùng
    const sourceNode = selectedNode || lastNode;
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
                    text: ghichuNode.value, // Nội dung nhãn
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
    }
    lastNode = newNode;
    selectedNode = null;
    hideContextMenu();
};

// Xóa node đã chọn
const deleteNode = () => {
    if (selectedNode) {
        selectedNode.remove();
        selectedNode = null;
    } else {
        alert('Vui lòng chọn node cần xóa');
    }
    hideContextMenu();
};

// Cấu hình từng bước của quy trình
function CauHinhBuoc() {
    isPanelOpen.value = !isPanelOpen.value;
    hideContextMenu();
}

const saveNodes = () => {
    const nodes = graph.getElements();
    const nodeData = nodes.map((node, index) => {
        return {
            KeyId: String(node.id),
            type: String(node.get('type')) || null,
            position: JSON.stringify(node.position()),
            label: node.attr('label/text') || null,
        };
    });
    request.value.INodeMap = nodeData;
};


const saveLinks = () => {
    const links = graph.getLinks();
    const linkData = links.map((link) => {
        return {
            KeyId: link.id ? String(link.id) : null, // Chuyển ID sang string
            source: link.source().id ? String(link.source().id) : null, // Chuyển ID nguồn sang string
            target: link.target().id ? String(link.target().id) : null, // Chuyển ID đích sang string
            lineAttributes: link.attr('line') ? JSON.stringify(link.attr('line')) : null, // Thuộc tính đường liên kết
        };
    });
    request.value.IDiagram = linkData;
};

async function API_AddQuyTrinh() {
    try {
        saveNodes();
        saveLinks();
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
        dialog.value = false;
        createNode(typeText);
    }
    tenNode.value = "";
    ghichuNode.value = "";

}

function btnAddNode(type: string) {
    dialog.value = true;
    typeText = type;
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
</style>
