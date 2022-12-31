<template>
    <div class="form-serve_hobby">
        <div class="mg-bottom8">Món ăn: <b>{{getDishObject.dishName}}</b></div>
        <div style="display: flex; flex-direction: row;" class="mg-bottom8">
            <div class="sprite-question mg-right10"></div>
            <i>Ghi lại các sở thích của khách hàng giúp nhân viên phục vụ chọn nhanh order.<br/>VD: không cay/ít hành/thêm phomai...</i>
        </div>

        <div>
            <!-- BANG -->
            <base-table :style="{height: '214px', width: '100%',backgroundColor:'#FFF'}"
                :noUseHeadCombobox="true"
                
                :styleTHeadWrapper="{
                    borderBottom: '1px solid #ccc'
                }"
                :styleTHead="{
                    borderTop: '1px solid #ccc',
                    height: '100px'
                }"
                :theadArray="theadArray"
                :tbodyArray="getTbodyArray"
                :chosenIdArray="[]"
                :canInputRow="true"
                :cbxItems="hobbyArr"
                :cbxLiUseSpan="true"
                :displayCbx="getCbxDisplay"
                @clickChooseCell="clickCell"
                @clikEmitDynamicCbxVal="clickCbxItemSetValue">
            </base-table>
        </div>

        <div style="display: flex; flex-direction: row; padding: 2px" >
            <!-- NUT THEM DONG VA XOA DONG -->
            <base-button
                :useText="true"
                :text="`Thêm dòng`"
                :useClassPic="true"
                :picUrl="`sprite-add-row`"
                :styleBtn="{
                    marginRight: '5px',
                }"
                @clickBtn="addTableRow">
            </base-button>
            <base-button
                :useText="true"
                :text="`Xóa dòng`"
                :useClassPic="true"
                :picUrl="`sprite-delete-row`"
                @clickBtn="deleteTableRow">
            </base-button>
        </div>
    </div>
</template>
<script>
import BaseButton from '../../../components/BaseButton.vue'
import BaseTable from '../../../components/BaseTable.vue'
import {fixedHobby} from '../../../resource'
import { dishModule } from '../../../store/mutationTypes'
export default {
    name: 'ServeHobby',
    components: {
        BaseButton,
        BaseTable,
    },
    data() {
        return {
            currentRowIdx: 0,//XOa dong duoc chon
            hobbyArr: fixedHobby,
            theadArray:[
                {
                    title: "Sở thích phục vụ",
                    field: "hobbyName",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '480px',
                        height: '29px',
                        borderRight: '1px solid #CCCCCC',
                        borderLeft: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '480px',
                        borderRight: '1px solid #CCCCCC',
                        borderLeft: '1px solid #CCCCCC',
                    },
                    typeValue: 4,
                },
                {
                    title: "Thu thêm",
                    field: "priceAdd",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '242px',
                        height: '29px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '242px',
                        borderRight: '1px solid #CCCCCC',
                        textAlign: 'right',
                    },
                    typeValue: 3,
                },
            ],
            displayCbx: false,

            tbodyArray: [
                {
                    // serveHobbyName: "Tuong ca",
                    // priceAdd: "12000",
                },
            ],
        }
    },

    methods: {
        /**
         * Ham them 1 dong cua bang so thich phuc vu
         * Nguyen Ba Hai - 23/12/2022
         */
        addTableRow() {
            this.tbodyArray = this.$store.state.dish.hobbyArray;
            this.tbodyArray.push({});
            this.$store.commit(dishModule.SET_HOBBY_ARRAY, this.tbodyArray);
        },

        /**
         * Ham them 1 dong cua bang so thich phuc vu
         * Nguyen Ba Hai - 23/12/2022
         */
        deleteTableRow() {
            this.tbodyArray = this.$store.state.dish.hobbyArray;
            if(this.currentRowIdx < 0) {
                this.tbodyArray.pop();
            } else {
                this.tbodyArray.splice(this.currentRowIdx,1);//Loai bo dong duoc chon
                this.currentRowIdx = -1;//Dat lai xoa tu duoi len tren
            }
            this.$store.commit(dishModule.SET_HOBBY_ARRAY, this.tbodyArray);
        },

        /**
         * Ham lay index cua row 
         */
        clickCell(value) {
            this.currentRowIdx = value;
        },

        clickCbxItemSetValue(value) {
            this.tbodyArray = this.$store.state.dish.hobbyArray;
            let idHobby = value.idValue
            let hobby = this.hobbyArr.find(hob => hob.id == idHobby);
            this.tbodyArray[this.currentRowIdx].hobbyID = hobby.id;
            this.tbodyArray[this.currentRowIdx].hobbyName = hobby.name;
            this.tbodyArray[this.currentRowIdx].priceAdd = hobby.price;
            //Lay gia tri bang ID va truyen vao bang trong store
            this.$store.commit(dishModule.SET_HOBBY_ARRAY, this.tbodyArray);
        },
    },

    computed: {
        getDishObject(){
            return this.$store.state.dish.dishObj;
        },

        getComponent(){
            return this.$store.state.dish.componentChoose;
        },

        getTbodyArray() {
            return this.$store.state.dish.hobbyArray;
        },

        getCbxDisplay() {
            return this.displayCbx;
        }
    },
}

</script>
<style lang="css" scoped>
    .form-serve_hobby{
        display: flex;
        flex-direction: column;
    }
</style>