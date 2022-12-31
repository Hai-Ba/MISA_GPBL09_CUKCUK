<template>
<div class="header">
    <div>
        <div class="sprite-toggle header-toggle">
        </div>
        <div class="header-logo">
            <!-- <img class="header-logo" src="../../assets/images/logo-text.png" alt="CukCuk_pic"> -->
        </div>
    </div>
    <div>
        <div>
            <combo-box 
                :cbxType="dataLeftCombobox.cbxType"
                :canAutocomplete="dataLeftCombobox.canAutocomplete" 
                :isReadonly="dataLeftCombobox.isReadonly" 
                :iconCbx="dataLeftCombobox.iconCbx" 
                :initVal="dataLeftCombobox.initVal"
                :styleInput="dataLeftCombobox.styleInput"
                :styleCbx="dataLeftCombobox.styleCbx"
                :styleMenu="dataLeftCombobox.styleMenu"
                :cbxItems="dataLeftCombobox.cbxItems"
                :hasSuggestion="dataLeftCombobox.hasSuggestion"
                :suggestionName="dataLeftCombobox.suggestionName"
                :posItem="dataLeftCombobox.posItem"
                :styleBtn="dataLeftCombobox.styleBtn">
            </combo-box>
        </div>
        <div>
            <combo-box 
                :cbxType="dataRightCombobox.cbxType"
                :canAutocomplete="dataRightCombobox.canAutocomplete" 
                :isReadonly="dataRightCombobox.isReadonly" 
                :iconCbx="dataRightCombobox.iconCbx" 
                :initVal="dataRightCombobox.initVal"
                :styleInput="dataRightCombobox.styleInput"
                :styleCbx="dataRightCombobox.styleCbx"
                :styleMenu="dataRightCombobox.styleMenu"
                :cbxItems="dataRightCombobox.cbxItems"
                :posItem="dataRightCombobox.posItem"
                :styleBtn="dataRightCombobox.styleBtn">
            </combo-box>
            <div style="margin-left: 24px;">
                <i class="icofont-bullhorn header-icofont"></i>
            </div>
            <div style="margin-left: 24px;">
                <i class="icofont-earth header-icofont"></i>
            </div>
            <div style="color: white;">
                <div class="user-pic" style="margin-left: 56px;"></div>
                <div style="margin-left: 8px;">Hai</div>
                <div class="sprite-trigger" style="margin-left: 56px;"></div>
            </div>
        </div>
    </div>
</div>
</template>

<script>
import {typeComboBox} from '../../resource';
import ComboBox from '../../components/ComboBox.vue';
export default {
    name: 'HeaderBar',
    data() {
        return {
            //Prop truyền vào combobox bên trái
            dataLeftCombobox: {
                cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                canAutocomplete: "off",
                isReadonly: true,
                iconCbx: "sprite-trigger",
                initVal: "--Chọn nhà hàng--",
                styleInput: {
                    height: '29px',
                    // cursor: 'pointer',
                    border: 'none',
                    backgroundColor: '#005082!important',
                    color: '#fff',  
                },
                styleBtn: {
                    border: 'none',
                    backgroundColor: '#005082!important',
                },
                hasSuggestion: true,
                suggestionName: "--Chọn nhà hàng--",
                styleCbx: {
                    width: '200px',
                    height: '29px',
                    position: 'relative',
                                      
                },
                styleMenu: {
                    top: '100%',
                    width: '200px',
                    position: 'absolute',
                },
                posItem: 0,
                cbxItems: [ //Sau nay goij API se gen Array tu API
                    {
                        id: 1,
                        name: "Biển Đông",
                    },
                    {
                        id: 2,
                        name: "Biển Tây",
                    },
                    {
                        id: 3,
                        name: "Biển Bắc",
                    },
                    {
                        id: 4,
                        name: "Biển Nam",
                    },
                    {
                        id: 5,
                        name: "Biển Địa Trung Hải",
                    },
                ],
            },

            //Prop truyền vào combobox bên phải
            dataRightCombobox: {
                cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                canAutocomplete: "on",
                isReadonly: false,
                iconCbx: "sprite-trigger",
                initVal: "Tiếng Việt (Vietnamese)",
                styleInput: {
                    height: '29px',
                    border: 'none',
                    backgroundColor: '#005082!important',
                    color: '#fff',  
                },
                styleBtn: {
                    border: 'none',
                    backgroundColor: '#005082!important',
                },
                styleCbx: {
                    width: '170px',
                    height: '29px',
                    position: 'relative',
                },
                styleMenu: {
                    top: '100%',
                    width: '170px',
                    position: 'absolute',
                },
                posItem: 0,
                cbxItems: [ //Sau nay goij API se gen Array tu API
                    {
                        id: 1,
                        name: "Tiếng Việt (Vietnamese)",
                    },
                    {
                        id: 2,
                        name: "Tiếng Anh (English)",
                    },
                ],
            },
        }
    },
    components: {
        ComboBox,
    },
}
</script>

<style lang="css" scoped>
.header {
    display: flex;
    justify-content: space-between;
    background-color: var(--color-normal);
    height: 55px;
    width: 100%;
}


.header-icofont{
    color: white;
    font-size: 24px;
    cursor: pointer;
}

/* Left header*/
.header > div:first-child {
    display: flex;
    align-items: center;
    width: 230px;
    padding: 16px;
    margin-top: 1px;
}

.header-logo {
    background: url(../../assets/images/logo-text.png) no-repeat;
    height: 30px;
    width: 184px;
    background-position: 2px center;
    margin-left: 8px;
    margin-bottom: 1px;
    cursor: pointer;
}



/* Right header */
.header > div:last-child{
    display: flex;
    justify-content: space-between;
    align-items: center;
    width: calc(100% - 230px);
    padding: 0 12px;
}

.header > div:last-child > div:last-child{
    display: flex;
    width: 1000px;
    justify-content: end;
    align-items: center;
    padding-right: 10px;
}

.header > div:last-child > div:last-child > div:last-child{
    display: flex;
    align-items: center;
}

.header .user-pic{
    display: flex;
    align-items: flex-end;
    justify-content: center;
    height: 28px;
    width: 28px;
    background-color: #ccc;
    border-radius: 50%;
}
</style>
