<template>
    <div class="content">
        <div>
            <!-- content-wrapper -->
            <div class="title">
                <!-- title -->
                <div class="title-name">
                    <!-- Tiêu đề -->
                    Thực đơn
                </div>
                <div class="title-btn">
                    <!-- Nút -->
                    <base-button 
                        :useText="titleBtn.useText"
                        :text="titleBtn.text"
                        :useClassPic="titleBtn.useClassPic"
                        :picUrl="titleBtn.picUrl"
                        :styleIcon="titleBtn.styleIcon">
                    </base-button>
                </div>
            </div>
            <div class="table">
                <!-- table & paging -->
                <div class="table-tool">
                    <!-- Thanh công cụ -->
                    <base-button
                        :useText="true"
                        :text="constants.WORD_ADD"
                        :useClassPic="true"
                        :picUrl="constants.ICON_ADD"
                        :titleBtn="constants.SHORTCUT_ADD"
                        :styleIcon="titleBtn.styleIcon"
                        :styleBtn="styleToolBtn"
                        @clickBtn="openFormAdd">
                    </base-button>
                    <base-button
                        :useText="true"
                        :text="constants.WORD_DUPLICATE"
                        :useClassPic="true"
                        :picUrl="constants.ICON_DUPLIECATE"
                        :isDisable="getDisableBtnTableTool"
                        :titleBtn="constants.SHORTCUT_DUPLICATE"
                        :styleIcon="titleBtn.styleIcon"
                        :styleBtn="styleToolBtn"
                        @clickBtn="openFormDuplicate">
                    </base-button>
                    <base-button
                        :useText="true"
                        :text="constants.WORD_UPDATE"
                        :useClassPic="true"
                        :picUrl="constants.ICON_UPDATE"
                        :isDisable="getDisableBtnTableTool"
                        :titleBtn="constants.SHORTCUT_UPDATE"
                        :styleIcon="titleBtn.styleIcon"
                        :styleBtn="styleToolBtn"
                        @clickBtn="openFormUpdate">
                    </base-button>
                    <base-button
                        :useText="true"
                        :text="constants.WORD_DELETE"
                        :useClassPic="true"
                        :picUrl="constants.ICON_DELETE"
                        :isDisable="getDisableBtnTableTool"
                        :titleBtn="constants.SHORTCUT_DELETE"
                        :styleIcon="titleBtn.styleIcon"
                        :styleBtn="styleToolBtn"
                        @clickBtn="deleteChosenRows">
                    </base-button>
                </div>
                <div class="table-content"  style="position: relative">
                    <!-- Bảng -->
                    <base-table
                        :theadArray="theadArray"
                        :tbodyArray="getDishList"
                        :styleTHead="styleTHead"
                        :tableName="`dish`"
                        :chosenIdArray="getChosenIdArray"
                        :loadingStatus="getLoadingStatus"
                        @emitFilterObj="getFilter"
                        @clickChooseRow="getChoosenRow"
                        @clickChooseOneMoreRow="getOneMoreRowChossen"
                        @clickChooseManyRows="getManyRowsChossen"
                        @dbClickRow="openFormUpdate">
                    </base-table>
                </div>
                <div class="table-paging">
                    <!-- Phân trang -->
                    <div class="table-paging_btn">
                        <!-- HIển thị các nút phân trang -->
                        <div>
                            <!-- Nút tiến, lùi, về đầu, về cuối, tải lại -->
                            <div>
                                <!-- Btn về đầu -->
                                <base-button
                                    class="mg-right8"
                                    :styleBtn="pagingBtn.styleBtn"
                                    :useClassPic="true"
                                    :titleBtn="constants.HINT_PAGE_FIRST"
                                    :picUrl="constants.ICON_PAGE_FIRST_ENABLE"
                                    :isDisable="getPagingBtnState.disableGoFirst"
                                    @clickBtn="clickGoToFirstPage">
                                </base-button>
                            </div>
                            <div>
                                <!-- Btn lùi 1 -->
                                <base-button
                                    :styleBtn="pagingBtn.styleBtn"
                                    :useClassPic="true"
                                    :titleBtn="constants.HINT_PAGE_PREV"
                                    :picUrl="constants.ICON_PAGE_PREV_ENABLE"
                                    :isDisable="getPagingBtnState.disableGoPrev"
                                    @clickBtn="clickGoPrev">
                                </base-button>
                            </div>
                            <span class="border-custom"></span>
                            <div>
                                <!-- Trang -->
                                {{constants.WORD_PAGE}}
                            </div>
                            <base-input
                                class="mg-left12 mg-right8"
                                id="input-page-number"
                                :inputType="pageNumberInput.inputType"
                                :styleInput="pageNumberInput.styleInput"
                                :initVal="getPageNumber"
                                :acceptOnlyNumber="true"
                                :isDisable="getDisableInputPageNumber"
                                @enterEmitValue="onChangePageNumberByInput"
                                @blurEmitValue="onStayPageNumberByInput">
                                <!-- Input nhập trang -->
                            </base-input>
                            <div>
                                <!-- Trên tổng số trang -->
                                {{constants.WORD_BEYOND}}
                                <span>{{getNumberOfPages}}</span>
                            </div>
                            <span class="border-custom"></span>
                            <div>
                                <!-- Btn tiến 1 -->
                                <base-button
                                    class="mg-right8"
                                    :styleBtn="pagingBtn.styleBtn"
                                    :useClassPic="true"
                                    :titleBtn="constants.HINT_PAGE_NEXT"
                                    :picUrl="constants.ICON_PAGE_NEXT_ENABLE"
                                    :isDisable="getPagingBtnState.disableGoNext"
                                    @clickBtn="clickGoNext">
                                </base-button>
                            </div>
                            <div>
                                <!-- Btn về cuối -->
                                <base-button
                                    :styleBtn="pagingBtn.styleBtn"
                                    :useClassPic="true"
                                    :titleBtn="constants.HINT_PAGE_LAST"
                                    :picUrl="constants.ICON_PAGE_LAST_ENABLE"
                                    :isDisable="getPagingBtnState.disableGoLast"
                                    @clickBtn="clickGoToLastPage">
                                </base-button>
                            </div>
                            <span class="border-custom"></span>
                            <div>
                                <!-- Btn tải lại trang -->
                                <base-button
                                    :styleBtn="pagingBtn.styleBtn"
                                    :useClassPic="true"
                                    :titleBtn="constants.HINT_REFRESH"
                                    :picUrl="constants.ICON_REFRESH"
                                    :isDisable="getLoadingStatus"
                                    @clickBtn="clickRefreshTable">
                                </base-button>
                            </div>
                            <span class="border-custom"></span>
                        </div>
                        <div>
                            <!-- Dropdown số bản ghi 1 trang -->
                            <combo-box
                                :cbxType="limitRecordCbx.cbxType"
                                :isReadonly="limitRecordCbx.isReadonly"
                                :initVal="limitRecordCbx.initVal"
                                :iconCbx="limitRecordCbx.iconCbx"
                                :styleInput="limitRecordCbx.styleInput"
                                :styleCbx="limitRecordCbx.styleCbx"
                                :styleMenu="limitRecordCbx.styleMenu"
                                :styleBtn="limitRecordCbx.styleBtn"
                                :cbxItems="limitRecordCbx.cbxItems"
                                :posItem="limitRecordCbx.posItem"
                                @clickItemEmitValue="clickChangeTableSize">
                            </combo-box>
                        </div>
                    </div>
                    <div v-if="getDishList.length > 0" class="table-paging_result">
                        <!-- Hiển thị kết quả tổng số bản ghi -->
                        {{constants.WORD_SHOW}}
                        <span class="word-space"> {{getStartRecord}} </span>
                        {{constants.WORD_HYPHEN}}
                        <span class="word-space"> {{getEndRecord}} </span>
                        {{constants.WORD_BEYOND}}
                        <span class="word-space"> {{getDishTotalRecords}} </span>
                        {{constants.WORD_VALUE}}
                    </div>
                    <div v-else class="table-paging_result">
                        <!-- Hiển thị kết quả tổng số bản ghi -->
                        Không có dữ liệu
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
import BaseButton from '../../components/BaseButton.vue';
import ComboBox from '../../components/ComboBox.vue';
import BaseInput from '../../components/BaseInput.vue';
import BaseTable from '../../components/BaseTable.vue';
import {constants} from '../../constants/Dish.constants';
import {typeComboBox, mathOperator, dialogType} from '../../resource';
import {dishModule} from '../../store/mutationTypes';
import {dishAction} from '../../store/actionTypes';
export default {
    name: 'DishContent',
    components: {
        BaseButton,
        ComboBox,
        BaseInput,
        BaseTable,
    },
    mounted() {
        this._keyListener = function(e) {
            if (e.key === "1" && (e.ctrlKey)) {
                e.preventDefault(); 
                //Open form add
                if(!this.$store.state.dish.formState) {
                    this.openFormAdd();
                }
            }
            if (e.key === "2" && (e.ctrlKey)) {
                e.preventDefault(); 
                //Open form duplicate
                if(!this.$store.state.dish.formState) {
                    this.openFormDuplicate();
                }
            }
            if (e.key === "e" && (e.ctrlKey)) {
                e.preventDefault(); 
                //Open form update
                if(!this.$store.state.dish.formState) {
                    this.openFormUpdate();
                }
            }
            if (e.key === "d" && (e.ctrlKey)) {
                e.preventDefault(); 
                //Delte selected row
                if(!this.$store.state.dish.formState) {
                    this.deleteChosenRows();
                }
            }
        };
        document.addEventListener('keydown', this._keyListener.bind(this));
    },
    unmounted() {
        document.removeEventListener('keydown', this._keyListener);
    },
    methods: {
        /**
         * Hanh dong xoa ban ghi
         * Nguyen Ba Hai - 23/12/2022
         */
        deleteChosenRows() {
            let deleteArr = this.$store.state.dish.chosenIdArray;
            //Thoong bao co muon xoa hay khong
            if(deleteArr.length == 1) {
                //Xoa 1 ban ghi
                this.$store.commit(dishModule.SET_DISH_DELETE);
                //Thong boa chi tiet
                this.$store.commit(dishModule.SET_DIALOG_TYPE, dialogType.DELETE_ONE);
                this.$store.commit(dishModule.SET_DIALOG_STATE,true);
                this.$store.commit(dishModule.SET_DIALOG_ONE_BUTTON,true);
                this.$store.commit(dishModule.SET_DIALOG_TITLE,"CUKCUK - Quản lí nhà hàng");
                this.$store.commit(dishModule.SET_DIALOG_CONTENT,`Bạn có chắc chắn muốn xóa món <<${this.getDishID} - ${this.getDishName}>> không?`);
                this.$store.commit(dishModule.SET_DIALOG_ICON,"sprite-question");
                this.$store.commit(dishModule.SET_DIALOG_TWO_BUTTON,true);
                this.$store.commit(dishModule.SET_DIALOG_THREE_BUTTON,false);

            }
            if(deleteArr.length > 1) {
                //Xoa nhieu ban ghi
                //Thong bao chung
                this.$store.commit(dishModule.SET_DIALOG_TYPE, dialogType.DELETE_MANY);
                this.$store.commit(dishModule.SET_DIALOG_STATE,true);
                this.$store.commit(dishModule.SET_DIALOG_THREE_BUTTON,false);
                this.$store.commit(dishModule.SET_DIALOG_TITLE,"CUKCUK - Quản lí nhà hàng");
                this.$store.commit(dishModule.SET_DIALOG_CONTENT,"Bạn có chắc chắn muốn xóa những món đã chọn không?");
                this.$store.commit(dishModule.SET_DIALOG_ICON,"sprite-question");
                this.$store.commit(dishModule.SET_DIALOG_TWO_BUTTON,true);
                this.$store.commit(dishModule.SET_DIALOG_ONE_BUTTON,true);
            }
        },

        /**
         * Hành động mở form thêm mới
         * Nguyễn Bá Hải - 15-12-2022
         */
        openFormAdd() {
            this.$store.commit(dishModule.SET_FORM_STATE, true);
            this.$store.commit(dishModule.SET_DIALOG_TYPE, dialogType.ADD_ONE);
            this.$store.commit(dishModule.SET_FORM_TITLE, constants.FORM_ADD);
        },

        /**
         * Hành động mở form nhan ban
         * Nguyễn Bá Hải - 15-12-2022
         */
        openFormDuplicate() {
            if(this.$store.state.dish.chosenIdArray.length > 0){
                this.$store.commit(dishModule.SET_DISH_OBJECT_DUPLICATE);
                this.$store.commit(dishModule.SET_FORM_STATE, true);
                this.$store.commit(dishModule.SET_DIALOG_TYPE, dialogType.ADD_ONE);
                this.$store.commit(dishModule.SET_FORM_TITLE, constants.FORM_ADD);
                //Lay ma moi
                this.$store.dispatch(dishAction.GEN_CODE_BY_NAME, this.$store.state.dish.dishObj.dishName);
            }
        },

        /**
         * Hành động mở form sửa
         * Nguyễn Bá Hải - 15-12-2022
         */
        openFormUpdate() {
            //Doi lay gia tri cua dong duco chon de sua
            if(this.$store.state.dish.chosenIdArray.length > 0) {
                //Goi dispatch lay du lieu Hobby
                this.$store.commit(dishModule.SET_DISH_OBJECT_UPDATE);
                this.$store.commit(dishModule.SET_FORM_STATE, true);
                this.$store.commit(dishModule.SET_DIALOG_TYPE, dialogType.UPDATE_ONE);
                this.$store.commit(dishModule.SET_FORM_TITLE, constants.FORM_UPDATE);
                this.$store.dispatch(dishAction.GET_DISH_HOBBY);
                
            }
        },

        /**
         * Tạo mảng chứa các giá trị filter
         * Nguyễn bá Hải - 19/12/2022
         */
        getFilter(value) {
            //Check xem mang filter co giong lan tim kiem truoc khong
            //Neu giong thi thoi khong filter nua, khac thi moi filter
            let notNecessaryToFilter = false;
            if(value.value != '') {
                this.$store.state.dish.filterArray.forEach(obj => {
                    if(obj.field == value.field && obj.value == value.value && obj.type == value.type) {
                        notNecessaryToFilter = true;
                    }
                });
            } else{
                let isFound = this.$store.state.dish.filterArray.find(obj => obj.field == value.field);
                if(!isFound) {
                    notNecessaryToFilter = true;
                }
            }
            if(!notNecessaryToFilter) {
                this.$store.commit(dishModule.SET_FILTER_ARRAY, value);
                //Ve trang dau
                this.$store.commit(dishModule.SET_PAGE_NUMBER, 1);
                //Goi lay ai du lieu
                this.$store.dispatch(dishAction.GET_PAGE_DATA);
            }
        },

        /**
         * Ham thay doi limit size cua table
         * Nguyen Ba Hai - 21/12/2022
         */
        clickChangeTableSize(emitVal) {
            let size = emitVal.value;
            this.$store.commit(dishModule.SET_DISH_LIMIT_RECORDS, size);
            //Ve trang dau
            this.$store.commit(dishModule.SET_PAGE_NUMBER, 1);
            //Goi action lay lai du lieu
            this.$store.dispatch(dishAction.GET_PAGE_DATA);
        },

        /**
         * Ham thay doi so trang bang input
         * Nguyen Ba Hai - 21/12/2022
         */
        onChangePageNumberByInput(value) {
            let pageNumber;
            let input_component = document.querySelector("#input-page-number input");
            if(value == 0) {
                pageNumber = 1;// neu ma ve khong thi se len 1
                this.$store.commit(dishModule.SET_PAGE_NUMBER, pageNumber);
                input_component.value = 1;
            }else {
                pageNumber = value;
                //Doi so trang
                if(pageNumber > this.getNumberOfPages) {
                    pageNumber = this.getNumberOfPages;
                }
                this.$store.commit(dishModule.SET_PAGE_NUMBER, pageNumber);
                input_component.value = pageNumber;
                //Goi lay lai du lieu
                this.$store.dispatch(dishAction.GET_PAGE_DATA);
            }
            
        },

        /**
         * Su kien blur khoi o input nhap so trang, se tro ve gia tri trang hien tai, khong thay goi du lieu
         * Nguyen Ba Hai - 26/12/2022
         */
        onStayPageNumberByInput() {
            let input_component = document.querySelector("#input-page-number input");
            input_component.value = this.getPageNumber;
        },

        /**
         * Ham goi lai api refresh du lieu
         * Nguyen Ba Hai - 21/12/2022
         */
        clickRefreshTable() {
            //Goi lay lai du lieu
            this.$store.dispatch(dishAction.GET_PAGE_DATA);
        },

        /**
         * Ham click sang trang moi(tien 1)
         * Nguyen Ba Hai - 21/12/2022
         */
        clickGoNext() {
            //goi commit thay doi page: page = page + 1
            this.$store.commit(dishModule.SET_PAGE_NUMBER_BY_CLICK, 4); //4 la loai sang trang tiep theo
            //Goi lay lai du lieu
            this.$store.dispatch(dishAction.GET_PAGE_DATA);
        },

        /**
         * Ham click lui ve 1 trang(lui 1)
         * Nguyen Ba Hai - 21/12/2022
         */
        clickGoPrev() {
            //goi commit thay doi page: page = page - 1
            this.$store.commit(dishModule.SET_PAGE_NUMBER_BY_CLICK, 3); //3 la loai ve trang truoc
            //Goi lay lai du lieu
            this.$store.dispatch(dishAction.GET_PAGE_DATA);
        },

        /**
         * Ham click ra trang dau(trang 1)
         * Nguyen Ba Hai - 21/12/2022
         */
        clickGoToFirstPage() {
            //goi commit thay doi page: page = 1
            this.$store.commit(dishModule.SET_PAGE_NUMBER_BY_CLICK, 1); //1 la loai ve trang dau
            //Goi lay lai du lieu
            this.$store.dispatch(dishAction.GET_PAGE_DATA);
        },

        /**
         * Ham click ra trang cuoi
         * Nguyen Ba Hai - 21/12/2022
         */
        clickGoToLastPage() {
            //goi commit thay doi page: page = numberOfPages
            this.$store.commit(dishModule.SET_PAGE_NUMBER_BY_CLICK, 2); //2 la loai ve trang cuoi
            //Goi lay lai du lieu
            this.$store.dispatch(dishAction.GET_PAGE_DATA);
        },

        /**
         * Ham bat su kien chon row trong bang
         * Nguyen Ba Hai-23/12/2022
         */
        getChoosenRow(value) {
            this.$store.commit(dishModule.SET_CHOSEN_ID_ARRAY, value); 
        },

        /**
         * Ham bat su kien chon them 1 row trong bang
         * Nguyen Ba Hai-23/12/2022
         */
        getOneMoreRowChossen(value) {
            this.$store.commit(dishModule.SET_CHOSEN_ID_ARRAY_ADD_ONE, value);
        },

        /**
         * Ham bat su kien chon nhieu dong
         * Nguyen Ba Hai-23/12/2022
         */
        getManyRowsChossen(value) {
            this.$store.commit(dishModule.SET_CHOSEN_ID_ARRAY_ADD_MANY, value);
        },
    },
    computed: {
        //Ham lay gia tri mang de bind vao table
        getDishList() {
            return this.$store.state.dish.dishList;
        },

        //Ham lay gia tri tong so ban ghi khi goi api
        getDishTotalRecords() {
            return this.$store.state.dish.totalRecords;
        },

        //Ham lay gia tri tong so trang khi goi api
        getNumberOfPages() {
            return this.$store.state.dish.numberOfPages;
        },

        //Ham lay so trang hien tai
        getPageNumber() {
            return this.$store.state.dish.pageNumber.toString();
        },

        //Ham lay trang thai btn phan trang
        getPagingBtnState() {
            return this.$store.state.dish.btnChangePage;//la 1 object
        },

        //Ham lay gia tri mang dc chon
        getChosenIdArray() {
            return this.$store.state.dish.chosenIdArray;
        },

        //Ham lay index record bat dau
        getStartRecord() {
            return this.$store.state.dish.indexFirstRecord;
        },

        //Ham lay index record ket thuc
        getEndRecord() {
            return this.$store.state.dish.indexLastRecord;
        },

        getDishID() {
            return this.$store.state.dish.dishCodeDelete;
        },

        getDishName() {
            return this.$store.state.dish.dishNameDelete;
        },

        getLoadingStatus() {
            return this.$store.state.dish.onLoadingTable;
        },  

        getDisableInputPageNumber() {
            return this.$store.state.dish.isDisableInputPageNumber;
        },

        getDisableBtnTableTool() {
            return this.$store.state.dish.isDisableBtnTableTool;
        },
    },
    data() {
        return {
            // tbodyArray: this.getDataTable,
            constants: constants, //Các từ khóa

            titleBtn: { //Props cho title của bảng
                useText: true,
                text: "Phản hồi",
                useClassPic: true,
                picUrl: "sprite-view-email",
                styleIcon: {
                    marginRight: '8px',
                },
            },

            limitRecordCbx: { //props cho dropdown lấy limit bản ghi 1 trang
                cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                isReadonly: true,
                initVal: "100",
                iconCbx: "sprite-triangle-down",
                styleInput: {
                    height: '25px',
                },
                styleCbx: {
                    height: '25px',
                    width: '60px',
                    position: 'relative',
                },
                styleMenu: {
                    width: '60px',
                    bottom: "100%",
                    height: 'fit-content',
                    position: 'absolute',
                },
                posItem: 1,
                cbxItems: [
                    {
                        id: 1,
                        name: "25",
                    },
                    {
                        id: 2,
                        name: "50",
                    },
                    {
                        id: 3,
                        name: "100",
                    },
                ],
            },

            styleToolBtn: {//Props cho dòng công cụ của bảng
                border: 'none',
                backgroundImage: '-webkit-linear-gradient(top,#f9f9f9,#e3e4e6)'
            },

            pagingBtn: {//Props cho các nút phân trang
                styleBtn: {
                    height: '24px',
                    width: '24px',
                    border: 'none',
                    backgroundColor: '#FFF',
                },
            },

            pageNumberInput: {//props cho ô nhập số trang
                inputType: 'text',
                styleInput: {
                    height: '25px',
                    width: '38px',
                    border: '1px solid #CCCCCC',
                    paddingLeft: '5px',
                },
            },

            styleTHead: {//Style cho 
                height: '64px',
            },
            
            theadArray: [ //Props cho table
                {
                    field: "dishTypeName",
                    title: "Loại món",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '160px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '160px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleCbx: {
                        height: '28px',
                        borderRadius: '2px'
                    },
                    styleInput: {
                        height: '28px',
                    },
                    styleMenu: {
                        position: 'fixed',
                        width: '160px'
                    },
                    posItem: 0,
                    initVal: "Tất cả",
                    isReadonly: true,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 0,
                            name: "Tất cả",
                        },
                        {
                            id: 1,
                            name: "Món ăn",
                        },
                        {
                            id: 2,
                            name: " Đồ uống",
                        },
                    ],
                    typeValue: 0,
                },

                {
                    field: "dishCode",
                    title: "Mã món",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '180px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '180px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    cbxType: typeComboBox.FIXED_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleBtn: {
                        height: '28px',
                        width: '28px',
                        border: '1px solid #cccccc',
                        borderRadius: '2px 0 0 2px',
                    },
                    styleCbx: {
                        borderRadius: '2px',
                        // border: '1px solid #cccccc',
                    },
                    styleInput: {
                        height: '28px',
                        borderRadius: '0px 2px 2px 0px',
                    },
                    operatorInit: mathOperator.LIKE,
                    styleMenu: {
                        position: 'fixed',
                        width: '150px',
                    },
                    posItem: 0,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 1,
                            name: "∗ : Chứa",
                            value: `${mathOperator.LIKE}`,
                        },
                        {
                            id: 2,
                            name: "= : Bằng",
                            value: `${mathOperator.EQUAL}`,
                        },
                        {
                            id: 3,
                            name: "+ : Bắt đầu bằng",
                            value: `${mathOperator.BEGIN_WITH}`,
                        },
                        {
                            id: 4,
                            name: "- : Kết thúc bằng",
                            value: `${mathOperator.END_WITH}`,
                        },
                        {
                            id: 5,
                            name: "! : Không chứa",
                            value: `${mathOperator.NOT_LIKE}`,
                        },
                    ],
                    typeValue: 0,
                },

                {
                    field: "dishName",
                    title: "Tên món",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '200px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '200px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    cbxType: typeComboBox.FIXED_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleBtn: {
                        height: '28px',
                        border: '1px solid #cccccc',
                        borderRadius: '2px 0 0 2px',
                        width: '28px',
                    },
                    styleCbx: {
                        borderRadius: '2px',
                        // border: '1px solid #cccccc',
                    },
                    styleInput: {
                        height: '28px',
                        borderRadius: '0px 2px 2px 0px',
                    },
                    operatorInit: mathOperator.LIKE,
                    styleMenu: {
                        position: 'fixed',
                        width: '150px',
                    },
                    posItem: 0,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 1,
                            name: "∗ : Chứa",
                            value: `${mathOperator.LIKE}`,
                        },
                        {
                            id: 2,
                            name: "= : Bằng",
                            value: `${mathOperator.EQUAL}`,
                        },
                        {
                            id: 3,
                            name: "+ : Bắt đầu bằng",
                            value: `${mathOperator.BEGIN_WITH}`,
                        },
                        {
                            id: 4,
                            name: "- : Kết thúc bằng",
                            value: `${mathOperator.END_WITH}`,
                        },
                        {
                            id: 5,
                            name: "! : Không chứa",
                            value: `${mathOperator.NOT_LIKE}`,
                        },
                    ],
                    typeValue: 0,
                },

                {
                    field: "categoryName",
                    title: "Nhóm thực đơn",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '180px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '180px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    cbxType: typeComboBox.FIXED_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleBtn: {
                        height: '28px',
                        border: '1px solid #cccccc',
                        borderRadius: '2px 0 0 2px',
                        width: '28px',
                    },
                    styleCbx: {
                        borderRadius: '2px',
                        // border: '1px solid #cccccc',
                    },
                    styleInput: {
                        height: '28px',
                        borderRadius: '0px 2px 2px 0px',
                    },
                    operatorInit: mathOperator.LIKE,
                    styleMenu: {
                        position: 'fixed',
                        width: '150px',
                    },
                    posItem: 0,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 1,
                            name: "∗ : Chứa",
                            value: `${mathOperator.LIKE}`,
                        },
                        {
                            id: 2,
                            name: "= : Bằng",
                            value: `${mathOperator.EQUAL}`,
                        },
                        {
                            id: 3,
                            name: "+ : Bắt đầu bằng",
                            value: `${mathOperator.BEGIN_WITH}`,
                        },
                        {
                            id: 4,
                            name: "- : Kết thúc bằng",
                            value: `${mathOperator.END_WITH}`,
                        },
                        {
                            id: 5,
                            name: "! : Không chứa",
                            value: `${mathOperator.NOT_LIKE}`,
                        },
                    ],
                    typeValue: 0,
                },

                {
                    field: "unitName",
                    title: "Đơn vị tính",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '150px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '150px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    cbxType: typeComboBox.FIXED_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleBtn: {
                        height: '28px',
                        border: '1px solid #cccccc',
                        borderRadius: '2px 0 0 2px',
                        width: '28px',
                    },
                    styleCbx: {
                        borderRadius: '2px',
                        // border: '1px solid #cccccc',
                    },
                    styleInput: {
                        height: '28px',
                        borderRadius: '0px 2px 2px 0px',
                    },
                    operatorInit: mathOperator.LIKE,
                    styleMenu: {
                        position: 'fixed',
                        width: '150px',
                    },
                    posItem: 0,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 1,
                            name: "∗ : Chứa",
                            value: `${mathOperator.LIKE}`,
                        },
                        {
                            id: 2,
                            name: "= : Bằng",
                            value: `${mathOperator.EQUAL}`,
                        },
                        {
                            id: 3,
                            name: "+ : Bắt đầu bằng",
                            value: `${mathOperator.BEGIN_WITH}`,
                        },
                        {
                            id: 4,
                            name: "- : Kết thúc bằng",
                            value: `${mathOperator.END_WITH}`,
                        },
                        {
                            id: 5,
                            name: "! : Không chứa",
                            value: `${mathOperator.NOT_LIKE}`,
                        },
                    ],
                    typeValue: 0,
                },

                
                {
                    field: "primeCost",
                    title: "Giá vốn",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '150px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '150px',
                        borderRight: '1px solid #CCCCCC',
                        textAlign: 'right'
                    },
                    cbxType: typeComboBox.FIXED_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleBtn: {
                        height: '28px',
                        border: '1px solid #cccccc',
                        borderRadius: '2px 0 0 2px',
                        width: '28px',
                    },
                    styleCbx: {
                        borderRadius: '2px',
                        // border: '1px solid #cccccc',
                    },
                    styleInput: {
                        height: '28px',
                        borderRadius: '0px 2px 2px 0px',
                        textAlign: 'right',
                    },
                    operatorInit: mathOperator.SMALLER_EQUAL,
                    styleMenu: {
                        position: 'fixed',
                        width: '180px',
                    },
                    posItem: 0,
                    acceptOnlyNumber: true,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 1,
                            name: "= : Bằng",
                            value: `${mathOperator.EQUAL}`,
                        },
                        {
                            id: 2,
                            name: "< : Nhỏ hơn",
                            value: `${mathOperator.SMALLER}`,
                        },
                        {
                            id: 3,
                            name: "≤ : Nhỏ hơn hoặc bằng",
                            value: `${mathOperator.SMALLER_EQUAL}`,
                        },
                        {
                            id: 4,
                            name: "> : Lớn hơn",
                            value: `${mathOperator.HIGHER}`,
                        },
                        {
                            id: 5,
                            name: "≥ : Lớn hơn hoặc bằng",
                            value: `${mathOperator.HIGHER_EQUAL}`,
                        },
                    ],
                    typeValue: 0,
                },

                {
                    field: "salePrice",
                    title: "Giá bán",
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '150px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '150px',
                        borderRight: '1px solid #CCCCCC',
                        textAlign: 'right'
                    },
                    cbxType: typeComboBox.FIXED_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleBtn: {
                        height: '28px',
                        border: '1px solid #cccccc',
                        borderRadius: '2px 0 0 2px',
                        width: '28px',
                    },
                    styleCbx: {
                        borderRadius: '2px',
                        // border: '1px solid #cccccc',
                    },
                    styleInput: {
                        height: '28px',
                        borderRadius: '0px 2px 2px 0px',
                        textAlign: 'right',
                    },
                    operatorInit: mathOperator.SMALLER_EQUAL,
                    styleMenu: {
                        position: 'fixed',
                        width: '180px',
                    },
                    posItem: 0,
                    acceptOnlyNumber: true,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 1,
                            name: "= : Bằng",
                            value: `${mathOperator.EQUAL}`,
                        },
                        {
                            id: 2,
                            name: "< : Nhỏ hơn",
                            value: `${mathOperator.SMALLER}`,
                        },
                        {
                            id: 3,
                            name: "≤ : Nhỏ hơn hoặc bằng",
                            value: `${mathOperator.SMALLER_EQUAL}`,
                        },
                        {
                            id: 4,
                            name: "> : Lớn hơn",
                            value: `${mathOperator.HIGHER}`,
                        },
                        {
                            id: 5,
                            name: "≥ : Lớn hơn hoặc bằng",
                            value: `${mathOperator.HIGHER_EQUAL}`,
                        },
                    ],
                    typeValue: 0,
                },

                {
                    field: "priceChangeInTime",
                    title: "Thay đổi theo thời giá",
                    isTrueFalseField: true,
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '180px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '180px',
                        borderRight: '1px solid #CCCCCC',
                        justifyContent: 'center'
                    },
                    cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleCbx: {
                        height: '28px',
                        borderRadius: '2px'
                    },
                    styleInput: {
                        height: '28px',
                    },
                    styleMenu: {
                        position: 'fixed',
                        
                    },
                    posItem: 0,
                    initVal: "Tất cả",
                    isReadonly: true,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 0,
                            name: "Tất cả",
                        },
                        {
                            id: 1,
                            name: "Có",
                        },
                        {
                            id: 2,
                            name: "Không",
                        },
                    ],
                    typeValue: 1,
                },

                {
                    field: "freeModifyPrice",
                    title: "Điều chỉnh giá tự do",
                    isTrueFalseField: true,
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '174px',
                        borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '174px',
                        borderRight: '1px solid #CCCCCC',
                        justifyContent: 'center'
                    },
                    cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleCbx: {
                        height: '28px',
                        borderRadius: '2px'
                    },
                    styleInput: {
                        height: '28px',
                    },
                    styleMenu: {
                        position: 'fixed',
                        
                    },
                    posItem: 0,
                    initVal: "Tất cả",
                    isReadonly: true,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 0,
                            name: "Tất cả",
                        },
                        {
                            id: 1,
                            name: "Có",
                        },
                        {
                            id: 2,
                            name: "Không",
                        },
                    ],
                    typeValue: 1,
                },

                {
                    field: "showOnMenu",
                    title: "Hiển thị trên thực đơn",
                    isTrueFalseField: true,
                    styleCol: {
                        padding: '0 6px 3px 6px',
                        width: '150px',
                        // borderRight: '1px solid #CCCCCC',
                    },
                    styleCell: {
                        width: '151px',
                        borderRight: '1px solid #CCCCCC',
                        justifyContent: 'center'
                    },
                    cbxType: typeComboBox.DYMAMIC_DROPDOWN_VAL,
                    iconCbx: "sprite-triangle-down",
                    styleCbx: {
                        height: '28px',
                        borderRadius: '2px'
                    },
                    styleInput: {
                        height: '28px',
                    },
                    styleMenu: {
                        position: 'fixed',
                        
                    },
                    posItem: 0,
                    initVal: "Tất cả",
                    isReadonly: true,
                    cbxItems: [ //Sau nay goij API se gen Array tu API
                        {
                            id: 0,
                            name: "Tất cả",
                        },
                        {
                            id: 1,
                            name: "Có",
                        },
                        {
                            id: 2,
                            name: "Không",
                        },
                    ],
                    typeValue: 1,
                },

                
            ],
        }
    },
}
</script>
<style lang="css" scoped>
    .content {
        flex: 1;
        width: calc(100% - 230px);
        height: 100%;
        padding: 12px 8px 8px 8px;
    }

    .content > div {
        height: 100%;
        width: 100%;
    }

    .title {
        display: flex;
        justify-content: space-between;
        height: 36px;
        margin-bottom: 10px;
    }

    .title-name {
        height: 100%;
        font-size: var(--size-font-title);
        display: flex;
        align-items: center;
    }

    .title-btn {
        height: 100%;
        display: flex;
        align-items: flex-start;
    }

    /* BẢNG */
    .table {
        display: flex;
        flex-direction: column;
        justify-content: space-between;
        height: calc(100% - 46px);
        flex: 1;
        /* border: 1px solid #CCCCCC; */
        border-radius: 2px;
    }

    .table-tool {
        display: flex;
        align-items: center;
        height: 27px;
        width: 100%;
        background-image: -webkit-linear-gradient(top,#f9f9f9,#e3e4e6);
        border: 1px solid #CCCCCC;
        padding: 0 1px;
        
    }

    .table-content {
        /* width: 100%; */
        width: calc(100vw - 246px);
        flex: 1;
    }

    .table-paging {
        display: flex;
        justify-content: space-between;
        height: 38px;
        width: 100%;
        border-left: 1px solid #CCCCCC;
        border-right: 1px solid #CCCCCC;
        border-bottom: 1px solid #CCCCCC;
    }

    .table-paging_result {
        display: flex;
        height: 100%;
        justify-content: flex-end;
        align-items: center;
        padding-right: 12px;
    }

    .table-paging_btn {
        display: flex;
        justify-content: space-between;
        align-items: center;
        height: 100%;
        min-width: 430px;
    }

    .table-paging_btn > div:first-child {
        display: flex;
        align-items: center;
    }
</style>