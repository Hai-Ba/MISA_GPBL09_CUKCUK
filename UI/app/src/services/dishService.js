import axios from "axios";
import { dishModule } from "@/store/mutationTypes";
import { dishAction } from "@/store/actionTypes";
import { store } from "@/store";
import { formatToSpecialTypeMoney } from '../utils/Dish.function'
const BASE_URL = "https://localhost:7179/api/v1/Dish";

/**
 * Ham lay gia tri phan trnag va tong so ban ghi
 * Nguyen Ba Hai - 2/12/2022
 * @param {*} limit So ban ghi 1 trang
 * @param {*} pageNumber So trang
 * @param {*} filterList Danh sach cac truong filter
 */
export async function getDishListService(limit, pageNumber, filterList) {
    await axios
        .post(`${BASE_URL}/filter?limit=${limit}&pageNumber=${pageNumber}`, filterList)
        .then(async(res) => {
            //Set mang du lieu
            store.commit(dishModule.SET_DISH_LIST, res.data.list);
            //Set tong so ban ghi
            store.commit(dishModule.SET_DISH_TOTAL_RECORDS, res.data.total);
            //Set tong so trang
            store.commit(dishModule.SET_NUMBER_OF_PAGES);
            //Thay doi hien thi button phan trang
            store.commit(dishModule.SET_PAGING_BTN_STATE);
            //Chon hang dau tien luon
            if(res.data.list.length > 0) {
                store.commit(dishModule.SET_CHOSEN_ID_ARRAY, res.data.list[0]["dishID"]);
            } else {
                store.commit(dishModule.RESET_CHOSEN_ID_ARRAY);
                store.commit(dishModule.SET_PAGE_NUMBER, 0);
            }
            //Set ban ghi bat dau va ket thuc
            store.commit(dishModule.SET_TABLE_START_END_RECORD);
            //Dung chay loading
            store.commit(dishModule.SET_TABLE_LOADING, false);
        })
        .catch((error) => {
            console.log(error);
            store.commit(dishModule.SET_TABLE_LOADING, false);
        });
}

/**
 * Service Xoa 1 hoac nhieu ban ghi
 * Nguyen Ba Hai - 24/12/2022
 * @param {*} listId 
 */
export async function deleteDishService(listId) {
    await axios 
        .delete(BASE_URL, {data: listId})
        .then(async() => {
            //Render lai bang
            store.dispatch(dishAction.GET_PAGE_DATA);
        })
        .catch((error) => {
            console.log(error);
            store.commit(dishModule.SET_TABLE_LOADING, false);
        });
}

/**
 * Service Gen ma code moi
 * Nguyen Ba Hai - 24/12/2022
 * @param {*} dishName
 */
export async function genNewCodeByName(dishName) {
    await axios
        .post(`${BASE_URL}/GetNewCode`, dishName, {
            headers: {
                'Content-Type': 'application/json',
            }
        })
        .then(async(res) => {
            let dishObj = store.state.dish.dishObj;
            dishObj.dishCode = res.data;
            store.commit(dishModule.SET_DISH_OBJECT_VAL, dishObj);
            //Dung chay loading
            store.commit(dishModule.SET_FORM_LOADING, false);
        })
        .catch((error) => {
            console.log(error);
            store.commit(dishModule.SET_TABLE_LOADING, false);
        });
}

/**
 * Service Them moi hoac sua ban ghi
 * @param {*} dishObj 
 */
export async function insertOrUpdateService(dishObj) {
    await axios
        .post(BASE_URL, dishObj, {
            headers: {
                'Content-Type': 'application/json',
            }
        })
        .then(async() => {
            console.log("them thanh cong");
            store.commit(dishModule.SET_DISH_OBJECT_VAL, {
                dishTypeID: "13a507d0-342d-6292-6de3-2ceaa9cf3765",
                dishTypeName: "Món ăn",
            });
            
            //Render lai bang
            store.dispatch(dishAction.GET_PAGE_DATA);
        })
        .catch((error) => {
            console.log(error);
            store.commit(dishModule.SET_TABLE_LOADING, false);
        });
}

/**
 * Service lay hobby theo dishID
 * Nguyen Ba Hai - 28/12/2022
 */
export async function getHobbyByDishID(dishID) {
    await axios
        .post(`${BASE_URL}/GetHobbyByID`, dishID, {
            headers: {
                'Content-Type': 'application/json',
            }
        })
        .then(async(res) => {
            //Format tien
            res.data.forEach(ele => {
                for (const key in ele) {
                    if (key == "priceAdd") {
                        ele[key] = formatToSpecialTypeMoney(ele[key]);
                    }
                }
            });
            store.commit(dishModule.SET_HOBBY_ARRAY,res.data);
        })
        .catch((error) => {
            console.log(error)
        });
}

/**
 * Service Check trung ma code
 * Nguyen Ba Hai - 28/12/2022
 */
export async function checkDuplicateCode(dishCode) 
{
    await axios
        .post(`${BASE_URL}/CheckDuplicateCode`, dishCode,{
            headers: {
                'Content-Type': 'application/json',
            }
        })
        .then((res) => {
            store.commit(dishModule.IS_DUPLICATE_CODE, res.data);
        })
        .catch((error) => {
            console.log(error);
        })
}