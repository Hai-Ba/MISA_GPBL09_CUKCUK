import {constants} from './constants/Dish.constants'
export const typeComboBox = {
    DYMAMIC_DROPDOWN_VAL: 1,
    FIXED_DROPDOWN_VAL: 2,
}

export const mathOperator = {
    LIKE: "∗",
    NOT_LIKE: "!",
    EQUAL: "=",
    BEGIN_WITH: "+",
    END_WITH: "-",
    SMALLER: "<",
    HIGHER: ">",
    SMALLER_EQUAL: "≤",
    HIGHER_EQUAL: "≥",
}

export const filterType = {
    NORMAL_FILTER: 0,
    LIKE: 1,
    NOT_LIKE: 2,
    EQUAL: 3,
    BEGIN_WITH: 4,
    END_WITH: 5,
    SMALLER: 6,
    HIGHER: 7,
    SMALLER_EQUAL: 8,
    HIGHER_EQUAL: 9,
    FILTER_TRUE_FALSE: 10,
}

export const ruleValidate = {
    REQUIRED: 1,
    FORMATTING: 2,
    UNIQUE: 3,
}

export const dialogType = {
    DELETE_ONE: 1,
    DELETE_MANY: 2,
    ADD_ONE: 3,
    UPDATE_ONE: 4,
    DEFAULT: 10,
}

export const fixedDishType = [
    {
        id: "13a507d0-342d-6292-6de3-2ceaa9cf3765",
        name: "Món ăn",
    },
    {
        id: "1f60e54f-196f-45f3-5498-ae38c5379e4b",
        name: " Đồ uống",
    },
]

export const fixedCategory = [
    {
        id: "142cb08f-7c31-21fa-8e90-67245e8b283e",
        name: "Đồ luộc",
    },
    {
        id: "17120d02-6ab5-3e43-18cb-66948daf6128",
        name: "Đồ chiên",
    },
    {
        id: "469b3ece-744a-45d5-957d-e8c757976496",
        name: "Đồ hấp",
    },
    {
        id: "4e272fc4-7875-78d6-7d32-6a1673ffca7c",
        name: "Đồ hầm",
    },
]

export const fixedUnit = [
    {
        id: "27f91d6c-14b1-6c74-92ef-c9d5c2d91e91",
        name: "Suất",
    },
    {
        id: "30007451-29ff-4fe4-3707-70859f4ff30d",
        name: "Cốc",
    },
    {
        id: "34e2b4cc-3758-293d-7aa4-4b24de418577",
        name: "Chai",
    },
    {
        id: "3e39129b-279f-623f-88ea-778aee59fea3",
        name: "Thùng",
    },
    {
        id: "6528b15d-6674-724f-79a4-4b24de418577",
        name: "Bát",
    },
]

export const fixedCookPlace = [
    {
        id: "19165ed7-212e-21c4-0428-030d4265475f",
        name: "Bếp 4",
    },
    {
        id: "2924c34d-68f1-1d0a-c9c7-6c0aeb6ec302",
        name: "Bếp 3",
    },
    {
        id: "3631011e-4559-4ad8-b0ad-cb989f2177da",
        name: "Bếp 2",
    },
    {
        id: "7a0b757e-41eb-4df6-c6f8-494a84b910f4",
        name: "Bếp 1",
    },
]

export const fixedHobby = [
    {
        id: "cda281af-8586-11ed-a2e0-08979872ab9b",
        name: "Tương cà",
        price: "5000"
    },
    {
        id: "cda545d3-8586-11ed-a2e0-08979872ab9b",
        name: "Tương ớt",
        price: "5000"
    },
    {
        id: "cda54c74-8586-11ed-a2e0-08979872ab9b",
        name: "Đậu chiên",
        price: "15000"
    }
]

export function fnGetDataCheckBox(value) {
    return value == 1? "sprite-nor-checked" : "sprite-nor-unchecked";
}

export function fnCheckInputFormat(input, rule, lable) {
    switch(rule) {
        case ruleValidate.FORMATTING: 
            switch(lable) {
                case constants.DISH_CODE: 
                    if(input.length > 25) {
                        return "Tối đa 25 kí tự";
                    }
                    //Validate input ma mon an
                    if(!/^[A-Z0-9]*$/.test(input)) {
                        return "Trường mã chỉ bao gồm kí tự hoa hoặc số";
                    }
                break;
                case constants.DISH_NAME:
                    if(input.length > 128) {
                        return "Tối đa 128 kí tự";
                    }
                break;
                case constants.SALE_PRICE:
                    if(input.length > 16) {
                        return "Tối đa 16 kí tự";
                    }
                break;
                case constants.PRIME_COST:
                    if(input.length > 16) {
                        return "Tối đa 16 kí tự";
                    }
                break;
            }
        break;
    }
}

export function fnGetFilterType(value) {
    switch(value) {
        case mathOperator.LIKE: 
            return filterType.LIKE
        case mathOperator.BEGIN_WITH: 
            return filterType.BEGIN_WITH
        case mathOperator.END_WITH: 
            return filterType.END_WITH
        case mathOperator.NOT_LIKE: 
            return filterType.NOT_LIKE
        case mathOperator.EQUAL: 
            return filterType.EQUAL
        case mathOperator.HIGHER: 
            return filterType.HIGHER
        case mathOperator.HIGHER_EQUAL: 
            return filterType.HIGHER_EQUAL
        case mathOperator.SMALLER: 
            return filterType.SMALLER
        case mathOperator.SMALLER_EQUAL: 
            return filterType.SMALLER_EQUAL
    } 
}

export function fnGetValueTrueFalse(value) {
    switch(value) {
        case "Có":
            return "1";
        case "Không":
            return "0";
        default:
            return "";
    }
}

