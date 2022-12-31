using MISA.CUKCUK.API.Common;
using MISA.CUKCUK.API.Common.CustomAttributes;
using MISA.CUKCUK.API.Common.Entities;
using MISA.CUKCUK.API.Common.Entities.DTO;
using MISA.CUKCUK.API.Common.Enums;
using MISA.CUKCUK.DL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.BL
{
    public class DishBL : BaseBL<Dish>, IDishBL
    {
        #region Properties
        private IDishDL _dishDL;
        #endregion

        #region Constructors
        public DishBL(IDishDL dishDL) : base(dishDL)
        {
            _dishDL = dishDL;
        }
        #endregion


        #region Methods
        /// <summary>
        /// API lay danh sach DTODish hien thi va tong so ban ghi khong ke phan trang
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="limit">So ban ghi 1 trang</param>
        /// <param name="pageNumber">So trang</param>
        /// <param name="filterList">Danh sach ca truong can filter</param>
        /// <returns></returns>
        public ResponseService GetDishByPagingAndFiltering(int limit, int pageNumber, List<FilterObject> filterList)
        {
            //Gen filter string
            int offset = (pageNumber - 1) * limit;      //Param: index of start get record in DB
            string filterAndSortQuery = "SELECT d1.DishTypeName,\r\n" +
                                                "d.DishID,\r\n" +
                                                "d.DishTypeID,\r\n" +
                                                "d.DishCode,\r\n" +
                                                "d.DishName,\r\n" +
                                                "c.CategoryName,\r\n" +
                                                "d.CategoryID,\r\n" +
                                                "u.UnitName,\r\n" +
                                                "d.UnitID,\r\n" +
                                                "d.CookPlaceID,\r\n" +
                                                "c1.CookPlaceName,\r\n" +
                                                "d.PrimeCost,\r\n" +
                                                "d.SalePrice,\r\n" +
                                                "d.PriceChangeInTime,\r\n" +
                                                "d.FreeModifyPrice,\r\n" +
                                                "d.Description,\r\n" +
                                                "d.ShowOnMenu\r\n" +
                                        "FROM dish d\r\n" +
                                        "LEFT JOIN unit u ON d.UnitID = u.UnitID\r\n" +
                                        "LEFT JOIN category c ON d.CategoryID = c.CategoryID\r\n" +
                                        "LEFT JOIN cookplace c1 ON d.CookPlaceID = c1.CookPlaceID\r\n" +
                                        "LEFT JOIN dishtype d1 ON d.DishTypeID = d1.DishTypeID\r\n";

            string totalQuery = "SELECT COUNT(d.DishID) FROM dish d\r\n" +
                                        "LEFT JOIN unit u ON d.UnitID = u.UnitID\r\n" +
                                        "LEFT JOIN category c ON d.CategoryID = c.CategoryID\r\n" +
                                        "LEFT JOIN cookplace c1 ON d.CookPlaceID = c1.CookPlaceID\r\n" +
                                        "LEFT JOIN dishtype d1 ON d.DishTypeID = d1.DishTypeID\r\n";
            if (filterList.Count > 0) 
            {
                filterAndSortQuery += " WHERE ";
                totalQuery += " WHERE ";
                foreach (FilterObject filterObject in filterList)//Duyet tung phan tu cua filterList
                {
                    string stringFilter = GenStringFilter(filterObject.Type, filterObject.Value);
                    switch (filterObject.Field) 
                    {
                        case "dishTypeName":
                            filterAndSortQuery += $" d1.DishTypeName {stringFilter} AND";
                            totalQuery += $" d1.DishTypeName {stringFilter} AND";
                            break;
                        case "dishCode":
                            filterAndSortQuery += $" d.DishCode {stringFilter} AND";
                            totalQuery += $" d.DishCode {stringFilter} AND";
                            break;
                        case "dishName":
                            filterAndSortQuery += $" d.DishName {stringFilter} AND";
                            totalQuery += $" d.DishName {stringFilter} AND";
                            break;
                        case "categoryName":
                            filterAndSortQuery += $" c.CategoryName {stringFilter} AND";
                            totalQuery += $" c.CategoryName {stringFilter} AND";
                            break;
                        case "unitName":
                            filterAndSortQuery += $" u.UnitName {stringFilter} AND";
                            totalQuery += $" u.UnitName {stringFilter} AND";
                            break;
                        case "primeCost":
                            filterAndSortQuery += $" d.PrimeCost {stringFilter} AND";
                            totalQuery += $" d.PrimeCost {stringFilter} AND";
                            break;
                        case "salePrice":
                            filterAndSortQuery += $" d.SalePrice {stringFilter} AND";
                            totalQuery += $" d.SalePrice {stringFilter} AND";
                            break;
                        case "priceChangeInTime":
                            filterAndSortQuery += $" d.PriceChangeInTime {stringFilter} AND";
                            totalQuery += $" d.PriceChangeInTime {stringFilter} AND";
                            break;
                        case "freeModifyPrice":
                            filterAndSortQuery += $" d.FreeModifyPrice {stringFilter} AND";
                            totalQuery += $" d.FreeModifyPrice {stringFilter} AND";
                            break;
                        case "showOnMenu":
                            filterAndSortQuery += $" d.ShowOnMenu {stringFilter} AND";
                            totalQuery += $" d.ShowOnMenu {stringFilter} AND";
                            break;
                        default: break;
                    }
                }
                //Xoa ki tu AND cuoi cung
                filterAndSortQuery = filterAndSortQuery.Remove(filterAndSortQuery.Length-3);
                totalQuery = totalQuery.Remove(totalQuery.Length-3);
            }
            //Goi vao DL de lay ket qua
            var dlResponse = _dishDL.GetDishByPagingAndFiltering(limit, offset, filterAndSortQuery, totalQuery);

            //Tra ve ket qua goi tu DL
            if (dlResponse.List != null) 
            {
                return new ResponseService(true, dlResponse);
            }
            return new ResponseService(false, null);
        }

        /// <summary>
        /// API them moi hoac sua ban ghi mon an
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public ResponseService InsertUpdateDish(DTODishAndHobbyList dishObject)
        {
            ResponseService dishValidated = ValidateData(dishObject);
            //Goi lay ma code cu de xem ma code co thay doi khong 
            //Neu ma code thay doi thi moi check trung ma
            if (dishObject.Dish != null && dishObject.Dish.DishID != null) //Check ma code cho sua ban ghi
            {
                var dishPresentCode = _dishDL.GetRecordByID(dishObject.Dish.DishID).DishCode;
                if (dishPresentCode != dishObject.Dish.DishCode && dishObject.Dish.DishCode != null && _dishDL.CheckDuplicateCode(dishObject.Dish.DishCode) == 1) //Check trung ma
                {
                    return new ResponseService(false, ErrorCodes.DuplicateCode);
                }
            }
            else //Check trung ma cho them moi
            {
                if (dishObject.Dish.DishCode != null && _dishDL.CheckDuplicateCode(dishObject.Dish.DishCode) == 1) //Check trung ma
                {
                    return new ResponseService(false, ErrorCodes.DuplicateCode);
                }
            }
            if (dishValidated.IsSuccess)
            {
                //Goi vao Dl de lay du lieu
                var dlResponse = _dishDL.InsertUpdateDish(dishObject);
                if (dlResponse != Guid.Empty && dlResponse != null)
                {
                    return new ResponseService(true, dlResponse);
                }
                return new ResponseService(false, ErrorCodes.Exception);
            }
            return dishValidated;
        }

        /// <summary>
        /// API lay ma moi cho mon an
        /// Nguyen Ba Hai- 23/12/2022
        /// </summary>
        /// <param name="dishName"></param>
        /// <returns></returns>
        public ResponseService GetNewDishCode(string dishName)
        {
            //Xu li va tao ra ma CODE
            string dishCode = NonUnicode(dishName.Trim()).ToUpper();
            string[] arrString = dishCode.Split(" ");
            if (arrString.Length == 1)
            {
                //Chuoi co 1 tu
                return new ResponseService(true, dishCode);
            }
            else 
            {
                //GHep chuoi
                string stringCodeFirstLetter = "";
                string stringCodeFullLetter = "";
                //TH1: Lay chu cai dau tien viet hoa
                foreach (string str in arrString) 
                {
                    stringCodeFirstLetter = string.Concat(stringCodeFirstLetter, str[0]);
                    stringCodeFullLetter = string.Concat(stringCodeFullLetter, str);
                    
                }
                if (_dishDL.CheckDuplicateCode(stringCodeFirstLetter) > 0)//Bi trung ma
                {
                    return new ResponseService(true, stringCodeFullLetter);
                }
                return new ResponseService(true, stringCodeFirstLetter);
            }
        }

        /// <summary>
        /// API lay so thich phuc vu theo DishID
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public ResponseService GetHobbiesByDishID(string dishID) 
        {
            return new ResponseService(true, _dishDL.GetHobbiesByDishID(dishID));
        }

        /// <summary>
        /// API xóa mot hoac nhieu bản ghi
        /// Author: Nguyễn Bá Hải
        /// Date:22/12/2022
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public ResponseService DeleteRecord(string[] listID)
        {


            //Goi vao DL
            int dlResponse = _dishDL.DeleteRecord(listID);
            if (dlResponse == -1)
            {
                return new ResponseService(false, ErrorCodes.Exception);
            }
            return new ResponseService(true, dlResponse);
        }

        /// <summary>
        /// Ham gen ra cac string filter de tao ra cau lenh query
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private static string GenStringFilter(int type, string value)
        {
            if (value == "" || value == null)
            {
                return $"LIKE \"%%\"";
            }
            switch (type)
            {
                case (int)FilterType.Like:
                    return $"LIKE \"%{value}%\"";
                case (int)FilterType.BeginWith:
                    return $"LIKE \"{value}%\"";
                case (int)FilterType.EndWith:
                    return $"LIKE \"%{value}\"";
                case (int)FilterType.NotLike:
                    return $"NOT LIKE \"%{value}%\"";
                case (int)FilterType.Equal:
                    return $"= \"{value}\"";
                case (int)FilterType.Smaller:
                    return $"< \"{value}\"";
                case (int)FilterType.SmallerEqual:
                    return $"<= \"{value}\"";
                case (int)FilterType.Higher:
                    return $"> \"{value}\"";
                case (int)FilterType.HigherEqual:
                    return $">= \"{value}\"";
                case (int)FilterType.FilterTrueFalse:
                    return $"= \"{value}\"";
                default:
                    return $"= \"{value}\"";
            }
        }

        /// <summary>
        /// Ham chuyen tieng viet co dau thanh khong dau
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        private static string NonUnicode(string text)
        {
            string[] arr1 = new string[] { "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ",
    "đ",
    "é","è","ẻ","ẽ","ẹ","ê","ế","ề","ể","ễ","ệ",
    "í","ì","ỉ","ĩ","ị",
    "ó","ò","ỏ","õ","ọ","ô","ố","ồ","ổ","ỗ","ộ","ơ","ớ","ờ","ở","ỡ","ợ",
    "ú","ù","ủ","ũ","ụ","ư","ứ","ừ","ử","ữ","ự",
    "ý","ỳ","ỷ","ỹ","ỵ",};
            string[] arr2 = new string[] { "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a", "a",
    "d",
    "e","e","e","e","e","e","e","e","e","e","e",
    "i","i","i","i","i",
    "o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o","o",
    "u","u","u","u","u","u","u","u","u","u","u",
    "y","y","y","y","y",};
            for (int i = 0; i < arr1.Length; i++)
            {
                text = text.Replace(arr1[i], arr2[i]);
                text = text.Replace(arr1[i].ToUpper(), arr2[i].ToUpper());
            }
            return text;
        }

        /// <summary>
        /// Hàm validate input cho Dish entity
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        private ResponseService ValidateData(DTODishAndHobbyList dishObject) 
        {
            //Validate cho dish
            if (dishObject.Dish != null) 
            {
                var validateProperties = dishObject.Dish.GetType().GetProperties().Where(prop => Attribute.IsDefined(prop, typeof(MyAttributes)));
                foreach (var prop in validateProperties) 
                {
                    //Lấy giá trị prop truyền lên
                    var propVal = prop.GetValue(dishObject.Dish);
                    var propName = prop.Name;
                    var attr = prop.GetCustomAttributes(typeof(MyAttributes), true).FirstOrDefault();

                    if ((attr as MyAttributes).RequiredProp)
                    {
                        //Validate cho require prop
                        if (propVal == null || propVal.ToString().Trim() == "")
                        {
                            return new ResponseService(false, ErrorCodes.MissingField);
                        }
                    }

                    if ((attr as MyAttributes).MaxLength25)
                    {
                        //Validate cho Age
                        if (propVal != null && propVal.ToString().Trim() != "")
                        {
                            if (MaxLength25Validation(propVal.ToString()) == false)
                            {
                                return new ResponseService(false, ErrorCodes.Max25Characters);
                            }
                        }
                    }

                    if ((attr as MyAttributes).MaxLength128)
                    {
                        //Validate cho Age
                        if (propVal != null && propVal.ToString().Trim() != "")
                        {
                            if (MaxLength128Validation(propVal.ToString()) == false)
                            {
                                return new ResponseService(false, ErrorCodes.Max128Characters);
                            }
                        }
                    }

                    if ((attr as MyAttributes).MaxLength255)
                    {
                        //Validate cho Age
                        if (propVal != null && propVal.ToString().Trim() != "")
                        {
                            if (MaxLength255Validation(propVal.ToString()) == false)
                            {
                                return new ResponseService(false, ErrorCodes.Max255Characters);
                            }
                        }
                    }

                    if ((attr as MyAttributes).OnlyUpperCaseLetterAndNumber)
                    {
                        //Validate cho Age
                        if (propVal != null && propVal.ToString().Trim() != "")
                        {
                            if (OnlyUpperCaseLetterAndNumberValidation(propVal.ToString()) == false)
                            {
                                return new ResponseService(false, ErrorCodes.OnlyUpperCaseLetter);
                            }
                        }
                    }
                }
            }
            //Validate cho Hobby List
            if (dishObject.HobbyList != null && dishObject.HobbyList.Count > 0) 
            {
                var newHobbyList = new List<Hobby>();
                //Loai bo phan tu rong
                foreach (var hobby in dishObject.HobbyList) 
                {
                    if (hobby.HobbyID != Guid.Empty) 
                    {   //Lay gia tri khong null
                        newHobbyList.Add(hobby);
                    }
                }

                if (newHobbyList.Count > 1) 
                {
                    for (int i = 0; i < newHobbyList.Count; i++) 
                    {
                        for (int j = i + 1; j < newHobbyList.Count; j++) 
                        {
                            if (newHobbyList[i].HobbyID.Equals(newHobbyList[j].HobbyID)) 
                            {//Neu co 2 obj trung nhau thi return false
                                return new ResponseService(false, ErrorCodes.DuplicateItem);
                            }
                        }
                    }
                }
                dishObject.HobbyList = newHobbyList;
            }
            return new ResponseService(true, dishObject);
        }


        #endregion
    }
}
