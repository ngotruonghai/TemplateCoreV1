<template>
    <div id="bieudoquytrinh">
        <div ref="paperContainer" class="diagram-container" @contextmenu.prevent="showContextMenu"></div>

        <!-- Context Menu -->
        <div v-if="isContextMenuVisible" :style="contextMenuStyle" class="context-menu">
            <ul>
                <li @click="createNode('bauduc')" style=" color: #0066CC;">
                    <span class="icon-border2" style=" color: #0066CC;">
                        <i class="fas fa-circle"></i>
                    </span>
                    Bắt đầu
                </li>
                <li @click="createNode('thoi')" style=" color: #FF9933;">
                    <span class="icon-border2" style=" color: #FF9933;">
                        <i class="fas fa-exclamation-triangle"></i>
                    </span>
                    Điều kiện
                </li>
                <li @click="createNode('vuong')" style=" color: #0099CC;">
                    <span class="icon-border2" style=" color: #0099CC;">
                        <i class="fas fa-square"></i>
                    </span>
                    Bước
                </li>
                <li @click="createNode('thoi_ketthuc')" style=" color: #009966;">
                    <span class="icon-border2" style=" color: #009966;">
                        <i class="fas fa-check"></i>
                    </span>
                    Duyệt
                </li>
                <li @click="createNode('thoi_tuchoi')" style=" color: #DD0000;">
                    <span class="icon-border2" style=" color: #DD0000;">
                        <i class="fas fa-times"></i>
                    </span>
                    Từ chối
                </li>
                <li>Chỉnh sửa</li>
                <li @click="deleteNode">Xóa node</li>
                <li @click="hideContextMenu">Hủy</li>
            </ul>
        </div>
    </div>
</template>

<script lang="ts" setup>
import { ref, reactive, onMounted, computed } from 'vue';
import * as joint from 'jointjs';
import { callAuthenticationAPI } from "@/providers/data-provider";

// Tham chiếu tới container của sơ đồ
const paperContainer = ref<HTMLDivElement | null>(null);

// Quản lý trạng thái của context menu
const isContextMenuVisible = ref(false);
const contextMenuPosition = reactive({ x: 0, y: 0 });

const contextMenuStyle = computed(() => ({
    top: `${contextMenuPosition.y - 150}px`,
    left: `${contextMenuPosition.x - 260}px`,
}));

// Khởi tạo các thành phần của JointJS
let paper: joint.dia.Paper;
let graph: joint.dia.Graph;

// Biến lưu trữ nút cuối cùng và nút được chọn
let lastNode: joint.dia.Element | null = null;
let selectedNode: joint.dia.Element | null = null;

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

onMounted(() => {
    // Tạo Graph (model)
    graph = new joint.dia.Graph();

    // Tạo Paper (hiển thị)
    paper = new joint.dia.Paper({
        el: paperContainer.value!,
        model: graph,
        width: 2500, // Full window width
        height: window.innerHeight - 40, // Full window height
        gridSize: 10,
        drawGrid: true,
    });

    // Sự kiện chuột phải vào node
    paper.on('element:contextmenu', (elementView: joint.dia.ElementView, evt: joint.dia.Event, x: number, y: number) => {
        evt.preventDefault(); // Ngăn menu mặc định của trình duyệt
        // Lưu node được chọn
        selectedNode = elementView.model;
       

        // Hiển thị menu ngữ cảnh tại vị trí chuột
        contextMenuPosition.x = x + 230;
        contextMenuPosition.y = y + 3400;
        isContextMenuVisible.value = true;
    });
});

// Hiển thị context menu tại vị trí click
const showContextMenu = (event: MouseEvent) => {
    //event.preventDefault(); // Ngăn menu mặc định của trình duyệt

    const paperWidth = paper.options.width;
    const paperHeight = paper.options.height;


    isContextMenuVisible.value = true;
    contextMenuPosition.x =  event.clientX;
    contextMenuPosition.y = event.clientY;
};

// Ẩn context menu
const hideContextMenu = () => {
    isContextMenuVisible.value = false;
};

// Tạo nút (node) mới với loại hình vuông, hình thoi, hoặc hình bầu dục
const createNode = (type: 'vuong' | 'tron' | 'bauduc' | 'thoi' | 'thoi_ketthuc' | 'thoi_tuchoi') => {
    if (!paper) return;

    let newNode;
    if (type === 'vuong') {
        // Tạo node hình vuông
        newNode = new joint.shapes.standard.Rectangle();
        newNode.attr({
            body: {
                fill: '#0099CC', // Màu nền
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: 'Bước',
                fill: 'black',
                refY: 90, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(80, 80); // Kích thước hình vuông
    } else if (type === 'thoi') {
        // Tạo node hình thoi
        newNode = new joint.shapes.standard.Polygon();
        newNode.attr({
            body: {

                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi
                fill: '#FF9933', // Màu nền
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: 'Điều kiện',
                fill: 'black',
                refY: 110, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(100, 100);
    } else if (type === 'thoi_ketthuc') {
        // Tạo node hình thoi
        newNode = new joint.shapes.standard.Polygon();
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi
                fill: '#009966', // Màu nền
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: 'Kết thúc',
                fill: 'black',
                refY: 110, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(100, 100);
    }
    else if (type === 'thoi_tuchoi') {
        // Tạo node hình thoi
        newNode = new joint.shapes.standard.Polygon();
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi
                fill: '#DD0000', // Màu nền
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: 'Từ chối',
                fill: 'black',
                refY: 110, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(100, 100);
    } else if (type === 'bauduc') {
        // Tạo node hình bầu dục (ellipse)
        newNode = new joint.shapes.standard.Ellipse();
        newNode.attr({
            body: {
                fill: '#0066CC', // Màu nền hình bầu dục
                stroke: 'black',  // Màu viền
                strokeWidth: 1, // Độ dày viền
            },
            label: {
                text: 'Bắt đầu',
                fill: 'black',
                refY: 90, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(100, 80);
    } else if (type === 'tron') {
        newNode = new joint.shapes.standard.Circle();
        newNode.attr({
            body: {
                fill: 'lightblue', // Màu nền hình bầu dục
            },
            label: {
                text: 'Bắt đầu',
                fill: 'black',
                refY: 90, // Đặt vị trí văn bản phía dưới (1.5 = dưới phần node)
            },
        });
        newNode.resize(80, 80);
    }

    if (!newNode) return;

    newNode.position(
        contextMenuPosition.x - (paper.options.el as HTMLElement).offsetLeft - 225,
        contextMenuPosition.y - (paper.options.el as HTMLElement).offsetTop - 135
    );
    const x = contextMenuPosition.x - (paper.options.el as HTMLElement).offsetLeft;
    const y = contextMenuPosition.y - (paper.options.el as HTMLElement).offsetTop;

    newNode.attr({
        //body: { fill: 'orange' },
        //label: { text: type === 'tron' ? 'Hình vuông' : 'Hình thoi', fill: 'white' },
    });

    graph.addCell(newNode);

    // Tạo liên kết từ node được chọn hoặc node cuối cùng
    const sourceNode = selectedNode || lastNode;
    if (sourceNode) {
        const link = new joint.shapes.standard.Link();
        link.source(sourceNode);
        link.target(newNode);
        link.attr({
            line: { stroke: 'black', strokeWidth: 2 },
        });

        graph.addCell(link);
    }

    // Cập nhật node cuối cùng
    lastNode = newNode;

    // Xóa trạng thái của selectedNode
    selectedNode = null;

    hideContextMenu();

    // Sau khi tạo node, lưu lại thông tin các node và các liên kết
    //saveNodes(x, y);
    //saveLinks();
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

    // Sau khi xóa node, lưu lại thông tin các node và các liên kết
    //saveNodes();
    //saveLinks();
};

// Hàm lưu các node và in ra console thông tin
const saveNodes = () => {
    // Lấy tất cả các node trong graph
    const nodes = graph.getElements();
    // Lấy thông tin của các node, bao gồm vị trí và loại hình
    const nodeData = nodes.map((node, index) => {
        return {
            KeyId: String(node.id), // Chuyển đổi ID thành chuỗi
            type: String(node.get('type')) || null, // Chuyển đổi type thành chuỗi hoặc null
            position: JSON.stringify(node.position()),//`${position.x}@${position.y}`, // Tọa độ x@y
            label: node.attr('label/text') || null, // Tên hiển thị
        };
    });
    request.value.INodeMap = nodeData;
};

// Hàm lưu các liên kết (links) giữa các node
const saveLinks = () => {
    // Lấy tất cả các liên kết trong graph
    const links = graph.getLinks();

    // Lấy thông tin của các liên kết, bao gồm nguồn, đích và các thuộc tính liên quan
    const linkData = links.map((link) => {
        return {
            KeyId: link.id ? String(link.id) : null, // Chuyển ID sang string
            source: link.source().id ? String(link.source().id) : null, // Chuyển ID nguồn sang string
            target: link.target().id ? String(link.target().id) : null, // Chuyển ID đích sang string
            lineAttributes: link.attr('line') ? JSON.stringify(link.attr('line')) : null, // Thuộc tính đường liên kết
        };
    });
    request.value.IDiagram = linkData;

    // In ra console thông tin các liên kết
};
async function AddQuyTrinh() {
    try {
        saveNodes();
        saveLinks();
        let responseData = await callAuthenticationAPI('/api/quanlythongtin/QuyTrinhNode/AddQuyTrinhNode', 'POST', {
            lsdiagram: request.value.IDiagram,
            lsnodes: request.value.INodeMap,
            MaQuyTrinh: "",
            TenQuyTrinh: "",
            UserId: "",
            UserName: localStorage.getItem("UserName")
        }, {
            timeout: 15000
        });
    } catch (error) {
        console.log(error);
    }
}
</script>

<style scoped>
#bieudoquytrinh {
    position: relative;
    overflow: auto;
    height: 100vh;
    box-shadow: 0 0 0 1.5px rgba(0, 0, 0, 0.3);
    padding: 0px;
}

.diagram-container {
    background: linear-gradient(0deg, transparent 9%, #ddd 10%) 0% 0%,
        linear-gradient(90deg, transparent 9%, #ddd 10%) 0% 0%;
    background-size: 10px 10px;
    /* position: relative; */
    /* overflow: auto; */
}

.context-menu {
    position: absolute;
    z-index: 2;
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
</style>
