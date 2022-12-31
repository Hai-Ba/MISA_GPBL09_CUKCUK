<template>
    <div v-if="getDialogState" class="dialog-wrapper">
        <div class="dialog-container">
            <div class="dialog-header">
                <!-- Tieu de dialog -->
                <div>{{getDialogTitle}}</div>
                <div></div>
            </div>
            <div class="dialog-content">
                <!-- Noi dung dialog -->
                <div :class="getDialogIcon"></div>
                <div>{{getDialogContent}}</div>
            </div>
            <div class="dialog-footer">
                <div v-if="getDialogOneButton">
                    <!-- Co -->
                    <base-button
                        :tabIndex="`1`"
                        :useText="true"
                        :text="`Có`"
                        :isAutoFocus="true"
                        :styleBtn="{
                            width: '75px'
                        }"
                        @clickBtn="clickYesBtn">
                    </base-button>
                </div>
                <div v-if="getDialogTwoButton">
                    <!-- Khong -->
                    <base-button
                        :tabIndex="`2`"
                        :useText="true"
                        :text="`Không`"
                        :styleBtn="{
                            width: '75px'
                        }"
                        @clickBtn="clickNoBtn">
                    </base-button>
                </div>
                <div v-if="getDialogThreeButton">
                    <!-- Huy bo -->
                    <base-button
                        :tabIndex="`3`"
                        :useText="true"
                        :text="getDialogButtonText"
                        :styleBtn="{
                            width: '75px'
                        }"
                        @clickBtn="clickCancelBtn">
                    </base-button>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import BaseButton from '../../components/BaseButton.vue';
import {dishModule} from '../../store/mutationTypes';
import {dishAction} from '../../store/actionTypes';
import {dialogType, ruleValidate} from '../../resource';
export default {
    name: 'BaseDialog',
    components: {
        BaseButton,
    },
    data() {
        return {
            
        }
    },
    mounted() {
        // Focus vao o co neu la 3 nut
    },
    methods: {
        /**
         * Bat su kien nhan nut Co
         * Nguyen Ba Hai - 16/12/2022
         */
        async clickYesBtn() {
            switch(this.getDialogType) {
                case dialogType.DELETE_ONE:
                    this.$store.commit(dishModule.SET_DIALOG_STATE,false);
                    //Goi dispatch xoa 
                    await this.$store.dispatch(dishAction.DELETE_RECORD);
                    this.displayPageNumber();
                break;
                case dialogType.DELETE_MANY:
                    this.$store.commit(dishModule.SET_DIALOG_STATE,false);
                    //Goi dispatch xoa 
                    await this.$store.dispatch(dishAction.DELETE_RECORD);
                    this.displayPageNumber();
                break;
                case dialogType.ADD_ONE:
                    this.$store.commit(dishModule.SET_DIALOG_STATE,false);
                    this.addRecordAndClose();
                break;
                case dialogType.UPDATE_ONE:
                    this.$store.commit(dishModule.SET_DIALOG_STATE,false);
                    this.addRecordAndClose();
                break;
                default: break;
            }
            //Dong
        },

        /**
         * Nat su kien nhan nut khong
         * Nguyen Ba Hai - 16/12/2022
         */
        clickNoBtn() {
            switch(this.getDialogType) {
                case dialogType.DELETE_ONE:
                break;
                case dialogType.DELETE_MANY:
                break;
                case dialogType.ADD_ONE:
                    this.$store.commit(dishModule.SET_FORM_STATE,false); 
                    this.$store.dispatch(dishAction.GET_PAGE_DATA)
                break;
                case dialogType.UPDATE_ONE:
                    this.$store.commit(dishModule.SET_FORM_STATE,false);
                    this.$store.dispatch(dishAction.GET_PAGE_DATA)
                break;
                default: break;
            }
            //Dong
            this.$store.commit(dishModule.SET_DIALOG_STATE,false);
        },

        /**
         * Bat su kien nhan nut huy bo
         * Nguyen Ba Hai - 16/12/2022
         */
        clickCancelBtn() {
            this.$store.commit(dishModule.SET_DIALOG_STATE,false);
        },

        /**
         * Ham validate check trung HObby
         * Nguyen Ba Hai - 28/12/2022
         */
        checkExistDuplicateHobbies(array) {
            let duplicateArr = [];
            for(let i = 0; i < array.length; i++) {
                for(let j = i + 1; j < array.length; j++) {
                    if(JSON.stringify(array[i]) === JSON.stringify(array[j])) {
                        let isExist = false;
                        duplicateArr.forEach(ele => {
                            if(JSON.stringify(ele) === JSON.stringify(array[i])) {
                                isExist = true;
                            }
                        });
                        if(!isExist) {
                            duplicateArr.push(array[i]);
                        }
                    }
                }
            }
            return duplicateArr;
        },

        /**
         * HIen thi dialog khi them moi hay sua that bai
         * Nguyen Ba Hai - 28/12/2022
         */
        displayDialogWhenInsertFail(duplicateArr) {
            //HIne dialog canh bao dien
            this.$store.commit(dishModule.SET_DIALOG_STATE,true);
            this.$store.commit(dishModule.SET_DIALOG_THREE_BUTTON,true);
            this.$store.commit(dishModule.SET_DIALOG_TITLE,"CUKCUK - Quản lí nhà hàng");
            this.$store.commit(dishModule.SET_DIALOG_ICON,"sprite-warning");
            this.$store.commit(dishModule.SET_DIALOG_TWO_BUTTON,false);
            this.$store.commit(dishModule.SET_DIALOG_ONE_BUTTON,false);
            this.$store.commit(dishModule.SET_DIALOG_BUTTON_TEXT,"Đồng ý");
            let stringDialog = "";
            //Xet hobby xem co bi trung khong
            if(duplicateArr.length > 0) {
                duplicateArr.forEach(ele => {
                    stringDialog = stringDialog + ele.hobbyName + " - " + ele.priceAdd + ", ";
                });
                stringDialog = stringDialog.substring(0, stringDialog.length - 2);
                this.$store.commit(dishModule.SET_DIALOG_CONTENT,`Sở thích phục vụ <${stringDialog}> đã bị trùng, vui lòng kiểm tra lại.`);
                this.$store.commit(dishModule.SET_FORM_TAB, "serveHobby");
            } else {
                if(this.$store.state.dish.isDuplicateCode) {
                    this.$store.commit(dishModule.SET_DIALOG_CONTENT,`Mã <${this.getDishObject.dishCode}> đã tồn tại trong hệ thống!`);
                }
                else {
                    this.$store.commit(dishModule.SET_DIALOG_CONTENT,"Còn trường trống hoặc chưa đúng định dạng!");
                }
                this.$store.commit(dishModule.SET_FORM_TAB, "generalInfo");
                this.validateForm();
            }
            //reset bien check trung ma trong store
            this.$store.commit(dishModule.IS_DUPLICATE_CODE, 0);
        },

        /**
         * Bat su kien them moi va dong form
         * Nguyen Ba Hai-16/12/2022
         */
        async addRecordAndClose() {
            //Check trung ma code
            if(this.$store.state.dish.dishObj.dishCode) {
                if(this.$store.state.dish.recentDishCode == ""){//Them moi
                    await this.$store.dispatch(dishAction.CHECK_DUPLICATE_CODE);
                }else {
                    if(this.$store.state.dish.recentDishCode != this.$store.state.dish.dishObj.dishCode){//Check trung ma khi sua ban ghi
                        await this.$store.dispatch(dishAction.CHECK_DUPLICATE_CODE);
                    }
                }
            }
            //Format object
            this.$store.commit(dishModule.FORMAT_DISH_OBJECT);//Nhung truong nao null se loai bo
            //Validate cac truong
            this.$store.commit(dishModule.CHECK_FORM_SUBMIT);
            let duplicateArr = [];
            duplicateArr = this.checkExistDuplicateHobbies(this.$store.state.dish.hobbyArray.filter(ele => JSON.stringify(ele) != JSON.stringify({})));
            //Bat dau validate
            if(!this.$store.state.dish.cannotAddOrUpdate && duplicateArr.length == 0){//Co the them duoc
                await this.$store.dispatch(dishAction.INSERT_OR_UPDATE_RECORD);
                //Dong form
                this.$store.commit(dishModule.SET_FORM_STATE, false);
            } else { //Khong them duoc va hien dialog
                this.displayDialogWhenInsertFail(duplicateArr);
            }
        },

        /**
         * Validate form
         * Nguyen Ba Hai - 26/12/2022
         */
        validateForm() {
            //Duyet qua tung input
            let listInput = document.querySelectorAll("#general-form input");
            listInput.forEach(element => {
                let component = element.__vueParentComponent;//Choc vao component va hien thi loi
                if(component.data.rules)
                {
                    if(component.data.rules.includes(ruleValidate.REQUIRED)){
                        if(!element.value){
                            component.data.notValid = true;
                            component.data.messageValid = "Trường này không được để trống"
                        }
                        else{//Khi co gia tri value o input
                            if(component.data.rules.includes(ruleValidate.UNIQUE)) { //Check trung ma
                                if(this.$store.state.dish.isDuplicateCode) {
                                    component.data.notValid = true;
                                    component.data.messageValid = "Mã code đã bị trùng"
                                }
                            }
                        }
                    }
                }
            });
            //Validate so thich phuc vu
        },

        /**
         * Hien thi so trang khi xoa het ban ghi 1 trang
         * Nguyen Ba Hai- 30/12/2022
         */
        displayPageNumber() {
            let input_component = document.querySelector("#input-page-number input");
            input_component.value = this.$store.state.dish.pageNumber;
        }
    },
    computed: {
        getDialogState() {
            return this.$store.state.dish.dialogState;
        },

        getDialogTitle() {
            return this.$store.state.dish.dialogTitle;
        },

        getDialogContent() {
            return this.$store.state.dish.dialogContent;
        },

        getDialogThreeButton() {
            return this.$store.state.dish.dialogThreeButton;
        },

        getDialogType() {
            return this.$store.state.dish.dialogType;
        },

        getDialogIcon() {
            return this.$store.state.dish.dialogIcon;
        },

        getDialogTwoButton() {
            return this.$store.state.dish.dialogTwoButton;
        },

        getDialogButtonText() {
            return this.$store.state.dish.dialogButtonText;
        },

        getDialogOneButton() {
            return this.$store.state.dish.dialogOneButton;
        }
    },
}
</script>
<style lang="css" scoped>
    .dialog-wrapper {
        position: fixed;
        top: 0;
        left: 0;
        width: 100vw;
        height: 100vh;
        z-index: 100;
        background-color: rgba(10, 10, 10, .4) !important;
    }

    .dialog-container {
        display: flex;
        flex-direction: column;
        position: absolute;
        top: 50%;
        left: 50%;
        width: 400px;
        min-height: 123px;
        background-color: #fff;
        transform: translate(-50%, -50%);
        border-bottom: 5px solid #0072bc!important;
        border-right: 5px solid #0072bc!important;
        border-left: 5px solid #0072bc!important;
        border-radius: 2px;
    }

    .dialog-header {
        display: flex;
        align-items: center;
        justify-content: space-between;
        height: 30px;
        width: 100%;
        color: #fff;
        background-color: #0072bc!important;
    }

    .dialog-content {
        flex: 1;
        width: 100%;
        padding: 10px;
        display: flex;
    }

    .dialog-content > div:last-child {
        margin-left: 12px;
        flex: 1;
    }

    .dialog-footer {
        width: 100%;
        height: 36px;
        padding: 6px;
        display: flex;
        justify-content: flex-end;
    }

    .dialog-footer > div {
        padding-right: 8px;
    }
</style>