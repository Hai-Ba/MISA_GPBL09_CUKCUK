<template>
    <div class="form-general_content">
        <base-loading v-if="getLoadingStatus"></base-loading>
        <div class="form-left">
            <div>
                <!-- Ten mon -->
                <!-- CONTENT form -->
                <base-input
                    :inputType="`text`"
                    :hasLable="true"
                    :lableName="constants.DISH_NAME"
                    :canAutoFocus="true"
                    :isRequired="true"
                    :styleLable="{
                        width: '125px'
                    }"
                    :styleError="{
                        paddingLeft: '125px'
                    }"
                    :styleInput="{
                        flex: '1',
                    }"
                    tabIndex="1"
                    :initVal="getDishObject.dishName"
                    :ruleSets="[ruleValidate.REQUIRED, ruleValidate.FORMATTING]"
                    @keyUpEmitValue="getEmittedValue"
                    @blurEmitValue="blurInputGenCode">
                </base-input>
            </div>
            <div ref="dish_code">
                <!-- Ma mon -->
                <base-input
                    :inputType="`text`"
                    :hasLable="true"
                    tabIndex="2"
                    :lableName="constants.DISH_CODE"
                    :isRequired="true"
                    :styleLable="{
                        width: '125px'
                    }"
                    :styleError="{
                        paddingLeft: '125px'
                    }"
                    :styleInput="{
                        flex: '1',
                    }"
                    :initVal="getDishObject.dishCode"
                    :ruleSets="[ruleValidate.REQUIRED, ruleValidate.FORMATTING, ruleValidate.UNIQUE]"
                    @keyUpEmitValue="getEmittedValue"
                    @blurEmitValue="blurValidate">
                </base-input>
            </div>
            <div>
                <!-- Nhom thuc don -->
                <combo-box
                    tabIndex="3"
                    :cbxType="menuCategoryCbx.cbxType"
                    :canAutocomplete="menuCategoryCbx.canAutocomplete" 
                    :isReadonly="menuCategoryCbx.isReadonly" 
                    :iconCbx="menuCategoryCbx.iconCbx" 
                    :initVal="getDishObject.categoryName"
                    :hasLable="menuCategoryCbx.hasLable"
                    :hasAddBtn="menuCategoryCbx.hasAddBtn"
                    :styleLable="menuCategoryCbx.styleLable"
                    :lableName="constants.MENU_CATEGORY"
                    :styleInput="menuCategoryCbx.styleInput"
                    :styleCbx="menuCategoryCbx.styleCbx"
                    :styleMenu="menuCategoryCbx.styleMenu"
                    :cbxItems="menuCategoryCbx.cbxItems"
                    :posItem="menuCategoryCbx.posItem"
                    @clickItemEmitValue="getEmittedValue"
                    @keyUpEmitValue="getEmittedValue"
                    @blurEmitValue="getCbxDynBlurNoRule">
                </combo-box>
            </div>
            <div>
                <!-- Don vi tinh -->
                <combo-box
                    tabIndex="4"
                    :cbxType="unitCbx.cbxType"
                    :canAutocomplete="unitCbx.canAutocomplete" 
                    :isReadonly="unitCbx.isReadonly" 
                    :iconCbx="unitCbx.iconCbx" 
                    :initVal="getDishObject.unitName"
                    :isRequired="unitCbx.isRequired"
                    :hasLable="unitCbx.hasLable"
                    :hasAddBtn="unitCbx.hasAddBtn"
                    :styleLable="unitCbx.styleLable"
                    :lableName="constants.UNIT"
                    :styleInput="unitCbx.styleInput"
                    :styleCbx="unitCbx.styleCbx"
                    :styleMenu="unitCbx.styleMenu"
                    :cbxItems="unitCbx.cbxItems"
                    :posItem="unitCbx.posItem"
                    :styleError="unitCbx.styleError"
                    :ruleSets="[ruleValidate.REQUIRED]"
                    @clickItemEmitValue="getEmittedValue"
                    @blurEmitValue="blurValidate"
                    @keyUpEmitValue="getEmittedValue">
                </combo-box>
            </div>
            <div>
                <!-- Gia ban -->
                <base-input
                    tabIndex="5"
                    :inputType="`text`"
                    :hasLable="true"
                    :lableName="constants.SALE_PRICE"
                    :isRequired="true"
                    :styleLable="{
                        width: '125px',
                    }"
                    :styleError="{
                        paddingLeft: '125px'
                    }"
                    :styleInput="{
                        width: '150px',
                        textAlign: 'right',
                    }"
                    :acceptOnlyNumber="true"
                    :isMoneyData="true"
                    :initVal="getDishObject.salePrice ? getDishObject.salePrice : '0,00'"
                    :ruleSets="[ruleValidate.REQUIRED, ruleValidate.FORMATTING]"
                    @keyUpEmitValue="getEmittedValue"
                    @blurEmitValue="blurValidate">
                </base-input>
            </div>
            <div>
                <!-- Gia von -->
                <base-input
                    tabIndex="6"
                    :inputType="`text`"
                    :hasLable="true"
                    :lableName="constants.PRIME_COST"
                    :styleLable="{
                        width: '125px',
                    }"
                    :styleInput="{
                        width: '150px',
                        textAlign: 'right',
                    }"
                    :styleError="{
                        paddingLeft: '125px'
                    }"
                    :acceptOnlyNumber="true"
                    :isMoneyData="true"
                    :ruleSets="[ruleValidate.FORMATTING]"
                    :initVal="getDishObject.primeCost ? getDishObject.primeCost : '0,00'"
                    @keyUpEmitValue="getEmittedValue">
                </base-input>
            </div>
            <div>
                <!-- Mo ta -->
                <base-text-area
                    tabindex="7"
                    :hasLable="true"
                    :lableName="constants.DESCRIPTION"
                    :styleLable="{
                        width: '125px',
                    }"
                    :styleInput="{
                       flex: '1',
                       height: '50px', 
                    }"
                    :initVal="getDishObject.description"
                    @keyUpEmitValue="getEmittedValue">
                </base-text-area>
            </div>
            <div>
                <!-- Che bien tai -->
                <combo-box
                    tabIndex="8"
                    :cbxType="cookPlaceCbx.cbxType"
                    :canAutocomplete="cookPlaceCbx.canAutocomplete" 
                    :isReadonly="cookPlaceCbx.isReadonly" 
                    :iconCbx="cookPlaceCbx.iconCbx" 
                    :initVal="getDishObject.cookPlaceName"
                    :hasLable="cookPlaceCbx.hasLable"
                    :hasAddBtn="cookPlaceCbx.hasAddBtn"
                    :styleLable="cookPlaceCbx.styleLable"
                    :lableName="constants.COOK_PLACE"
                    :styleInput="cookPlaceCbx.styleInput"
                    :styleCbx="cookPlaceCbx.styleCbx"
                    :styleMenu="cookPlaceCbx.styleMenu"
                    :cbxItems="cookPlaceCbx.cbxItems"
                    :posItem="cookPlaceCbx.posItem"
                    @clickItemEmitValue="getEmittedValue"
                    @blurEmitValue="getCbxDynBlurNoRule">
                </combo-box>
            </div>
            <div>
                <!-- Hien thi tren thuc don -->
                <base-check-box
                    :styleCheckBox="{
                        marginLeft: '125px',
                    }"
                    :hasLable="true"
                    :checkBoxValue="getDishObject.showOnMenu"
                    :lableName="constants.SHOW_ON_MENU"
                    @clickEmitValue="getEmittedValue">
                </base-check-box>
            </div>
        </div>
        <div class="form-right">
            <!-- Chèn ảnh, hiển thị ảnh -->
            <div class="form-right_img">
                <div class="form-right_img_content">
                    <!-- Display pic -->
                </div>
                <div class="form-right_img_text">
                    Chọn các ảnh có định dạng <br><b>(.jpg, .jpeg, .png, .gif)</b>
                </div>
            </div>
            <div class="form-right_btn">
                <!-- Button add pic -->
                <base-button
                    :useText="true"
                    :text="`...`"
                    :styleBtn="{
                        height: '25px',
                        width: '100%',
                    }">
                </base-button>
                <!-- Button delete pic -->
                <base-button
                    :useClassPic="true"
                    :picUrl="`sprite-close-pic`"
                    :styleBtn="{
                        height: '25px',
                        width: '100%',
                    }">
                </base-button>
            </div>
            <div class="form-right_symbolbtn">
                <!-- Button biểu tượng -->
                <base-button
                    :useText="true"
                    :text="`Biểu tượng`"
                    :useClassPic="true"
                    :picUrl="`sprite-pen`"
                    :styleBtn="{
                        height: '100%',
                        width: '100%',
                    }"
                    :styleIcon="{
                        marginRight: '6px',
                    }">
                </base-button>
            </div>
            <div class="form-right_lable">{{constants.DISH_IMAGE}}</div>
        </div>
    </div>
</template>
<script>
import {constants} from '../../../constants/Dish.constants';
import {ruleValidate, typeComboBox} from '../../../resource';
import BaseCheckBox from '../../../components/BaseCheckBox.vue';
import ComboBox from '../../../components/ComboBox.vue';
import BaseInput from '../../../components/BaseInput.vue';
import BaseButton from '../../../components/BaseButton.vue';
import BaseTextArea from '../../../components/BaseTextArea.vue';
import BaseLoading from '../../../components/BaseLoading.vue'
import {dishModule} from '../../../store/mutationTypes';
import {fixedCategory, fixedUnit, fixedCookPlace} from '../../../resource';
import { dishAction } from '../../../store/actionTypes';
import {fnGetCategoryID, fnGetUnitID, fnGetCookPlaceID} from '../../../utils/Dish.function'
export default {
    name: 'GeneralInfo',
    components:{
        BaseCheckBox,
        ComboBox,
        BaseInput,
        BaseButton,
        BaseTextArea,
        BaseLoading,
    },
    data() {
        return {
            validateField: this.validate,
            constants: constants,
            ruleValidate: ruleValidate,
            //Prop truyền vào combobox Nhoms thuc don
            menuCategoryCbx: {
                cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                canAutocomplete: "on",
                isReadonly: false,
                hasLable: true,
                hasAddBtn: false,
                iconCbx: "sprite-triangle-down",
                styleLable: {
                    width: '125px',
                },
                lableName: constants.MENU_CATEGORY,
                styleInput: {
                    height: '30px',
                },
                styleCbx: {
                    flex: '1', 
                    height: '30px',
                    position: 'relative',
                    borderRadius: '2px'
                },
                styleMenu: {
                    top: '100%',
                    width: '170px',
                    position: 'absolute',
                },
                posItem: 0,
                cbxItems: fixedCategory,
            },

            ///Prop truyền vào combobox Don vi
            unitCbx: {
                cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                canAutocomplete: "on",
                isReadonly: false,
                hasLable: true,
                hasAddBtn: false,
                isRequired: true,
                iconCbx: "sprite-triangle-down",
                
                styleLable: {
                    width: '125px',
                },
                styleError: {
                    paddingLeft: '125px',
                },
                lableName: constants.UNIT,
                styleInput: {
                    height: '30px',
                },
                styleCbx: {
                    flex: '1',
                    height: '30px',
                    position: 'relative',
                    borderRadius: '2px'
                },
                styleMenu: {
                    top: '100%',
                    width: '170px',
                    position: 'absolute',
                },
                posItem: 0,
                cbxItems: fixedUnit,
            },

            ///Prop truyền vào combobox Don vi
            cookPlaceCbx: {
                cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                canAutocomplete: "on",
                isReadonly: false,
                hasLable: true,
                hasAddBtn: false,
                isRequired: true,
                iconCbx: "sprite-triangle-down",
                styleLable: {
                    width: '125px',
                },
                lableName: constants.COOK_PLACE,
                styleInput: {
                    height: '30px',
                },
                styleCbx: {
                    flex: '1',
                    height: '30px',
                    position: 'relative',
                    borderRadius: '2px'
                },
                styleMenu: {
                    top: '100%',
                    width: '170px',
                    position: 'absolute',
                },
                posItem: 0,
                cbxItems: fixedCookPlace,
            },
        }
    },
    methods: {
        /**
         * Bat su kien nhap lieu thay doi gia tri cua obj dish
         * Nguyen Ba Hai-16/12/2022
         */
        getEmittedValue(component) {
            let dishObj = this.$store.state.dish.dishObj;
            //Dung id de so sanh
            //value de lay du lieu
            switch(component.id) {
                case constants.DISH_NAME:
                    dishObj.dishName = component.value; 
                break;

                case constants.DISH_CODE:
                    dishObj.dishCode = component.value; 
                break;

                case constants.MENU_CATEGORY:
                    dishObj.categoryID = component.idValue; 
                    dishObj.categoryName = component.value; 
                break;

                case constants.UNIT:
                    dishObj.unitID = component.idValue;
                    dishObj.unitName = component.value; 
                break;

                case constants.SALE_PRICE:
                    dishObj.salePrice = component.value; 
                break;

                case constants.PRIME_COST:
                    dishObj.primeCost = component.value; 
                break;

                case constants.DESCRIPTION:
                    dishObj.description = component.value; 
                break;

                case constants.COOK_PLACE:
                    dishObj.cookPlaceID = component.idValue;
                    dishObj.cookPlaceName = component.value; 
                break;

                case constants.SHOW_ON_MENU:
                    dishObj.showOnMenu = component.text; 
                break;
            }
            //Mutate gia tri cua dish obj trong store
            this.$store.commit(dishModule.SET_DISH_OBJECT_VAL, dishObj);
        },

        /**
         * Ham blur ra khoi o input ten va sinh ma code khi o code chua duoc dien
         */
        async blurInputGenCode(component) {
            if(component[0].value && !this.$store.state.dish.dishObj.dishCode) {
                await this.$store.dispatch(dishAction.GEN_CODE_BY_NAME);
            }
            //Tat loi o dishCode
            this.$refs["dish_code"].querySelector("input").__vueParentComponent.data.notValid = false;
        },

        /**
         * Ham blur ra khoi o input CBX khong co dieu kien
         */
        getCbxDynBlurNoRule(component) {
            let dish = this.$store.state.dish.dishObj;
            switch(component.id) {
                case constants.MENU_CATEGORY: 
                    dish.categoryName = component.value
                    dish.categoryID = fnGetCategoryID(component.value)
                break;
                case constants.COOK_PLACE: 
                    dish.cookPlaceName = component.value
                    dish.cookPlaceID = fnGetCookPlaceID(component.value)
                break;
            }
            //Mutate gia tri cua dish obj trong store
            this.$store.commit(dishModule.SET_DISH_OBJECT_VAL, dish);
        },

        /**
         * Ham blur ra khoi o input de validate
         */
        blurValidate(component) {
            let dish = this.$store.state.dish.dishObj;
            switch(component[0].id) {
                case constants.UNIT: 
                    dish.unitID = fnGetUnitID(component[0].value)
                break;
            }
            //Mutate gia tri cua dish obj trong store
            this.$store.commit(dishModule.SET_DISH_OBJECT_VAL, dish);
        }
    },
    computed:{
        getDishObject(){
            return this.$store.state.dish.dishObj;
        },

        getLoadingStatus() {
            return this.$store.state.dish.onLoadingForm;
        },

        getValidateState() {
            console.log(this.$store.state.dish.startValidate)
            return this.$store.state.dish.startValidate;
        },

    },
}
</script>
<style lang="css" scoped>
.form-general_content{
    display: flex;
    flex-direction: row;
}
.form-left {
    flex: 1;
    padding-right: 8px;
    padding-top: 8px;
}

.form-left > div {
    margin-bottom: 10px;
}

.form-right {
    position: relative;
    width: 210px;
    height: 180px;
    padding: 8px;
    margin-top: 8px;
    display: flex;
    border: 1px solid #cccccc;
}

.form-right_lable {
    position: absolute;
    background-color: #fff;
    bottom: calc(100% - 5px);
    padding: 0 3px;
}

.form-right_symbolbtn {
    position: absolute;
    height: 25px;
    width: 110px;
}

.form-right_img {
    flex: 1;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    
}

.form-right_img_text {
    white-space: nowrap;
    text-align: center;
    font-size: 13px;
}

.form-right_img_content {
    width: 160px;
    height: 120px;
    background: url(~/src/assets/images/ImageHandler.png) no-repeat;
}

.form-right_btn {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    width: 27px;
    height: 54px;
    margin-left: 3px;
}
</style>