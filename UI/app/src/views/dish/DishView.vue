<template>
<div class="wrapper">
    <header-bar></header-bar>
    <div class="body-wrapper">
        <side-bar></side-bar>
        <dish-content></dish-content>
    </div>
    <form-input></form-input>
    <dish-dialog></dish-dialog>
</div>
</template>

<script>
import HeaderBar from './HeaderBar.vue';
import SideBar from './SideBar.vue';
import DishContent from './DishContent.vue';
import FormInput from './FormInput.vue';
import DishDialog from './DishDialog.vue'
import {dishAction} from '../../store/actionTypes';

export default {
    name: 'DishView',
    components: {
        HeaderBar,
        SideBar,
        DishContent,
        FormInput,
        DishDialog,
    },
    mounted() {
        //Goi store lay du lieu
        this.$store.dispatch(dishAction.GET_PAGE_DATA);
    },
}
</script>

<style lang="css" scoped>
.wrapper {
    display: flex;
    position: relative;
    flex-direction: column;
}

.body-wrapper {
    display: flex;
    /* flex: 1; */
    height: calc(100vh - 55px);
}
</style>
