<template>
<div>
    <div class="cukcuk-cbx">
        <label v-if="hasLable" for="" :style="styleLable">
            {{lableName}}
            <span v-if="isRequired" style="color: var(--color-error); margin-left: 4px;">
                (*)
            </span>
        </label>
        <!-- CBX type 1 -->
        <div class="cbx-wrapper" ref="cbx" v-if="type == typeComboBox.DYMAMIC_DROPDOWN_VAL" :style="styleCbx" @mouseleave="onMouseLeave">
            <input type="text" class="input-dynamic"
                :tabindex="tabIndex" 
                :autocomplete="canAutocomplete" 
                :autofocus="canAutoFocus" 
                :readonly="isReadonly" 
                :style="styleInput"
                :id="lableName"
                :class="[notValid? classInvalid : classValid]"
                :title="titleMessage" 
                :value="value != ''?value:initVal"
                @keyup="onAutoSuggest"
                @blur="onValidateAndEmit"
                @keyup.enter="onEnterKey"
                ref="inputValue">
            <div :style="styleBtn"
                :class="[notValid? classInvalid : classValid]"
                class="btn-dynamic">
                <!-- Nút dropdown trong combobox -->
                <div @click="onClickDropdownBtn" style="width: 22px">
                    <div :class="iconCbx"></div>
                </div>
                <div v-if="hasAddBtn" @click="onClickAddBtn" style="width: 22px">
                    <div class="sprite-plus-icon"></div>
                </div>
            </div>
            <ul v-if="showCbxValue" :style="styleMenus">
                <!-- Combobox Value -->
                <li v-if="hasSuggestion" style="background-color: #FFF !important;">{{suggestionName}}</li>
                <li v-for="item in items" 
                :key="item" 
                :id="item.id"
                :class="[this.$refs.inputValue.value == item.name ? liActiveClass : liInactiveClass]" @click="onClickDynamicCbxValue">
                    {{item.name}}
                </li>
            </ul>
        </div>

        <!-- CBX type 2 -->
        <div class="cbx-wrapper" ref="cbx" v-if="type == typeComboBox.FIXED_DROPDOWN_VAL" :style="styleCbx" @mouseleave="onMouseLeave">
            <div :style="styleBtn" class="cbx-btn">
                <!-- Nút dropdown trong combobox -->
                <div @click="onClickDropdownBtn">
                    <!-- <div :class="iconCbx"></div> -->
                    {{operator}}
                </div>
            </div>
            <input type="text" class="input-fixed" 
                v-if="inputOnlyNumber"
                :tabindex="tabIndex"
                :autocomplete="canAutocomplete" 
                :autofocus="canAutoFocus" 
                :readonly="isReadonly" 
                :style="styleInput" 
                @keypress="numbersOnly"
                @keyup.enter="onEnterKey"
                @blur="onBlurFixEmitValue"
                ref="inputValue">
            <input type="text" class="input-fixed" 
                v-else
                :autocomplete="canAutocomplete" 
                :autofocus="canAutoFocus" 
                :readonly="isReadonly" 
                :style="styleInput" 
                @keyup.enter="onEnterKey"
                @blur="onBlurFixEmitValue"
                ref="inputValue">
            <ul v-if="showCbxValue" :style="styleMenus">
                <!-- Combobox Value -->
                <li v-if="hasSuggestion" style="background-color: #FFF !important;">{{suggestionName}}</li>
                <li v-for="item in items" :id="item.value" :key="item" @click="onClickFixedCbxValue">
                    <div :class="[operator == item.value ? spanLiActive : spanLiInactive]"></div>
                    {{item.name}}
                </li>
            </ul>
        </div>

    </div>
    <div v-if="notValid" style="color: var(--color-error); font-size: 12px; padding-top: 3px;" :style="styleError">
        <!-- Khi người dùng nhập sai sẽ hiển thị -->
        {{messageValid}}
    </div>
</div>
</template>

<script>
import {
    typeComboBox,
    ruleValidate,
    fnCheckInputFormat,
    filterType,
    fnGetFilterType,
    fnGetValueTrueFalse
} from '../resource';
export default {
    name: 'ComboBox',

    data() {
        return {
            showCbxValue: false,
            value: "",
            operator: this.operatorInit ? this.operatorInit : "",
            items: this.cbxItems ? this.cbxItems : [], //List hay thay ddooir
            maintainArrayItems: this.cbxItems ? this.cbxItems : [], //List không thay đổi
            liActiveClass: "chossen-li",
            liInactiveClass: "normal-li",
            spanLiActive: "active-li",
            spanLiInactive: "inactive-li",
            typeComboBox: typeComboBox,
            position: this.posItem,
            type: this.cbxType,
            styleMenus: this.styleMenu,
            rules: this.ruleSets, //Dieu kien validate
            notValid: this.notValidField,
            messageValid: this.messageInform,
            classInvalid: "invalid-input",
            classValid: "valid-input",
            titleMessage: null,     //Hien thi title canh bao khi hover
            filter: this.useFilter ? true : false,
            field: this.fieldName,   
            isTrueFalse: this.isTrueFalseField,
            inputOnlyNumber: this.acceptOnlyNumber? this.acceptOnlyNumber : false,
        }
    },

    props: {
        tabIndex: String,
        canAutocomplete: String, //on: cho phép tự động gợi ý, off: không cho phép
        canAutoFocus: Boolean, //Mặc đinh focus true/false
        isReadonly: Boolean, //True: cho phép nhập liệu(Thêm [cursor: 'pointer'] cho prop), False: không cho nhập liệu
        initVal: String, //Giá trị ban đầu của ô input
        iconCbx: String, //Tên class icon dropdown
        styleInput: Object, //Obj truyền Style cho Input
        styleCbx: Object, //Obj truyền Style cho Combobox
        styleMenu: Object, //Obj truyền Style cho Menu dropdown
        styleLable: Object, //Obj truyền Style cho Lable
        styleBtn: Object, //Obj truyền Style cho Button
        styleError: Object, //Obj truyền Style cho ErrorMess
        hasLable: Boolean, //Có lable thì true, không la false
        lableName: String, //Tên của lable
        isRequired: Boolean, //True nếu ô nhập liệu bặt buộc điên giá trị
        hasAddBtn: Boolean, //True để thêm btn thêm vào input
        hasSuggestion: Boolean, //True Để hiện gợi ý trong thẻ li
        suggestionName: String, //Tên gợi ý trong thẻ li
        cbxType: Number, //Loại ComboBox
        cbxItems: Array, //Mảng chứa các giá trị của combobox
        posItem: Number, //0: Là dropdown, 1 là dropup
        operatorInit: String, //Toán tử ban đầu
        ruleSets: Array, //Mang chua dieu kien validate
        messageInform: String, //Message loi
        useFilter: Boolean, //Co dung filter hay khong mac dinh la khong
        fieldName: String, //Ten truong
        isTrueFalseField: Boolean, //Cos phair true false khong
        acceptOnlyNumber: Boolean,  //True; only input number
        notValidField: Boolean,  
    },

    mounted() {
        if(this.canAutoFocus) {
            this.$refs.inputValue.focus();
        }
    },

    methods: {
        /**
         * Ham accpept only number input
         * Nguyen Ba Hai - 21/12/2022
         */
        numbersOnly(event) {
            event = (event) ? event : window.event;
            var charCode = (event.which) ? event.which : event.keyCode;
            if ((charCode > 31 && (charCode < 48 || charCode > 57)) && charCode !== 46) {
                event.preventDefault();
            } else {
                return true;
            }
        },
        /**
         * Bắt sự kiện blur de validate input
         * Nguyễn Bá Hải - 10/12/2022
         */
        onValidateAndEmit(event) {
            let input = event.target.value;
            let message;
            let flag = false;
            let errorField = false;
            //Clear title
            this.titleMessage = null;
            //Check neu du lieu dien co trong list
            if(input){
                this.items.forEach(item => {
                    if(input == item.name) {
                        flag = true;
                    }
                });
            } else{
                flag = true;
            }
            if(!flag) {
                this.titleMessage = "Giá trị bạn chọn không có trong danh sách";
            }

            //Neu co dieu kien thi moi kich hoat validate
            if (this.rules) {
                if (!input && this.rules.includes(ruleValidate.REQUIRED)) {
                    message = "Trường này không được để trống";
                } else {
                    
                    //Check voi cac rules
                    for (let i = 0; i < this.rules.length; i++) {
                        if (this.rules[i] == ruleValidate.FORMATTING) {
                            //fnCheckInputFormat
                            message = fnCheckInputFormat(input, ruleValidate.FORMATTING, this.lable);
                            break;
                        }
                    }
                }
                this.messageValid = message;
            }
            if (message || this.titleMessage) {
                //Chen message o cuois
                this.notValid = true;
                this.textVal = input;
                errorField = true;
            }

            if(!message && !this.titleMessage) {
                this.notValid = false;
                this.textVal = input;
            }

            //Emit value
            if(!this.filter) {
                //CBS khong filter
                if(!this.rules) {
                    this.$emit('blurEmitValue', event.target);
                } else {
                    this.$emit('blurEmitValue', [event.target, {
                        field: this.lable,
                        error: errorField,
                    }]);
                }
            } else {
                if(this.isTrueFalse) {
                    //Filter cho combobox dong co true false
                    this.$emit('blurEmitValue', {
                        field: this.field,
                        value: fnGetValueTrueFalse(input),
                        type: filterType.FILTER_TRUE_FALSE,
                    })
                }
                else {
                    //Filter cho combobox dong khong true false
                    this.$emit('blurEmitValue', {
                        field: this.field,
                        value: input == "Tất cả"? "":input,
                        type: filterType.NORMAL_FILTER,
                    })
                }
            }
            
        },

        onEnterKey(event) {
            //emit filter object cho cbx type 2 bang enter
            if(this.filter) {
                this.$emit('onEnterEmitValue', {
                    field: this.field,
                    value: event.target.value,
                    type: fnGetFilterType(this.operator),
                })
            }
        },

        onBlurFixEmitValue(event) {
            //emit filter object cho cbx type 2 bang blur
            if(this.filter) {
                this.$emit('onBlurFixEmitValue', {
                    field: this.field,
                    value: event.target.value,
                    type: fnGetFilterType(this.operator),
                })
            }
        },

        /**
         * Bắt sự kiện click vào nút dropdown trong combobox
         * Nguyễn Bá Hải - 10/12/2022
         */
        onClickDropdownBtn() {
            if (this.$refs.cbx.style.position != "relative") {
                let topVal = this.$refs.cbx.getBoundingClientRect().top;
                let leftVal = this.$refs.cbx.getBoundingClientRect().left;
                if (this.position == 0) {
                    // Dropdown
                    topVal = topVal + this.$refs.cbx.offsetHeight;
                    this.styleMenus.top = `${topVal}px`;
                    this.styleMenus.left = `${leftVal}px`;
                } else if (this.position == 1) {
                    // Dropup
                    this.styleMenus.bottom = `calc(100vh - ${topVal}px)`;
                }
            }
            if (this.type == typeComboBox.DYMAMIC_DROPDOWN_VAL) {
                this.styleMenus.width = `${this.$refs.cbx.offsetWidth}px`
            }
            this.items = this.maintainArrayItems;
            this.showCbxValue = !this.showCbxValue;
        },

        /**
         * Bắt sự kiện khi chọn item ở CBX Dynamic
         * Nguyễn Bá Hải - 10/12/2022
         */
        onClickDynamicCbxValue(event) {
            this.value = event.target.innerHTML; //Thay giá trị ô dropdown
            this.showCbxValue = !this.showCbxValue; //Đóng dropdown
            this.notValid = false;
            //Emit value
            if(!this.filter) {
                //CBS khong filter
                this.$emit('clickItemEmitValue', {
                    id: this.$refs.inputValue.id,
                    value: this.value,
                    idValue: event.target.id,
                });
            } else {
                if(this.isTrueFalse) {
                    //Filter cho combobox dong co true false
                    this.$emit('clickItemEmitValue', {
                        field: this.field,
                        value: fnGetValueTrueFalse(this.value),
                        type: filterType.FILTER_TRUE_FALSE,
                    })
                }
                else {
                    //Filter cho combobox dong khong true false
                    this.$emit('clickItemEmitValue', {
                        field: this.field,
                        value: this.value == 'Tất cả'? "" : this.value,
                        type: filterType.NORMAL_FILTER,
                    })
                }
            }
        },

        /**
         * Bắt sự kiện khi chọn item ở CBX Dynamic
         * Nguyễn Bá Hải - 10/12/2022
         */
        onClickFixedCbxValue(event) {
            this.operator = event.target.id;
            this.showCbxValue = !this.showCbxValue; //Đóng dropdown
            if(this.filter) {
                this.$emit('clickItemEmitValue', {
                    field: this.field,
                    value: this.$refs.inputValue.value,
                    type: fnGetFilterType(this.operator),
                })
            }
        },

        /**
         * Hàm bắt sự kiện Key-up cho auto-suggest cho input
         * Nguyễn Bá Hải = 11/12/2022
         */
        onAutoSuggest(event) {
            this.value = event.target.value;
            //Clear Array items[]
            this.items = [];
            //Tìm trong Array items[], và thay đổi array các giá trị trùng với string input
            this.maintainArrayItems.forEach(ele => {
                if (ele.name.includes(this.$refs.inputValue.value)) {
                    this.items.push(ele);
                }
            });
            //Hiển thị array khi có 1 hoặc nhiều kết quả trùng
            if (this.items.length >= 1 && this.$refs.inputValue.value != "") {
                this.showCbxValue = true;
            } else {
                this.showCbxValue = false;
            }
            //Khi this.$refs.inputValue.value == "" thì array trở về ban đầu
            if (this.$refs.inputValue.value == "" || this.$refs.inputValue.value == undefined) {
                this.items = this.maintainArrayItems;
            }
            //Dung validate
            if (this.notValid) {
                this.notValid = false;
            }
            this.$emit("keyUpEmitValue", event.target.value);
        },
    },
    created() {
        // close the options if click out the component
        window.addEventListener("click", (e) => {
          if (!this.$el.contains(e.target)) {
            this.showCbxValue = false;
          }
        });
    },
}
</script>

<style lang="css" scoped>
.normal-li {
    background-color: white;
}

.chossen-li {
    background-color: var(--color-cbx-item-selected);
}

.cukcuk-cbx {
    display: flex;
    align-items: center;
}

.cbx-wrapper {
    cursor: pointer;
    /* position: relative; */
    display: flex;
    height: 100%;
    align-items: center;

}

.cbx-wrapper>input {
    flex: 1;
    padding: 5px;
    width: 100%;
    border-top-left-radius: 2px;
    border-bottom-left-radius: 2px;
}

.cbx-wrapper>div {
    display: flex;
    height: 100%;
    background-color: #FFFFFF;
    border-top-right-radius: 2px;
    border-bottom-right-radius: 2px;
}

.cbx-wrapper>ul {
    box-shadow: rgba(0, 0, 0, 0.23) 0px 3px 6px;
    width: 100%;
    background-color: #FFF;
    padding-top: 5px;
    border-radius: 2px;
    z-index: 5;
}

.cbx-wrapper>ul li {
    display: flex;
    align-items: center;
    list-style: none;
    justify-content: flex-start;
    line-height: 23px;
    padding: 0 5px;
    z-index: 6;
}

.cbx-wrapper>ul li:hover {
    background-color: var(--color-cbx-item-hover);
}

.cbx-wrapper>div>div {
    display: flex;

    justify-content: center;
    align-items: center;
    width: 100%;
    height: 100%;
    cursor: pointer;
}

/* .cbx-btn {
    padding: none;
} */

.cbx-btn:hover {
    background-color: #E4F2EA !important;
    border: 1px solid #0072BC !important;
}

.active-li {
    width: 16px;
    height: 16px;
    border-radius: 50%;
    background-image: -webkit-linear-gradient(top, #53e253, #148322);
    border: #05A055;
    margin-right: 8px;
}

.inactive-li {
    width: 16px;
    height: 16px;
    border-radius: 50%;
    margin-right: 8px;
}

.input-dynamic {
    border-top: 1px solid #cccccc;
    border-left: 1px solid #cccccc;
    border-bottom: 1px solid #cccccc;
}

.input-dynamic:focus,
.input-dynamic:focus+.btn-dynamic {
    border-color: #0072BC;
}

.btn-dynamic {
    border-top: 1px solid #cccccc;
    border-right: 1px solid #cccccc;
    border-bottom: 1px solid #cccccc;
}

.input-fixed {
    border: 1px solid #cccccc;
}

.input-fixed:focus {
    border-color: #0072BC;
}

label {
    display: flex;
}

.valid-input {
    border-color: #cccccc;
}

.invalid-input {
    border-color: var(--color-error) !important;
}
</style>
