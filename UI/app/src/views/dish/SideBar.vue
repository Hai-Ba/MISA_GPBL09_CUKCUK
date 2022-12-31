<template>
<div class="sidebar">
    <side-bar-component v-for="item in items" 
        :key="item.id"
        :id="item.id"
        :iconSidebarItem="item.icon"
        :titleSidebarItem="item.title"
        :isChoosenItem="item.isClicked"
        @clickOnItem="onChangeMenu">
    </side-bar-component>
</div>
</template>

<script>
import SideBarComponent from '../../components/SideBarComponent.vue';
export default {
    name: 'SideBar',
    components: {
        SideBarComponent,
    },
    data() {
        return {
            items: [ //Các giá trị truyền vào menu của Sidebar
                {
                    icon: "sprite-menu",
                    title: "Tổng quan",
                    isClicked: false,
                    id: 1
                },
                {
                    icon: "sprite-menu",
                    title: "Mua hàng",
                    isClicked: false,
                    id: 2
                },
                {
                    icon: "sprite-menu",
                    title: "Thực đơn",
                    isClicked: true,
                    id: 3
                },
            ],
        }
    },
    methods: {
        /**
         * Bắt sự kiện click thay đổi menu ở SideBar
         * Nguyễn Bá Hải - 12/12/2022
         */
        onChangeMenu(data) {
            this.items.forEach(ele => {
                ele.isClicked = false;
                if(data.id == ele.id) { //Đặt class active là true cho giá trị được chọn
                    ele.isClicked = true;
                }
            });
        },
    },
}
</script>

<style lang="css" scoped>
.sidebar {
    background-color: var(--color-normal);
    height: 100%;
    width: 230px;
    border-top: 1px solid #016087;
}
</style>
