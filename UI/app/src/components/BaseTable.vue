<template>
<div class="cukcuk-table" ref="base_table">
    <div class="cukcuk-thead-wrapper" ref="headScroll" :style="styleTHeadWrapper">
        <div class="cukcuk-thead" :style="styleTHead">
            <!-- thead -->
            <div v-for="itemTHead in theadArray" :key="itemTHead" :style="itemTHead.styleCol" class="cukcuk-thead-col">
                <div>
                    <!-- Tiêu đề -->
                    {{itemTHead.title}}
                </div>
                <div v-if="!noUseHeadCombobox">
                    <!-- CBX -->
                    <combo-box :cbxType="itemTHead.cbxType" :iconCbx="itemTHead.iconCbx" :styleBtn="itemTHead.styleBtn" :styleInput="itemTHead.styleInput" :styleCbx="itemTHead.styleCbx" :styleMenu="itemTHead.styleMenu" :cbxItems="itemTHead.cbxItems" :operatorInit="itemTHead.operatorInit"
                    :posItem="itemTHead.posItem" :fieldName="itemTHead.field" :useFilter="true"
                    :initVal="itemTHead.initVal"
                    :isReadonly="itemTHead.isReadonly"
                    :isTrueFalseField="itemTHead.isTrueFalseField"
                    :acceptOnlyNumber="itemTHead.acceptOnlyNumber"
                    @clickItemEmitValue="tableEmitValue"
                    @onBlurFixEmitValue="tableEmitValue"
                    @onEnterEmitValue="tableEmitValue">
                    </combo-box>
                </div>
            </div>
            <div class="header-scroll-box" ref="headScrollBox"></div>
        </div>
    </div>
    <div class="cukcuk-tbody-wrapper" ref="bodyScroll">
        <div class="cukcuk-tbody" ref="bodyContentHeight">
            <!-- tbody -->
            <div v-for="row in tbodyArray" :indexRow="tbodyArray.indexOf(row)" :key="row" class="cukcuk-tbody-row" @dblclick="dbClickRow">
                <!-- tbody-row -->
                <div v-for="cell in theadArray"
                    ref="table_cell" 
                    :indexRow="tbodyArray.indexOf(row)"
                    :key="cell" 
                    :typeCell="cell.typeValue"
                    :style="cell.styleCell" 
                    :id="row[`${tableName}ID`]"
                    :class="[getChoosenIdArray.includes(row[`${tableName}ID`]) && getChoosenIdArray != [] ? chosenRowClass : '']"
                    class="cukcuk-tbody-cell"
                    @click.exact="clickChooseRow"
                    @click.ctrl="clickChooseOneMoreRow"
                    @click.shift="clickChooseManyRows">
                    <!-- tbody-cell -->
                    <div v-if="cell.typeValue == 0"
                        :id="row[`${tableName}ID`]"
                        style="cursor: default;"
                        class="cukcuk-tbody-cell_value">
                        {{row[cell.field]}}
                    </div>
                    <div v-if="cell.typeValue == 1" 
                        :id="row[`${tableName}ID`]"
                        :class="fnGetDataCheckBox(row[cell.field])">
                    </div>
                    <div v-if="cell.typeValue != 0 && cell.typeValue != 1"
                        :id="row[`${tableName}ID`]"
                        style="cursor: default;"
                        class="cukcuk-tbody-cell_value">
                        {{row[cell.field]}}
                    </div>
                </div>
            </div>
        </div>
    </div>
    <!-- Loading here -->
    <base-loading v-if="getLoadingStatus"></base-loading>
    <!-- Dymanic CBX position fixed -->
    <div ref="cbx_dynamic" v-if="displayCbxInput">
        <combo-box ref="inputCbx"
            :canAutoFocus="true"
            :isReadonly="true"
            :style="{
                position: 'absolute',
                zIndex: '10',
                top: getCbxTopVal,
                left: getCbxLeftVal,
            }"
            :initVal="getCellValue"
            :cbxType="typeComboBox.DYMAMIC_DROPDOWN_VAL"
            :useSpanLi="cbxLiUseSpan"
            :iconCbx="`sprite-triangle-down`"
            :styleInput="{
                height: '28px',
            }"
            :styleCbx="{
                height: '28px',
                width: '479px',
                position: 'relative',
            }"
            :styleMenu="{
                width: '60px',
                top: '100%',
                height: 'fit-content',
                position: 'absolute',
                    
            }"
            :cbxItems="cbxItems"
            @clickItemEmitValue="emitClickItemCbx">
        </combo-box>
    </div>
    <base-input v-if="false"
        :canAutoFocus="true"
        :style="{
            position: 'fixed',
            top: getInputTopVal,
            left: getInputLeftVal,
            width: '242px',
        }"
        :styleInput="{
            height: '28px',
            width: '100%',
            textAlign: 'right',
        }"
        :acceptOnlyNumber="true">
    </base-input>
</div>
</template>

<script>
import ComboBox from './ComboBox.vue';
import BaseLoading from './BaseLoading.vue';
import {fnGetDataCheckBox, typeComboBox} from '../resource';
import BaseInput from './BaseInput.vue';
// import FormInputVue from '../views/dish/FormInput.vue';
export default {
    name: 'BaseTable',
    components: {
        ComboBox,
        BaseLoading,
        BaseInput,
    },
    data() {
        return {
            bodyArray: this.tbodyArray,
            fnGetDataCheckBox,
            chosenRowClass: 'cukcuk-chosen-row',
            typeComboBox,
            canInput: this.canInputRow,
            displayCbxInput: this.displayCbx, //Hien tri CBX
            fixedCbxTopVal: "0",
            fixedCbxLeftVal: "0",
            displayTextInput: false, //Hien tri CBX
            fixedInputTopVal: "0",
            fixedInputLeftVal: "0",
            currentIndex: 0,
            cellValue: "",
        }
    },
 
    props: {
        styleTHead: Object, //Obj style cho table header row
        styleTHeadWrapper: Object,  //Style thead wrapper
        noUseHeadCombobox: Boolean,   //Co dung combobox filter hay khong
        theadArray: Array, //Mảng chưa Obj có dạng { //Dùng cho table head
        //                          title: String,  //Tiêu đề bảng
        //                          styleCol: Obj,  //Style width
        //                          cbxType: number
        //                          ....các giá trị props của cbx
        tbodyArray: Array, //Mảng chưa các giá trị từng dòng cho body
        tableName: String, //Ten bang de lay gia tri id
        chosenIdArray: Array, //Mang chua ca gia tri duocj chon
        loadingStatus: Boolean, //Trang thai loading
        canInputRow: Boolean, //Co duoc nhap lieu tu ban phim hay khong
        cbxItems: Array, //Mang truyen vao dropdown CBX
        displayCbx: Boolean,//use Cbx or not
        cbxLiUseSpan: Boolean,
    },
    methods: {
        disableDropdown(event) {
            console.log(event.target);
        },
        /**
         * Su kien dbclick vao 1 dong
         * Nguyen Ba Hai - 27/12/2022
         */
        dbClickRow(event) {
            this.$emit("dbClickRow", event.target);
        },

        /**
         * Emit value cho filter box
         * Nguyen Ba Hai - 23/12/2022
         */
        tableEmitValue(value) {
            this.$emit("emitFilterObj",value);
        },

        /**
         * Emit value on click item of fixed dropdown
         * Nguyen Ba Hai - 27/12/2022
         */
        emitClickItemCbx(value) {
            this.$emit("clikEmitDynamicCbxVal", value);
        },

        /**
         * Click chon 1 dong trong table
         * Nguyen Ba Hai - 23/12/2022
         */
        clickChooseRow(event) {
            this.displayTextInput = false;
            this.displayCbxInput = false;
            this.$emit("clickChooseRow", event.target.id);
            if(this.canInput) {
                let overTop = document.getElementById("form-input").getBoundingClientRect().top
                let overLeft = document.getElementById("form-input").getBoundingClientRect().left
                let type = event.target.parentNode.getAttribute("typeCell");
                this.currentIndex = event.target.parentNode.getAttribute("indexRow");
                this.$emit("clickChooseCell", this.currentIndex);
                if(type == 4) {
                    //Hien thi CBX
                    this.fixedCbxTopVal = event.target.getBoundingClientRect().top - overTop;
                    this.fixedCbxLeftVal = event.target.getBoundingClientRect().left - overLeft;
                    this.cellValue = event.target.textContent;
                    this.displayCbxInput = true;
                } else if(type == 3) {
                    //Hien thi Input
                    this.fixedInputTopVal = event.target.getBoundingClientRect().top;
                    this.fixedInputLeftVal = event.target.getBoundingClientRect().left;
                    this.displayTextInput = true;
                }
            }
        },

        /**
         * Click chon them 1 dong trong table
         * Nguyen Ba Hai - 23/12/2022
         */
        clickChooseOneMoreRow(event) {
            this.$emit("clickChooseOneMoreRow", event.target.id);
        },

        /**
         * Click chon nhieu dong trong table
         * Nguyen Ba Hai - 23/12/2022
         */
        clickChooseManyRows(event) {
            this.$emit("clickChooseManyRows", event.target.id);
        },

        /**
         * Nhan emit blur value tu modified table
         * Nguyen Ba Hai- 22/12/2022
         */
        emitTableCbxModified() {
            // component.__vueParentComponent.attrs.style.display = 'none';
        },

        /**
         * Nhan emit blue value tu modified table
         * Nguyen Ba Hai- 22/12/2022
         */
        emitTableInputModified(value) {
            console.log(value);
        },
    },
    computed: {
        getTBodyArray() {
            return this.tbodyArray;
        },

        getChoosenIdArray() {
            return this.chosenIdArray;
        },

        getLoadingStatus() {
            return this.loadingStatus;
        },

        getCbxTopVal() {
            return `${this.fixedCbxTopVal-1}px`
        },

        getCbxLeftVal() {
            return `${this.fixedCbxLeftVal-1}px`
        },

        getInputDisplay() {
            return this.displayTextInput
        },

        getInputTopVal() {
            return `${this.fixedInputTopVal-1}px`
        },

        getInputLeftVal() {
            return `${this.fixedInputLeftVal}px`
        },

        getCellValue() {
            return this.cellValue;
        },
    },
    mounted() {
        let scrollHeader = this.$refs.headScroll;
        let scrollBody = this.$refs.bodyScroll;
        let bodyHeight = this.$refs.bodyContentHeight;
        let headBox = this.$refs.headScrollBox;
        //Đồng bộ scroll header và scroll body
        scrollBody.addEventListener('scroll', function (event) {
            scrollHeader.scrollLeft = event.target.scrollLeft;
            if (bodyHeight.scrollHeight > scrollBody.clientHeight) {
                headBox.style.display = "block";
            }
        });
        window.addEventListener("click", (e) => {
          if (!this.$el.contains(e.target)) {
            this.displayCbxInput = false;
          }
        });
    },
    created() {
        // close the options if click out the component
    },
}
</script>

<style lang="css" scoped>
.header-scroll-box {
    display: none;
    color: #f5f5f5;
    height: 100%;
    width: 100px;
    background-color: #f5f5f5;
    border-left: 1px solid #cccccc;
    border-bottom: 1px solid #cccccc;
}

.cukcuk-table {
    display: flex;
    background-color: #f5f5f5;
    flex-direction: column;
    height: 100%;
    /* width: 100%; */
    width: calc(100vw -246px);
    overflow: hidden;
}

.cukcuk-thead-wrapper {
    width: 100%;
    display: flex;
    overflow: hidden;
    border-right: 1px solid #cccccc;
    border-left: 1px solid #cccccc;
}

.cukcuk-thead {
    display: flex;
    /* height: 85px; */
}

.cukcuk-tbody-wrapper {
    /* display: none; */
    position: relative;
    height: calc(100vh - 250px);
    width: 100%;
    overflow: auto;
    border-right: 1px solid #ccc;
    border-left: 1px solid #cccccc;
    border-bottom: 1px solid #cccccc;
}

.input_table, .cbx_table{
    display: none;
}

.cukcuk-tbody {
    display: flex;
    flex-direction: column;
}

.cukcuk-tbody-row {
    display: flex;
    height: 28px;
    z-index: 0;
}

.cukcuk-tbody-row:nth-child(even) > div {
    background-color: #f5f5f5;
}

.cukcuk-tbody-row:nth-child(odd) > div {
    background-color: #fff;
}

.cukcuk-tbody-row:hover > div {
    background-color: #d0e1ec;
}

.cukcuk-chosen-row {
    background-color: #c1ddf1 !important;
}

.cukcuk-tbody-cell {
    display: flex;
    position: relative;
    z-index: -1;
    align-items: center;
    justify-content: flex-start;
    height: 100%;
    border-bottom: 1px solid #cccccc;
    flex-shrink: 0;
}

.cukcuk-tbody-cell_value {
    height: 100%;
    width: 100%;
    padding: 5px 10px 4px 10px;
}

.cukcuk-thead-col {
    display: flex;
    flex-direction: column;
    padding: 2px;
    background-color: #ededed;
    flex-shrink: 0;
    border-bottom: 1px solid #cccccc;
}

.cukcuk-thead-col:hover {
    background-color: #f5f5f5;
}

.cukcuk-thead-col>div:first-child {
    display: flex;
    justify-content: center;
    align-items: center;
    flex: 1;
    width: 100%;
}

.cukcuk-thead-col>div:last-child {
    display: flex;
    justify-content: center;
    align-items: center;
    height: 26px;
    width: 100%;
}
</style>
