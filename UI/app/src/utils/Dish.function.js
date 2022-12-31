import {fixedCategory, fixedUnit, fixedCookPlace, fixedDishType} from '../resource'
import { formatToSpecialTypeMoney } from './common.function'

/**
 * Ham format gia tri tra ve tu API de dien vao bang
 * Nguyen Ba Hai -21/12/2022
 * @param {*} array 
 * @returns 
 */
export function fnFormatArrayValue(array) {
    array.forEach(ele => {
        for (const key in ele) {
            if(ele[key] == null || ele[key] == "") {
                //Format truong hop rong
                if(key == "freeModifyPrice" || key == "priceChangeInTime" || key == "showOnMenu") {
                    ele[key] = "0";
                } else if(key == "primeCost") {
                    ele[key] = "0,00";
                } else {
                    ele[key] = "";
                }
            } else {
                //Format truong hop co gia tri
                if(key == "primeCost" || key == "salePrice") {
                    //Format tien
                    ele[key] = formatToSpecialTypeMoney(ele[key]);
                }
            }
        }
    });
    return array;
}



//Hàm lấy id loại món ăn theo tên
export function fnGetDishTypeID(name) {
    let id = null
    fixedDishType.forEach(element => {
        if(element.name == name){
            id = element.id;
        }
    });
    return id;
}

//Hàm lấy id nhóm thực đơn 
export function fnGetCategoryID(name) {
    let id = null;
    fixedCategory.forEach(element => {
        if(element.name == name){
            id = element.id;
        }
    });
    return id;
}

//Hàm lấy id đơn vị
export function fnGetUnitID(name) {
    let id = null;
    fixedUnit.forEach(element => {
        if(element.name == name){
            id = element.id;
        }
    });
    return id;
}

//Hàm lấy id ơi chế biên
export function fnGetCookPlaceID(name) {
    let id = null;
    fixedCookPlace.forEach(element => {
        if(element.name == name){
            id = element.id;
        }
    });
    return id;
}

