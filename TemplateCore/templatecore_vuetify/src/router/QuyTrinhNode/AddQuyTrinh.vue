<template>
    <div>
        <btn class="btnAdd btn" @click="AddQuyTrinh">Thêm quy trình</btn>
    </div>
    <div>
        <div ref="paperContainer" class="diagram-container" @contextmenu.prevent="showContextMenu"></div>

        <!-- Context Menu -->
        <div v-if="isContextMenuVisible" :style="contextMenuStyle" class="context-menu">
            <ul>
                <li @click="createNode('square')">Tạo hình vuông</li>
                <li @click="createNode('diamond')">Tạo hình thoi</li>
                <li @click="createNode('ellipse')">Tạo hình bầu dục</li>
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
    top: `${contextMenuPosition.y}px`,
    left: `${contextMenuPosition.x}px`,
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

onMounted(() => {
    // Tạo Graph (model)
    graph = new joint.dia.Graph();

    // Tạo Paper (hiển thị)
    paper = new joint.dia.Paper({
        el: paperContainer.value!,
        model: graph,
        width: 2500, // Full window width
        height: 2500, // Full window height
        gridSize: 10,
        drawGrid: true,
    });

    // Sự kiện chuột phải vào node
    paper.on('element:contextmenu', (elementView: joint.dia.ElementView, evt: joint.dia.Event, x: number, y: number) => {
        evt.preventDefault(); // Ngăn menu mặc định của trình duyệt

        // Lưu node được chọn
        selectedNode = elementView.model;

        // Hiển thị menu ngữ cảnh tại vị trí chuột
        contextMenuPosition.x = x;
        contextMenuPosition.y = y;
        isContextMenuVisible.value = true;
    });
});

// Hiển thị context menu tại vị trí click
const showContextMenu = (event: MouseEvent) => {
    event.preventDefault(); // Ngăn menu mặc định của trình duyệt
    isContextMenuVisible.value = true;
    contextMenuPosition.x = event.clientX;
    contextMenuPosition.y = event.clientY;
};

// Ẩn context menu
const hideContextMenu = () => {
    isContextMenuVisible.value = false;
};

// Tạo nút (node) mới với loại hình vuông, hình thoi, hoặc hình bầu dục
const createNode = (type: 'square' | 'diamond' | 'ellipse') => {
    if (!paper) return;

    let newNode;
    if (type === 'square') {
        // Tạo node hình vuông
        newNode = new joint.shapes.standard.Rectangle();
        newNode.resize(100, 100); // Kích thước hình vuông
    } else if (type === 'diamond') {
        // Tạo node hình thoi
        newNode = new joint.shapes.standard.Polygon();
        newNode.attr({
            body: {
                refPoints: '50,0 100,50 50,100 0,50', // Hình dạng thoi
                fill: 'orange',
            },
            label: { text: 'Hình thoi', fill: 'white' },
        });
        newNode.resize(100, 100);
    } else if (type === 'ellipse') {
        // Tạo node hình bầu dục (ellipse)
        newNode = new joint.shapes.standard.Ellipse();
        newNode.attr({
            body: {
                fill: 'lightblue', // Màu nền hình bầu dục
            },
            label: { text: 'Hình bầu dục', fill: 'black' },
        });
        newNode.resize(120, 60); // Kích thước hình bầu dục (tùy chỉnh)
    }

    if (!newNode) return;

    newNode.position(
        contextMenuPosition.x - (paper.options.el as HTMLElement).offsetLeft,
        contextMenuPosition.y - (paper.options.el as HTMLElement).offsetTop
    );

    newNode.attr({
        body: { fill: 'orange' },
        label: { text: type === 'square' ? 'Hình vuông' : 'Hình thoi', fill: 'white' },
    });

    graph.addCell(newNode);

    // Tạo liên kết từ node được chọn hoặc node cuối cùng
    const sourceNode = selectedNode || lastNode;
    if (sourceNode) {
        const link = new joint.shapes.standard.Link();
        link.source(sourceNode);
        link.target(newNode);
        link.attr({
            line: { stroke: 'blue', strokeWidth: 2 },
        });

        graph.addCell(link);
    }

    // Cập nhật node cuối cùng
    lastNode = newNode;

    // Xóa trạng thái của selectedNode
    selectedNode = null;

    hideContextMenu();

    // Sau khi tạo node, lưu lại thông tin các node và các liên kết
    saveNodes();
    saveLinks();
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
    saveNodes();
    saveLinks();
};

// Hàm lưu các node và in ra console thông tin
const saveNodes = () => {
    // Lấy tất cả các node trong graph
    const nodes = graph.getElements();

    // Lấy thông tin của các node, bao gồm vị trí và loại hình
    const nodeData = nodes.map((node) => {
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
    console.log('Danh sách các liên kết:', linkData);
};
async function AddQuyTrinh() {
    try {
        let responseData = await callAuthenticationAPI('/api/quanlythongtin/QuyTrinhNode/AddQuyTrinhNode', 'POST', {
            lsdiagram: request.value.IDiagram,
            lsnodes: request.value.INodeMap,
            MaQuyTrinh: "123",
            TenQuyTrinh: "123",
            UserId: "dsads",
            UserName: "dsads"
        }, {
            timeout: 15000
        });
        console.log(request.value.INodeMap);
    } catch (error) {
        console.log(error);
    }
}
</script>

<style scoped>
.diagram-container {
    background: linear-gradient(0deg, transparent 9%, #ddd 10%) 0% 0%,
        linear-gradient(90deg, transparent 9%, #ddd 10%) 0% 0%;
    background-size: 10px 10px;
    position: relative;
    overflow: auto;
}

.context-menu {
    position: absolute;
    z-index: 1000;
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
    padding: 8px 12px;
    cursor: pointer;
}

.context-menu li:hover {
    background-color: #eee;
}
</style>
