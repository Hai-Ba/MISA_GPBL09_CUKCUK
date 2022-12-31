<template>
<div class="cukcuk-sidebar-comp" 
    ref="item"
    :class="[isChoosenItem ? activeItem : inActiveItem]"
    @click.self="onCompClicked">
    <div @click.self="onCompClicked">
        <!-- Icon -->
        <div :class="iconSidebarItem" @click.self="onCompClicked"></div>
    </div>
    <div @click.self="onCompClicked">
        <!-- Title -->
        <div @click.self="onCompClicked">{{titleSidebarItem}}</div>
    </div>
</div>
</template>

<script>
export default {
    name: 'SideBarComponent',
    props: {
        iconSidebarItem: String, //Tên class icon Sidebar
        titleSidebarItem: String, //Tên title Sidebar
        isChoosenItem: Boolean, //Giá trị true khi ma item đc chọn
    },
    data() {
        return {
            activeItem: "active-class-item",
            inActiveItem: "inactive-class-item",
            // isClicked: this.isChoosenItem,
        }
    },
    methods: {
        onCompClicked() {
            this.$emit('clickOnItem', this.$refs.item)
        }
    },
}
</script>

<style lang="css" scoped>
.active-class-item {
    background-color: var(--color-active);
}

.inactive-class-item {
    background-color: var(--color-normal);
}

.cukcuk-sidebar-comp {
    display: flex;
    height: 40px;
    width: 100%;
    border-bottom: 1px solid red;
    color: #fff;
    align-items: center;
    padding-left: 12px;
    border-bottom: 1px solid #016087;
    border-top: 1px solid #038ac1;
    /* background-color: var(--hover-sidebar-color); */
    cursor: pointer;
}

.cukcuk-sidebar-comp>div:first-child {
    padding-right: 10px;
}

.cukcuk-sidebar-comp:hover {
    background-color: var(--hover-sidebar-color);
}
</style>
