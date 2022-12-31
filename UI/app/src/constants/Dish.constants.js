//File lưu các giá trị fix cứng
export const constants = {
    // REGION: Từ khóa
    WORD_ADD: "Thêm",
    WORD_DUPLICATE: "Nhân bản",
    WORD_UPDATE: "Sửa",
    WORD_DELETE: "Xóa",
    WORD_RELOAD: "Nạp",
    WORD_EXPORT: "Xuất khẩu",
    WORD_SHOW: "Hiển thị",
    WORD_HYPHEN: "-",
    WORD_BEYOND: "trên",
    WORD_VALUE: "kết quả",
    WORD_PAGE: "Trang",
    // END REGION: Từ khóa

    // REGION: Giá trị class picURL
    /*Table tab-menu bar */
    ICON_ADD: "sprite-add-record",
    ICON_DUPLIECATE: "sprite-duplicate-record",
    ICON_UPDATE: "sprite-update-record",
    ICON_DELETE: "sprite-delete-record",
    ICON_RELOAD: "sprite-reload",
    ICON_EXPORT: "sprite-export-table",
    /*Table tab-menu bar */

    /*Table paging */
    ICON_PAGE_FIRST_ENABLE: "sprite-page-first-enable",
    ICON_PAGE_LAST_ENABLE: "sprite-page-last-enable",
    ICON_PAGE_PREV_ENABLE: "sprite-page-prev-enable",
    ICON_PAGE_NEXT_ENABLE: "sprite-page-next-enable",
    ICON_REFRESH: "sprite-refresh",
    /*Table paging */

    // END REGION: Giá trị class picURL

    // REGION: Tiêu đề bảng Content
    // END REGION: Tiêu đề bảng Content

    // REGION: Từ khóa Sidebar
    // END REGION: Từ khóa Sidebar

    // REGION: Từ khóa Header
    // END REGION: Từ khóa Header

    // REGION: Gợi ý phím tắt
    SHORTCUT_ADD: "Ctrl+1",
    SHORTCUT_DUPLICATE: "Ctrl+2",
    SHORTCUT_DELETE: "Ctrl+D",
    SHORTCUT_UPDATE: "Ctrl+E",
    HINT_PAGE_LAST: "Về trang cuối",
    HINT_PAGE_FIRST: "Về trang đầu",
    HINT_PAGE_NEXT: "Trang tiếp",
    HINT_PAGE_PREV: "Trang trước",
    HINT_REFRESH: "Tải lại",
    // END REGION: Gợi ý phím tắt

    // REGION: Từ khóa FormInput
    FORM_ADD: "Thêm món",
    FORM_UPDATE: "Sửa món",
    // END REGION: Từ khóa FormInput


    // REGION: Properties cua Dish entity
    DISH_TYPE: "Loại món",
    DISH_NAME: "Tên món",
    DISH_CODE: "Mã món",
    MENU_CATEGORY: "Nhóm thực đơn",
    UNIT: "Đơn vị tính",
    SALE_PRICE: "Giá bán",
    PRIME_COST: "Giá vốn",
    DESCRIPTION: "Mô tả",
    COOK_PLACE: "Chế biến tại",
    SHOW_ON_MENU: "Không hiển thị trên thực đơn",
    DISH_IMAGE: "Ảnh đại diện",
    // REGION: Properties cua Dish entity
};



export function fnValidDishCode(value) {
    return /^[A-Z0-9]*$/.test(value);
}