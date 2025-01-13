<template>
    <div>
        <button @click="test">sdsads</button>
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
import { callAuthenticationAPI } from '@/providers/data-provider';

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
interface DiagramNode {
    keyId: string;
    source: string;
    lineAttributes: Record<string, any>; // Dùng Record nếu dữ liệu là JSON
    danhSachQuyTrinhId: number;
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
    target: string;
}

interface Node {
    keyId: string;
    type: string;
    position: string; // Dữ liệu vị trí dạng JSON
    label: string;
    danhSachQuyTrinhId: number;
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
}

interface QuyTrinh {
    maQuyTrinh: string;
    tenQuyTrinh: string;
    userId: string;
    userName: string;
    diagramNodes: DiagramNode[];
    nodes: Node[];
    id: number;
    createdBy: string;
    created: string;
    lastModifiedBy: string | null;
    lastModified: string | null;
}

interface ApiResponse<T> {
    succeeded: boolean;
    code: number;
    message: string | null;
    errors: any;
    data: T;
}

// Khởi tạo responseData với kiểu ApiResponse<QuyTrinh[]>
const responseData = ref<ApiResponse<QuyTrinh[]>>({
    succeeded: false,
    code: 0,
    message: null,
    errors: null,
    data: [], // Khởi tạo với danh sách rỗng
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
    LoadDataAPI();
});


async function LoadDataAPI() {
    try {
        const response = await callAuthenticationAPI('/api/quanlythongtin/QuyTrinhNode/GetQuyTrinhById?QuyTringId=50', 'GET', {}, { timeout: 15000 });
        responseData.value = response as ApiResponse<QuyTrinh[]>; // Gán trực tiếp nếu dùng reactive
        console.log(responseData.value.data);
        mapNodesFromAPI();
    } catch (error) {

    }
}
function test() {
    logAllNodes();
}

const mapNodesFromAPI = () => {
    paper.setOrigin(0, 0);
    if (!responseData.value.data) return;

    responseData.value.data.forEach((quyTrinh) => {
        quyTrinh.nodes.forEach((node) => {
            const newNode = new joint.shapes.standard.Rectangle();

            const position = JSON.parse(node.position) as { x: number; y: number };

            // Lấy vị trí của paper container và giấy
            const paperRect = paper.viewport.getBoundingClientRect();
            const containerRect = paperContainer.value?.getBoundingClientRect();
            if (containerRect && paperRect) {
                const offsetX = containerRect.left ;
                const offsetY = containerRect.top;

                // Điều chỉnh vị trí node
                newNode.position(position.x, position.y);

            }

            newNode.resize(120, 60);
            newNode.attr({
                body: { fill: 'lightblue' },
                label: { text: node.label, fill: 'black' },
            });
            newNode.set('id', node.keyId);
            graph.addCell(newNode);
        });

        quyTrinh.diagramNodes.forEach((diagramNode) => {
            const link = new joint.shapes.standard.Link();
            link.source({ id: diagramNode.source });
            link.target({ id: diagramNode.target });

            const lineAttributes =
                typeof diagramNode.lineAttributes === 'string'
                    ? JSON.parse(diagramNode.lineAttributes)
                    : diagramNode.lineAttributes;

            link.attr({
                line: {
                    ...lineAttributes,
                    stroke: lineAttributes.stroke || 'blue',
                    strokeWidth: lineAttributes.strokeWidth || 2,
                },
            });

            graph.addCell(link);
        });
    });
};


const createNode = (type: 'square' | 'diamond' | 'ellipse') => {
    if (!paper) return;

    let newNode;
    if (type === 'square') {
        newNode = new joint.shapes.standard.Rectangle();
        newNode.resize(100, 100);
    } else if (type === 'diamond') {
        newNode = new joint.shapes.standard.Polygon();
        newNode.attr({
            body: { refPoints: '50,0 100,50 50,100 0,50', fill: 'orange' },
            label: { text: 'Hình thoi', fill: 'white' },
        });
        newNode.resize(100, 100);
    } else if (type === 'ellipse') {
        newNode = new joint.shapes.standard.Ellipse();
        newNode.attr({ body: { fill: 'lightblue' }, label: { text: 'Hình bầu dục', fill: 'black' } });
        newNode.resize(120, 60);
    }

    if (!newNode) return;

    // Tính toán vị trí node từ context menu
    const containerRect = paperContainer.value?.getBoundingClientRect();
    if (containerRect) {
        const offsetX = containerRect.left + window.scrollX;
        const offsetY = containerRect.top + window.scrollY;

        newNode.position(
            contextMenuPosition.x - offsetX,  // Điều chỉnh vị trí theo container
            contextMenuPosition.y - offsetY
        );
    }

    graph.addCell(newNode);

    const sourceNode = selectedNode || lastNode;
    if (sourceNode) {
        const link = new joint.shapes.standard.Link();
        link.source(sourceNode);
        link.target(newNode);
        link.attr({ line: { stroke: 'blue', strokeWidth: 2 } });

        graph.addCell(link);
    }

    lastNode = newNode;
    selectedNode = null;
    hideContextMenu();

    // Lưu lại node và link sau khi tạo
    saveNodes();
    saveLinks();
};



const logAllNodes = () => {
    const cells = graph.getCells(); // Lấy tất cả các cell trong graph
    const nodes = cells.filter(cell => cell.isElement()); // Lọc ra các node (loại Element)

    console.log('Danh sách các node và tọa độ của chúng:');
    nodes.forEach(node => {
        const id = node.id; // ID của node
        const position = node.position(); // Lấy tọa độ x, y của node
        const label = node.attr('label/text'); // Lấy nhãn (label) của node

        console.log(`ID: ${id}, Label: ${label}, Position: (${position.x}, ${position.y})`);
    });
};
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
            id: node.id, // ID của node
            type: node.get('type'), // Loại hình của node (hình vuông, hình thoi, hình bầu dục)
            position: node.position().toString(), // Vị trí của node (x, y)
            label: node.attr('label/text'), // Label của node (nếu có)
        };
    });

    // In ra console thông tin các node
    console.log('Danh sách các node:', nodeData);
};

// Hàm lưu các liên kết (links) giữa các node
const saveLinks = () => {
    // Lấy tất cả các liên kết trong graph
    const links = graph.getLinks();

    // Lấy thông tin của các liên kết, bao gồm nguồn, đích và các thuộc tính liên quan
    const linkData = links.map((link) => {
        return {
            id: link.id, // ID của liên kết
            source: link.source().id, // ID của node nguồn
            target: link.target().id, // ID của node đích
            lineAttributes: JSON.stringify(link.attr('line')), // Các thuộc tính của đường liên kết (màu, độ dày, v.v.)
        };
    });

    // In ra console thông tin các liên kết
    console.log('Danh sách các liên kết:', linkData);
};


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
