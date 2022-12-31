<template>
    <button
     class="cukcuk-btn"
     :autofocus="isAutoFocus"
     :class="[isDisable ? classBtnDisable : '']"
     :style="styleBtn"
     :title="titleBtn"
     @click="onClickBtn">
        <!-- Icon svg -->
        <div v-if="useClassPic" :style="styleIcon">
            <div :class="picUrl"></div>
        </div>
        <!-- Icon background png, jpeg,.. -->
        <div v-if="useBackgroundPic" :style="styleIcon"></div>
        <!-- Text -->
        <div v-if="useText" style="white-space: nowrap">{{text}}</div>
    </button>
</template>
<script>
export default {
    name: 'BaseButton',
    props: {
        isAutoFocus: Boolean,
        tabIndex: String,
        styleBtn: Object,           //Style Động cho button(height, width, color, hover,...)
        styleIcon: Object,          //Style động cho icon
        useClassPic: Boolean,       //Dùng icon là .svg
        picUrl: String,             //Url của hình ảnh
        useBackgroundPic: Boolean,  //Dùng icon là background picture (.png, .jpg, .jpge,...)
        useText: Boolean,           //Có text hoặc không
        text: String,               //Nếu useText là true thì truyền props [text] vào
        titleBtn: String,           //Gợi ý khi người dùng hover vào
        isDisable: Boolean,         //Nút bị disable

    },
    methods: {
        /**
         * Bat su kien click vao button
         * Nguyen ba hai - 17/12/2022
         */
        onClickBtn() {
            this.$emit('clickBtn');
        },
    },
    data() {
        return {
            classBtnDisable: "cukcuk-btn-disable",
        }
    },
}
</script>
<style lang="css" scoped>
    .cukcuk-btn {
        /* Style mặc định */
        display: flex;
        justify-content: center;
        align-items: center;
        height: 25px;
        background-color: #FAFAFA;
        border: 1px solid #CCCCCC;
        border-radius: 2px;
        padding: 0 10px;
        cursor: pointer;
    }

    .cukcuk-btn:hover {
        background-color: #E4F2EA !important;
        border: 1px solid #0072BC !important;
        background-image: none;
    }

    .cukcuk-btn-disable {
        opacity: 30%;
        background-color: #FAFAFA !important;
        border: none;
        cursor: default;
        /* Ngat su kien cho button */
        pointer-events: none;
    }

    .cukcuk-btn:active {
        background-color: #E4F2EA !important;
        border: 1px solid #0072BC !important;
        background-image: none;
    }
</style>