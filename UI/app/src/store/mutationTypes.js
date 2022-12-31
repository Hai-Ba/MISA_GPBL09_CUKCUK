// REGION DISH
const dishModule = {
    SET_COMPONENT_CHOOSE: 'setComponentChoose',  //Chon component
    SET_HOBBY_ARRAY: 'setHobbyArray', //Set gia tri cho mang so thich ca nhan , mac dinh la rong

    FORMAT_DISH_OBJECT: 'formatDishObject', //Format cho dish object khi them
    SET_FORM_STATE: 'setFormState', //Set trạng thái form
    SET_FORM_TITLE: 'setFormTitle', //Set tên form
    SET_DISH_OBJECT_VAL: 'setDishObjectVal', //Set gia tri cho Dish object
    SET_DISH_OBJECT_UPDATE: 'setDishObjectUpdate', //Set gia tri cua obj Dish khi chon update
    SET_DISH_OBJECT_DUPLICATE: 'setDishObjectDuplicate',    //Set gia tri dish obj nhan ban
    CHECK_FORM_SUBMIT: 'checkFormSubmit',    //Check neu form co the submit hoac khong
    SET_VAILDATE_STATE: 'setVilidateState',     //Set trang thai validate

    IS_DUPLICATE_CODE: 'isDuplicateCode',   //Check trung ma, 1 la trung, 0 la khong trung

    SET_DIALOG_STATE: 'setDialogState', //Set trạng thái dialog
    SET_DIALOG_TYPE: 'setDialogType', //Set loại dialog
    SET_DIALOG_THREE_BUTTON: 'setDialogThreeButton', //Set dialog voi 3 nut
    SET_DIALOG_TWO_BUTTON: 'setDialogTwoButton', //Set dialog voi 2 nut
    SET_DIALOG_ONE_BUTTON: 'setDialogOneButton', //Set dialog voi 1 nut
    SET_DIALOG_ICON: 'setDialogIcon', //Set icon dialog
    SET_DIALOG_BUTTON_TEXT: 'setButtonText', //Noi dung button cho dialogs
    SET_DIALOG_TITLE: 'setDialogTitle', //Set tieu de dialog
    SET_DIALOG_CONTENT: 'setDialogContent', //Set noi dung dialog
    SET_FILTER_ARRAY: 'setFilterArray', //Set gia tri cho mang filter
    SET_DISH_DELETE: 'setDishDelete',  //Set dish ID Delete

    SET_DISH_LIST: 'setDishList', //Set gia tri cho dish list
    SET_DISH_TOTAL_RECORDS: 'setDishTotalRecords', //Set gia tri cho state totalRecords cua DishModule
    SET_DISH_LIMIT_RECORDS: 'setDishLimitRecords', //Set gia tri cho state limit cua DishModule
    SET_NUMBER_OF_PAGES: 'setNumberOfPage',     //Set gia tri cho state numberOfPages cua DishModule
    SET_PAGE_NUMBER: 'setPageNumber',     //Set gia tri cho state numberOfPages cua DishModule
    SET_PAGE_NUMBER_BY_CLICK: 'setPageNumberByClick', //Set page number to page on click
    SET_PAGING_BTN_STATE: 'setPagingBtnState', //set trang thai cho btn phan trang
    SET_FORM_TAB: 'setFormTab',//Set tab cho form
    SET_CHOSEN_ID_ARRAY: 'setChosenIdArray', //set gia tri cho chosenIdArray
    SET_CHOSEN_ID_ARRAY_ADD_ONE: 'setChosenIdArrayAddOne',      //Set them 1 gia tri cho mang
    SET_CHOSEN_ID_ARRAY_ADD_MANY: 'setChosenIdArrayAddMany',    //Set them nhieu dong cho mang id chon trong bang
    RESET_CHOSEN_ID_ARRAY: 'resetChosenIdArray',    //Resetmang gia tri duoc chon

    SET_TABLE_START_END_RECORD: 'setTableStartEndRecord',     //Set gia tri cho state indexFirstRecord, indexLastRecord cua DishModule
    SET_TABLE_LOADING: 'setTableLoading',       //Set trang thai loading cho bang khi dang fetch du lieu
    SET_FORM_LOADING: 'setFormLoading',
};
// ENDREGION DISH

export {dishModule};