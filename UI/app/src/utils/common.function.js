/**
 * Hàm format tiền theo dạng xxx.xxx.xxx,xx
 * @param {} name 
 * @returns 
 */
export function formatToSpecialTypeMoney(money) { //Money co dang bt(5000 or 5000.0000) sang 5.000,00
    let moneyFormatted = new Intl.NumberFormat('de-DE', { style: 'currency', currency: 'EUR' }).format(money).toString().slice(0, -2);
    return moneyFormatted;
}

/**
 * Hàm format tiền để có thể gọi api(chuyen tu dang x.xxx.xxx,xx sang dang xxxxxxx.xx)
 * @param {} name 
 * @returns 
 */
export function formatToDefaultMoney(money) {
    let moneyStr = money.toString().trim();
    let beforeComma = moneyStr.split(",")[0].replace(".","");
    let afterComma = moneyStr.split(",")[1];
    moneyStr = beforeComma + "." + afterComma
    return moneyStr;
}