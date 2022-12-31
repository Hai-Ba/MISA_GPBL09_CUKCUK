<template>
<div v-if="getFormState" class="form-wrapper" >
    <div class="form-container" id="form-input">
        <div class="form-header">
            <div>
                <!-- Tiêu đề form -->
                {{getFormTitle}}
            </div>
            <i class="icofont-close-circled closed-form-btn" @click="closeForm"></i>
        </div>
        <div class="form-body">
            <div class="form-body_upper">
                <span style="width: 45px">Loại</span>
                <combo-box 
                    :cbxType="dataTopCombobox.cbxType"         
                    :canAutocomplete="dataTopCombobox.canAutocomplete" :isReadonly="dataTopCombobox.isReadonly" 
                    :iconCbx="dataTopCombobox.iconCbx" 
                    :initVal="getDishObject.dishTypeName" 
                    :styleInput="dataTopCombobox.styleInput" 
                    :styleCbx="dataTopCombobox.styleCbx" 
                    :styleMenu="dataTopCombobox.styleMenu" 
                    :cbxItems="dataTopCombobox.cbxItems" 
                    :posItem="dataTopCombobox.posItem"
                    :lableName="dataTopCombobox.lableName"
                    @clickItemEmitValue="getEmittedValue">
                </combo-box>
            </div>
            <div class="form-body_bottom">
                <div class="form-body_bottom_tab">
                    <!-- TAB form -->
                    <button @click="openGeneralInfo" 
                        :class="[getFormTabsState.generalInfo? activeTab : '']">Thông tin chung</button>
                    <button @click="openServeHobby" 
                        :class="[getFormTabsState.serveHobby? activeTab : '']">Sở thích phục vụ</button>
                </div>

                <div class="form-body_bottom_content" id="general-form">
                    <general-info v-if="getFormTabsState.generalInfo" ></general-info>
                    <serve-hobby v-if="getFormTabsState.serveHobby"></serve-hobby>
                </div>

                <div class="form-body_bottom_footer">
                    <!-- FOOTEr form -->
                    <a href="https://help.cukcuk.com/vi/1060100_them_mon_an.htm" target="_blank" class="form-body_bottom_footer_left">
                        <!-- Giúp -->
                        <base-button :useText="true" :text="`Giúp`" :useClassPic="true" :picUrl="`sprite-help`" :styleIcon="{
                                marginRight: '6px',
                            }" />
                    </a>
                    <div class="form-body_bottom_footer_right">
                        <div>
                            <!-- Cất -->
                            <base-button :useText="true" 
                            tabIndex="9"
                            :text="`Cất`" 
                            :useClassPic="true" 
                            :picUrl="`sprite-save`" 
                            :styleIcon="{
                                marginRight: '6px',
                            }" :styleBtn="{
                                marginRight: '8px',
                                width: '86px',
                            }" 
                            @clickBtn="addRecordAndClose"/>
                        </div>
                        <div>
                            <!-- Cất & thêm -->
                            <base-button :useText="true" 
                            :text="`Cất & Thêm`" 
                            :useClassPic="true" 
                            :picUrl="`sprite-save-many`" 
                            :styleIcon="{
                                marginRight: '6px',
                            }" 
                            :styleBtn="{
                                marginRight: '8px',
                                width: '123px',
                            }" 
                            @clickBtn="addRecordAndRemain"/>
                        </div>
                        <div>
                            <!-- Hủy bỏ -->
                            <base-button :useText="true" :text="`Hủy bỏ`" :useClassPic="true" :picUrl="`sprite-cancel`" :styleIcon="{
                                marginRight: '6px',
                            }" :styleBtn="{
                                width: '91px',
                            }" 
                            @clickBtn="closeFormDirectly"/>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import BaseButton from '../../components/BaseButton.vue';
import ComboBox from '../../components/ComboBox.vue';
import GeneralInfo from './forms/GeneralInfo.vue';
import ServeHobby from './forms/ServeHobby.vue';
import {constants} from '../../constants/Dish.constants'
import {
    typeComboBox, ruleValidate, fixedDishType
} from '../../resource';
import {
    dishModule
} from '../../store/mutationTypes';
import { dishAction } from '../../store/actionTypes';
export default {
    el: '#form-input',
    name: 'FormInput',
    components: {
        BaseButton,
        ComboBox,
        GeneralInfo,
        ServeHobby,
    },
    mounted() {
         this._keyListener = function(e) {
            if (e.key === "Escape") {
                e.preventDefault(); 
                //Close form
                if(this.$store.state.dish.formState == true) 
                {
                    this.closeForm();
                }
            }
        };
        document.addEventListener('keydown', this._keyListener.bind(this));
    },
    unmounted() {
        document.removeEventListener('keydown', this._keyListener);
    },
    data() {
        return {
            //Trang thai loading
            loadingStatus: false,
            //Trang thai dialog
            dialogShow: false,
            constants: constants,
            //Dieu kien Validate
            ruleValidate: ruleValidate, 
            //Prop truyền vào combobox bên trai
            dataTopCombobox: {
                cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                canAutocomplete: "on",
                isReadonly: true,
                iconCbx: "sprite-triangle-down",
                styleInput: {
                    height: '24px',
                },
                lableName: constants.DISH_TYPE,
                styleCbx: {
                    width: '115px',
                    height: '24px',
                    position: 'relative',
                    borderRadius: '2px'
                },
                styleMenu: {
                    top: '100%',
                    width: '115px',
                    position: 'absolute',
                },
                posItem: 0,
                cbxItems: fixedDishType,
            },

            //Tab focus khi form hien len
            activeTab: "tab_chosen",
        }
    },
    computed: {
        getFormState() {
            return this.$store.state.dish.formState;
        },

        getFormTitle() {
            return this.$store.state.dish.formTitle;
        },

        getDialogType() {
            return this.$store.state.dish.dialogType;
        },

        getFormTabsState() {
            return this.$store.state.dish.formTab;
        },

        getDishObject(){
            return this.$store.state.dish.dishObj;
        },

        getStartValidate() {
            return this.validate;
        }

    },
    methods: {
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
         * Bat su kien them moi va dong form
         * Nguyen Ba Hai-16/12/2022
         */
        async addRecordAndRemain() {
            //Check trung ma code
            if(this.$store.state.dish.dishObj.dishCode) {
                if(this.$store.state.dish.recentDishCode == ""){//Them moi
                    await this.$store.dispatch(dishAction.CHECK_DUPLICATE_CODE);
                }else {
                    console.log(this.$store.state.dish.recentDishCode);
                    console.log(this.$store.state.dish.dishObj.dishCode);
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
                //Reset du lieu
                this.$store.commit(dishModule.SET_HOBBY_ARRAY, [{}]);
                this.$store.commit(dishModule.SET_FORM_TAB, "generalInfo");
                this.$store.commit(dishModule.SET_DISH_OBJECT_VAL, {
                    dishTypeID: "13a507d0-342d-6292-6de3-2ceaa9cf3765",
                    dishTypeName: "Món ăn",
                });
                //reset form
            } else {//Khong them duoc va hien dialog
                this.displayDialogWhenInsertFail(duplicateArr);
            }
        },

        /**
         * Bat su kien dong form
         * Nguyen Ba Hai-16/12/2022
         */
        closeForm() {
            //Mo DIALOG confirm
            this.$store.commit(dishModule.SET_DIALOG_STATE,true);
            this.$store.commit(dishModule.SET_DIALOG_THREE_BUTTON,true);
            this.$store.commit(dishModule.SET_DIALOG_TWO_BUTTON,true);
            this.$store.commit(dishModule.SET_DIALOG_ONE_BUTTON,true);
            this.$store.commit(dishModule.SET_DIALOG_TITLE,"CUKCUK - Quản lí nhà hàng");
            this.$store.commit(dishModule.SET_DIALOG_CONTENT,"Dữ liệu đã thay đổi, bạn có muốn cất không?");
            this.$store.commit(dishModule.SET_DIALOG_BUTTON_TEXT,"Hủy bỏ");
            this.$store.commit(dishModule.SET_DIALOG_ICON,"sprite-question");
        },

        /**
         * Dong form khong hoi han gi
         * Nguyen Ba Hai - 28/12/2022
         */
        closeFormDirectly() {
            this.$store.commit(dishModule.SET_FORM_STATE,false);
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
         * Mo form thong tin chug
         * Nguyen Bas Hai - 23/12/2022
         */
        openGeneralInfo() {
            this.$store.commit(dishModule.SET_FORM_TAB,"generalInfo");
        },

        /**
         * Mo form thong tin phuc vu
         * Nguyen Bas Hai - 23/12/2022
         */
        openServeHobby() {
            this.$store.commit(dishModule.SET_FORM_TAB,"serveHobby");
        },

        /**
         * Su kien thay doi loai mon an
         * Nguyen Ba Hai - 25/12/2022
         */
        getEmittedValue(component) {
            let dishObj = this.$store.state.dish.dishObj;
            //Dung id de so sanh
            //value de lay du lieu
            switch(component.id) {
                case constants.DISH_TYPE:
                    dishObj.dishTypeID = component.idValue; 
                    dishObj.dishTypeName = component.value; 
                break;
            }
            //Mutate gia tri cua dish obj trong store
            this.$store.commit(dishModule.SET_DISH_OBJECT_VAL, dishObj);
        },
    },
}
</script>

<style lang="css" scoped>
.form-wrapper {
    position: absolute;
    height: 100vh;
    width: 100vw;
    background-color: rgba(10, 10, 10, .4) !important;
}

.form-container {
    position: absolute;
    height: 626px;
    display: flex;
    flex-direction: column;
    width: 750px;
    background-color: #FFF;
    top: 50%;
    left: 50%;
    transform: translate(-50%, -50%);
    border-radius: 2px;
    /* border: 5px solid #0072bc!important; */
}

.form-header {
    display: flex;
    justify-content: space-between;
    align-items: center;
    padding: 0 4px;
    height: 30px;
    background-color: #0072bc !important;
    color: #FFF;
    border: 5px solid #0072bc !important;
    border-top-left-radius: 2px;
    border-top-right-radius: 2px;
}

.form-body {
    flex: 1;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    border-bottom: 5px solid #0072bc !important;
    border-left: 5px solid #0072bc !important;
    border-right: 5px solid #0072bc !important;
}

.form-body_upper {
    width: 100%;
    height: 40px;
    padding: 8px;
    display: flex;
    align-items: center;
    justify-content: flex-start;
    background-color: #e9e9e9 !important;
}

.form-body_bottom {
    flex: 1;
    display: flex;
    flex-direction: column;
    padding: 8px;
}

.form-body_bottom_tab {
    display: flex;
    justify-content: flex-start;
    width: 100%;
    height: 30px;
    padding: 0 1px;
    background-color: #f5f5f5;
    overflow: hidden;
}

.form-body_bottom_tab>button {
    display: flex;
    align-items: center;
    background-color: #f5f5f5;
    padding: 0 8px;
    margin: 0 1px;
}

.form-body_bottom_tab>button:hover {
    border-top: 2px solid #0072bc !important;
    box-shadow: 0 1px 1px 1px #d3d3d3 !important;
    background-color: #D7E9F4;
}

.tab_chosen{
    border-top: 2px solid #0072bc !important;
    box-shadow: 0 1px 1px 1px #d3d3d3 !important;
    background-color: #FFFFFF !important;
}

.form-body_bottom_tab>button:focus {
    border-top: 2px solid #0072bc !important;
    box-shadow: 0 1px 1px 1px #d3d3d3 !important;
    background-color: #FFFFFF;
    color: #0072bc !important;
    ;
}

.form-body_bottom_content {
    flex: 1;
    padding-top: 8px;
    margin-bottom: 8px;
}



.form-body_bottom_footer {
    display: flex;
    justify-content: space-between;
    width: 100%;
    height: 25px;
}

.form-body_bottom_footer_right {
    display: flex;
}
</style>
