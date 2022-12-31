<template>
<div>
    <div class="cukcuk-input">
        <label v-if="hasLable" for="" :style="styleLable">
            {{lableName}}
            <span v-if="isRequired" style="color: var(--color-error); margin-left: 4px;">
                (*)
            </span>
        </label>
        <!-- Input chi nhap gia tri so -->
        <input v-if="inputOnlyNumber" 
            :tabindex="tabIndex"
            :type="inputType" ref="inputFocus"
            :autocomplete="canAutocomplete"
            :style="styleInput"
            :value="initVal"
            :id="lableName"
            :class="[notValid ? classInvalid : classValid, isDisable ? disableInput : '']"
            @keydown="numbersOnly"
            @blur="onValidate"
            @keyup="stopValidateAndEmit"
            @input="formatInputField"
            @keydown.enter="onClickEnter">

        <!-- Input nhan moi gia tri -->
        <input v-else
            :tabindex="tabIndex"
            :type="inputType" ref="inputFocus"
            :autocomplete="canAutocomplete"
            :style="styleInput"
            :value="initVal"
            :id="lableName"
            :class="[notValid ? classInvalid : classValid]"
            @blur="onValidate"
            @keyup="stopValidateAndEmit"
            @keydown.enter="onClickEnter"
            >
    </div>
    <div v-if="notValid " 
        style="color: var(--color-error); font-size: 12px; padding-top: 3px;"
        :style="styleError">
        <!-- Khi người dùng nhập sai sẽ hiển thị -->
        {{messageValid}}
    </div>
</div>
</template>

<script>
import {fnCheckInputFormat, ruleValidate} from '../resource';
import {formatToSpecialTypeMoney } from '../utils/common.function'
export default {
    name: 'BaseInput',
    data() {
        return {
            disableInput: 'disable-input',
            inputOnlyNumber: this.acceptOnlyNumber? this.acceptOnlyNumber : false,
            rules: this.ruleSets,
            fnCheckInputFormat: fnCheckInputFormat,
            formatToSpecialTypeMoney: formatToSpecialTypeMoney,
            lable: this.lableName,
            notValid: this.notValidField,
            messageValid: this.messageInform,
            classInvalid: "invalid-input",
            classValid: "valid-input",
            isMoneyValue: this.isMoneyData,
        }
    },
    props: {
        isDisable: Boolean,
        tabIndex: String,
        canAutocomplete: String,    //on: cho phép tự động gợi ý, off: không cho phép
        canAutoFocus: Boolean,      //Mặc đinh focus true/false
        notValidField: Boolean,     //True: hiển thị dòng valid, False ngược lại
        messageInform: String,      //Nội dung validate
        inputType: String,          //Loại input: text, checkbox,....
        hasLable: Boolean,          //Có dung lable hay không
        isRequired: Boolean,        //Có Bắt buộc hay không
        lableName: String,          //Tên lable
        styleLable: Object,         //Style cho lable
        styleInput: Object,         //Obj truyền Style cho Input
        styleError: Object,         //Obj truyền Style cho Input
        initVal: String,            //Giá trị ban đầu
        ruleSets: Array,            //Mang chua dieu kien validate
        acceptOnlyNumber: Boolean,  //True; only input number
        isMoneyData: Boolean,       //Truong nay co la tien hay khong
    },
    methods: {
        /**
         * Ham accpept only number input
         * Nguyen Ba Hai - 21/12/2022
         */
        numbersOnly(event) {
            var keyCode = event.keyCode;
            if ((keyCode >= 48 && keyCode <= 57) || (keyCode >= 96 && keyCode <= 105) || keyCode == 8) {
                return true
            } else {
                event.preventDefault();
            }
            if(this.isMoneyValue) {
                let inputValueLength = event.target.value.length;
                //Chuyen con tro ve vi tri sau dau phay khi nhan ',' hoac '.'
                if(keyCode === 188 || keyCode === 190) {
                    event.target.selectionStart = inputValueLength - 2;//con tro chuot dich ve vi tri sau dau , 
                } else if(keyCode === 8) {
                    //
                }
                if(event.target.selectionStart >= inputValueLength - 2) {//VI tri con tro o sau dau phay
                    if(keyCode === 8) {//Phim back space
                        event.target.setSelectionRange(event.target.selectionStart - 1,event.target.selectionStart - 1);//Dich len tren 1 vi tri 
                        if(event.target.selectionStart == inputValueLength - 2) {
                            event.target.value = "";
                        }
                    }
                }
            }
        },

        /**
         * Ham format tien khi input
         * Nguyen Ba Hai - 21/12/2022
         */
        formatInputField() {
            
            //format
        },
        /**
         * Bat su kien nhan phim enter
         * Nguyen Ba Hai - 21/12/2022
         */
        onClickEnter(event) {
            this.$emit('enterEmitValue', event.target.value);
        },
        /**
         * Bắt sự kiện blur de validate input
         * Nguyễn Bá Hải - 10/12/2022
         */
        onValidate(event) {
            let input = event.target.value;
            let message;
            let errorField = false;
            //Neu co dieu kien thi moi kich hoat validate
            if(this.rules) {
                if(!input && this.rules.includes(ruleValidate.REQUIRED)) {
                    message = "Trường này không được để trống";
                } else {
                    for(let i = 0; i < this.rules.length; i++) {
                        if(this.rules[i] == ruleValidate.FORMATTING) {
                            //fnCheckInputFormat
                            message = fnCheckInputFormat(input, ruleValidate.FORMATTING, this.lable);
                            break;
                        }
                    }
                }
            }
            if(message) {
                //Chen message o cuois
                this.notValid = true;
                this.textVal = input;
                this.messageValid = message;
                errorField = true;
            }
            if(!message) {
                this.notValid = false;
            }
            //Su kien blur
            if(this.rules){
                this.$emit('blurEmitValue', [event.target, {
                    field: this.lable,
                    error: errorField,
                }]);
            } else{
                this.$emit('blurEmitValue', event.target.value);
            }
        },

        /**
         * Bắt sự kiện turn off validate input, va emit data
         * Nguyễn Bá Hải - 10/12/2022
         */
        stopValidateAndEmit(event) {
            this.notValid = false;
            //Emit input value
            this.$emit('keyUpEmitValue', event.target);
        },

    },
    mounted() {
        if(this.canAutoFocus) {
            this.$refs.inputFocus.focus();
            // console.log(this.$refs.inputFocus);
        }
    },
}
</script>

<style lang="css" scoped>
.cukcuk-input {
    display: flex;
}

.cukcuk-input > input:focus {
    border: 1px solid #0072BC;
}

.cukcuk-input > label {
    display: flex;
    align-items: center;
    /* word-wrap: unset; */
}

.cukcuk-input > input {
    height: 30px;
    padding: 0 5px;
    border: 1px solid #cccccc;
    border-radius: 2px;
}

.valid-input {
    border-color: #cccccc;
}

.invalid-input {
    border-color: var(--color-error) !important;
}

.disable-input{
    cursor: default;
    pointer-events: none;
    opacity: 30%;
}
</style>
