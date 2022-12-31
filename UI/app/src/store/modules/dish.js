// state, mutations, actions cho module Dish
import {dishModule} from '../mutationTypes';
import { dishAction } from '../actionTypes';
import {getDishListService, deleteDishService, genNewCodeByName,insertOrUpdateService, getHobbyByDishID, checkDuplicateCode} from '../../services/dishService';
import { fnFormatArrayValue } from '@/utils/Dish.function';

export const state = {
    componentChoose: null,
    //Trạng thái đóng mở form Input
    formState: false, //Trạng thái đóng mở của form, đóng là false
    onLoadingForm: false,     //Trang thai loading cua form
    wrongFieldArray: [],     //Truong nhap sai dinh dang hoac thieu
    cannotAddOrUpdate: false, //BIen the hien khi con trg nhap sai
    startValidate: false,   //Bat dau validate

    //Trạng thái tiêu đề form Input
    formTitle: String, //1 là from thêm, 2 là form sửa

    //Dish instance
    dishObj: {
        dishTypeID: "13a507d0-342d-6292-6de3-2ceaa9cf3765",
        dishTypeName: "Món ăn",
    },

    //State cho dialog
    dialogState: false, //Trang thai dong mo dialog
    dialogType: Number, //Loai DIALOG
    dialogThreeButton: false, //Mo dialog voi 3 nut
    dialogTwoButton: false, //Mo dialog voi 2 nut
    dialogOneButton: false, //Mo dialog voi 1 nut
    dialogButtonText: String,
    dialogTitle: String, //Tieu de dialog
    dialogIcon: String, //Icon dialog
    dialogContent: String,   //Noi dung cua dialog
    dishCodeDelete: String,   //ID cua mon can xoa
    dishNameDelete: String,   //ID cua mon can xoa

    //Object filter
    filterObj: {
        field: String, //truong filter
        value: null,   //Gia tri filter
        type: Number, //1 like, 2 equal, 3....
    },

    //State the hien co trung ma code hay khong
    isDuplicateCode: false,
    recentDishCode: "", //Ma code hien tai

    //Mang so thich ca nhan
    hobbyArray: [{}],

    //Mảng filter
    filterArray: [],

    //Trạng thái của table
    totalRecords: 0,
    limit: 100, //So ban ghi trong 1 bang
    pageNumber: 1,  //So trang hien tai
    numberOfPages: 1, //Tong so trang hien tai
    dishList: [], //List gia tri cua bang goi tu service GetPaging
    indexFirstRecord: 0, //So thu tu cua gia tri bat dau
    indexLastRecord: 0,  //So thu tu cua gia tri ket thuc
    chosenIdArray: [],      //Mang gom cac id dc chon
    onLoadingTable: false,   //State loading cua bang
    
    //Trang thai cua paging btn
    btnChangePage: { //true la khong the di dc nua, false la co the di tiep dc
        disableGoFirst: true,
        disableGoPrev: true,
        disableGoNext: false,
        disableGoLast: false,
    },
    //TRang thai disable input pagenumber
    isDisableInputPageNumber: false,
    //Disable nut sua, xoa nhan ban khi khong co ban ghi
    isDisableBtnTableTool: false,

    //Trang thai tab
    formTab: {
        generalInfo: true,
        serveHobby: false,
    },
};

export const mutations = {
    [dishModule.SET_HOBBY_ARRAY](state, value) {
        state.hobbyArray = [{}];
        state.hobbyArray = value;
    },

    //Set component choose
    [dishModule.SET_COMPONENT_CHOOSE](state, value) {
        state.componentChoose = value;
    },

    //Set trạng thái đóng mở của FORM
    [dishModule.SET_FORM_STATE](state, value) {
        state.formState = value;
        if(!value) { //Dong form
            //Reset state hobby array
            state.hobbyArray = [{}];
            //Reset Form tab
            state.formTab = {
                generalInfo: true,
                serveHobby: false,
            }
            //Reset dish Object
            state.dishObj = {
                dishTypeID: "13a507d0-342d-6292-6de3-2ceaa9cf3765",
                dishTypeName: "Món ăn",
            }
            state.recentDishCode = "";
        }
    },

    //Set gia tri cho bien isDuplicateCode
    [dishModule.IS_DUPLICATE_CODE](state, value) {
        if(value == 0) {
            state.isDuplicateCode = false;
        } else {
            state.isDuplicateCode = true;
        }
    },

    //Set trạng thái validate
    [dishModule.SET_VAILDATE_STATE](state, value) {
        state.startValidate = value;
    },

    //Check form submit
    [dishModule.CHECK_FORM_SUBMIT](state) {
        //reset 
        state.cannotAddOrUpdate = false;
        if(!state.dishObj.dishName || state.dishObj.dishName.trim().length > 128) {
            state.cannotAddOrUpdate = true;
        }
        if(!state.dishObj.dishCode || !/^[A-Z0-9]*$/.test(state.dishObj.dishCode) || state.dishObj.dishCode.trim().length > 25) {
            state.cannotAddOrUpdate = true;
        }
        if(state.isDuplicateCode) { //Ma bi trung
            state.cannotAddOrUpdate = true;
        }
        if(!state.dishObj.unitID) {
            state.cannotAddOrUpdate = true;
        }
        if(!state.dishObj.salePrice) {
            state.cannotAddOrUpdate = true;
        }else {
            if(state.dishObj.salePrice.length > 16) {
                state.cannotAddOrUpdate = true;
            }
        }
        if(state.dishObj.primeCost){
            if(state.dishObj.primeCost.length > 16) {
                state.cannotAddOrUpdate = true;
            }
        }
        if(state.dishObj.categoryName && !state.dishObj.categoryID || state.dishObj.cookPlaceName && !state.dishObj.cookPlaceID) {
            state.cannotAddOrUpdate = true;
        }
    },

    //Format cho dish object khi them
    [dishModule.FORMAT_DISH_OBJECT](state) {
        for (const key in state.dishObj) {
            if (state.dishObj[key] == null || state.dishObj[key] == "") {
                delete state.dishObj[key];
            }
        }
    },

    //Set tiêu đề của FORM
    [dishModule.SET_FORM_TITLE](state, value) {
        state.formTitle = value;
    },

    //Set gia tri obj dish khi them moi
    [dishModule.SET_DISH_OBJECT_VAL](state, value) {
        state.dishObj = value;
    },

    //Set gia tri obj dish khi sua
    [dishModule.SET_DISH_OBJECT_UPDATE](state) {
        state.dishObj = state.dishList.find(ele => ele.dishID == state.chosenIdArray[0]);
        for (const key in state.dishObj) {
            state.dishObj[key] = state.dishObj[key].toString();
        }
        //Gia tri code cu
        state.recentDishCode = state.dishObj.dishCode;
    },

    //Set gia tri obj dish khi Nhan ban
    [dishModule.SET_DISH_OBJECT_DUPLICATE](state) {
        state.dishObj = state.dishList.find(ele => ele.dishID == state.chosenIdArray[0]);
        for (const key in state.dishObj) {
            state.dishObj[key] = state.dishObj[key].toString();
        }
        delete state.dishObj.dishID;
    },

    //Set tab dang hien thi
    [dishModule.SET_FORM_TAB](state, tabValue) {
        for (const key in state.formTab) {
            if (key == tabValue) {
                state.formTab[key] = true;
            }else{
                state.formTab[key] = false;
            }
        }
    },

    //Set dialog butotn text
    [dishModule.SET_DIALOG_BUTTON_TEXT](state, value) {
        state.dialogButtonText = value;
    },

    //Set trang thai dong/mo cua Dialog
    [dishModule.SET_DIALOG_STATE](state, value) {
        state.dialogState = value;
    },

    //Set type cua Dialog
    [dishModule.SET_DIALOG_TYPE](state, value) {
        state.dialogType = value;
    },

    //Set tieu de cua Dialog
    [dishModule.SET_DIALOG_TITLE](state, value) {
        state.dialogTitle = value;
    },

    //Set noi dung cua Dialog
    [dishModule.SET_DIALOG_CONTENT](state, value) {
        state.dialogContent = value;
    },

    //Set ion cua Dialog
    [dishModule.SET_DIALOG_ICON](state, value) {
        state.dialogIcon = value;
    },

    //Set trang thai dialog co 3 nut
    [dishModule.SET_DIALOG_THREE_BUTTON](state, value) {
        state.dialogThreeButton = value;
    },

    //Set trang thai dialog co 2 nut
    [dishModule.SET_DIALOG_TWO_BUTTON](state, value) {
        state.dialogTwoButton = value;
    },

    //Set trang thai dialog co 1 nut
    [dishModule.SET_DIALOG_ONE_BUTTON](state, value) {
        state.dialogOneButton = value;
    },

    //Set Code va Name cua record khi xoa 1 ban ghi
    [dishModule.SET_DISH_DELETE](state) {
        state.dishList.forEach(ele => {
            if(ele.dishID == state.chosenIdArray[0]) {
                state.dishCodeDelete = ele.dishCode;
                state.dishNameDelete = ele.dishName;
            }
        });
    },

    //Set gia tri cho mang filter
    [dishModule.SET_FILTER_ARRAY](state, value) {
        if(value.value) {//Neu truong filter co gia tri
            let isFound = false;
            state.filterArray.forEach(ele => {
                if(value.field == ele.field) {
                    isFound = true;
                    ele.value = value.value;
                    ele.type = value.type;
                }
            });
            if(!isFound) {
                state.filterArray.push(value);
            }
        } else {
            let isFound = false;
            state.filterArray.forEach(ele => {
                if(value.field == ele.field) {
                    isFound = true;
                    ele.value = value.value;
                    ele.type = value.type;
                }
            });
            if(isFound) { //neu khong co gia tri truyen vao filter thi loai bo khoi mang
                state.filterArray.filter(ele => ele.field != value.field )
            }
        }
    },

    //Set gia tri cho state DishList
    [dishModule.SET_DISH_LIST](state, value) {
        //Format value truoc khi day vao array
        let arrayValue = fnFormatArrayValue(value);
        state.dishList = arrayValue;
    },

    //Set total record cho bang
    [dishModule.SET_DISH_TOTAL_RECORDS](state, value) {
        state.totalRecords = value;
    },

    //Set total record cho bang
    [dishModule.SET_DISH_LIMIT_RECORDS](state, value) {
        state.limit = value;
    },

    //Set gia tri tong so trang
    [dishModule.SET_NUMBER_OF_PAGES](state) {
        state.numberOfPages = Math.ceil(state.totalRecords/state.limit);
        
    },

    //Set gia tri cua trang hien tai bang input
    [dishModule.SET_PAGE_NUMBER](state, value) {
        state.pageNumber = value;
        if(value == 0) {
            state.isDisableInputPageNumber = true;
            state.isDisableBtnTableTool = true;
        } else {
            state.isDisableInputPageNumber = false;
            state.isDisableBtnTableTool = false;
        }
    },

    //Set gia tri cua trang hien tai bang click
    [dishModule.SET_PAGE_NUMBER_BY_CLICK](state, type) {
        switch(type) {
            case 1:
                //Go to first page
                state.pageNumber = 1;
            break;
            case 2:
                //Go to last page
                state.pageNumber = state.numberOfPages;
            break;
            case 3:
                //Go prev
                if(state.pageNumber > 1) {//Chck nay trang dang la 1 thi kgong the ve dc
                    state.pageNumber -= 1;
                }
            break;
            case 4:
                //Go next
                if(state.pageNumber < state.numberOfPages) {//Chck nay trang dang la trang cuoi thi kgong the tien dc
                    state.pageNumber += 1;
                }
            break;
        }
    },

    //Set gia tri cua trang hien tai bang input
    [dishModule.SET_PAGING_BTN_STATE](state) {
        //set gia tri cho btn phan trang
        if(state.numberOfPages <= 1) {
            state.btnChangePage = { //true la khong the di dc nua, false la co the di tiep dc
                disableGoFirst: true,
                disableGoPrev: true,
                disableGoNext: true,
                disableGoLast: true,
            }
        } else {
            if(state.pageNumber == 1) {//trang dau
                state.btnChangePage = {
                    disableGoFirst: true,
                    disableGoPrev: true,
                    disableGoNext: false,
                    disableGoLast: false,
                }
            } else if(state.pageNumber == state.numberOfPages) {//Trang cuoi
                state.btnChangePage = {
                    disableGoFirst: false,
                    disableGoPrev: false,
                    disableGoNext: true,
                    disableGoLast: true,
                }
            } else {
                state.btnChangePage = {
                    disableGoFirst: false,
                    disableGoPrev: false,
                    disableGoNext: false,
                    disableGoLast: false,
                }
            }
        }
    },

    //Set gia tri cho state indexFirstRecord, indexLastRecord cua DishModule
    [dishModule.SET_TABLE_START_END_RECORD](state) {
        if(state.totalRecords == 0) {
            state.indexFirstRecord = 0;
            state.indexLastRecord = 0;
        }
        else {
            //Set gia tri bat dau
            state.indexFirstRecord = state.limit * (state.pageNumber - 1) + 1;
            //Set gia tri ket thuc
            state.indexLastRecord = state.limit * state.pageNumber > state.totalRecords ? state.totalRecords : state.limit * state.pageNumber;
        }
    },

    //Set trang thai loading cho bang khi goi du lieu
    [dishModule.SET_TABLE_LOADING](state, value) {
        state.onLoadingTable = value;
    },

    //Set trang thai form
    [dishModule.SET_FORM_LOADING](state, value) {
        state.onLoadingForm = value;
    },

    //Reset va set gia tri cho mang cac id duoc chon
    [dishModule.SET_CHOSEN_ID_ARRAY](state, value) { //click chuot trais them va xoa 1
        //empty array
        state.chosenIdArray = [];
        //Day gia tri vao
        state.chosenIdArray.push(value);
    },

    //Reset mang cac id duoc chon
    [dishModule.RESET_CHOSEN_ID_ARRAY](state) { //click chuot trais them va xoa 1
        //empty array
        state.chosenIdArray = [];
    },

    //Set them gia tri cho mang ID dc chon trong bang
    [dishModule.SET_CHOSEN_ID_ARRAY_ADD_ONE](state, value) {
        if(state.chosenIdArray.includes(value)){
            //Xoa gia tri khoi mang
            state.chosenIdArray = state.chosenIdArray.filter(id => id != value);
        } else {
            state.chosenIdArray.push(value);//Them gia tri vao mang
        }
    },

    [dishModule.SET_CHOSEN_ID_ARRAY_ADD_MANY](state, value) {
        if(state.chosenIdArray != []) {
            //empty array
            let firstVal = state.chosenIdArray[0];
            let firstIdx, secondIdx;
            state.chosenIdArray = [];
            state.dishList.forEach(ele => { //Lay ra 2 gai tri index
                if(ele.dishID == firstVal) {
                    firstIdx = state.dishList.indexOf(ele);
                }
                if(ele.dishID == value) {
                    secondIdx = state.dishList.indexOf(ele)
                }
            });
            if(firstIdx < secondIdx) {
                for(let i = firstIdx; i <= secondIdx ; i++) {
                    state.chosenIdArray.push(state.dishList[i].dishID);
                }
            } else if(firstIdx > secondIdx) {
                for(let i = firstIdx; i >= secondIdx ; i--) {
                    state.chosenIdArray.push(state.dishList[i].dishID);
                }
            } else {
                state.chosenIdArray.push(state.dishList[firstIdx].dishID);
            }
        } 
        else {
            state.chosenIdArray.push(value);
        }
    }
};

export const actions = {
    //Goi service lay du lieu do vao bang va phan trang
    [dishAction.GET_PAGE_DATA]({commit}) {
        //Chay loading
        commit(dishModule.SET_TABLE_LOADING, true);
        getDishListService(state.limit, state.pageNumber, state.filterArray);
    },

    //Goi service de xoa ban ghi
    [dishAction.DELETE_RECORD]({commit}) {
        //Chay loading
        commit(dishModule.SET_TABLE_LOADING, true);
        if(state.chosenIdArray.length == state.dishList.length && state.pageNumber == state.numberOfPages) {
            state.pageNumber -= 1;
        }
        deleteDishService(state.chosenIdArray);
    },

    //Goi service sinh ma theo ten
    [dishAction.GEN_CODE_BY_NAME]({commit}) {
        commit(dishModule.SET_FORM_LOADING, true);
        genNewCodeByName(state.dishObj.dishName);
    },

    //Goi service them moi hoac sua
    [dishAction.INSERT_OR_UPDATE_RECORD]() {
        if(state.dishObj.salePrice && state.dishObj.salePrice != "") {
            state.dishObj.salePrice = state.dishObj.salePrice.replace(/,/g, '');
        }
        if(state.dishObj.primeCost && state.dishObj.primeCost != "") {
            state.dishObj.primeCost = state.dishObj.primeCost.replace(/,/g, '');
        }
        insertOrUpdateService({
            dish: state.dishObj,
            hobbyList: state.hobbyArray,
        });
    },

    //Goi service lay so thich phuc vu
    [dishAction.GET_DISH_HOBBY]() {
        getHobbyByDishID(state.dishObj.dishID);
    },

    //Goi service kiem tra trung ma
    async [dishAction.CHECK_DUPLICATE_CODE]() {
        await checkDuplicateCode(state.dishObj.dishCode);
    }
};

export default {
    state,
    mutations,
    actions,
};