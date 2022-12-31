<template>
<div class="cukcuk-textarea">
    <label v-if="hasLable" for="" :style="styleLable">
        {{lableName}}
        <span v-if="isRequired" style="color: var(--color-error); margin-left: 4px;">
            (*)
        </span>
    </label>
    <textarea
        :tabindex="tabIndex"
        :style="styleInput"
        :id="lableName"
        @keyup="emitValue"
        :value="getAreaText"></textarea>
    <div v-if="notValidField" style="color: var(--color-error)">
        <!-- Khi người dùng nhập sai sẽ hiển thị -->
        {{messageInform}}
    </div>
</div>
</template>

<script>
export default {
    name: 'BaseTextArea',
    props: {
        tabIndex: String,
        notValidField: Boolean,     //True: hiển thị dòng valid, False ngược lại
        messageInform: String,      //Nội dung validate
        hasLable: Boolean,          //Có dung lable hay không
        isRequired: Boolean,        //Có Bắt buộc hay không
        lableName: String,          //Tên lable
        styleLable: Object,         //Style cho lable
        styleInput: Object,
        initVal: String,            //Giá trị ban đầu
    },
    methods: {
        emitValue(event) {
            this.$emit('keyUpEmitValue', event.target);
        },
    },
    computed:{
        getAreaText() {
            return this.initVal;
        },
    }
}
</script>

<style lang="css" scoped>
textarea {
    resize: unset;
}
.cukcuk-textarea {
    display: flex;
}

.cukcuk-textarea > label {
    display: flex;
    align-items: flex-start;
}

.cukcuk-textarea > textarea {
    padding: 5px 8px 5px 8px;
    border: 1px solid #cccccc;
    border-radius: 2px;
}

.cukcuk-textarea > textarea:focus {
    border: 1px solid #0072BC;
}
</style>
