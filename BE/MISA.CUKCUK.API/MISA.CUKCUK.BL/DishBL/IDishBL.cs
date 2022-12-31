using MISA.CUKCUK.API.Common;
using MISA.CUKCUK.API.Common.Entities;
using MISA.CUKCUK.API.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.BL
{
    public interface IDishBL
    {
        /// <summary>
        /// API lay danh sach DTODish hien thi va tong so ban ghi khong ke phan trang
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="limit">So ban ghi 1 trang</param>
        /// <param name="pageNumber">So trang</param>
        /// <param name="filterList">Danh sach ca truong can filter</param>
        /// <returns></returns>
        public ResponseService GetDishByPagingAndFiltering(int limit, int pageNumber, List<FilterObject> filterList);

        /// <summary>
        /// API them moi hoac sua ban ghi mon an
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public ResponseService InsertUpdateDish(DTODishAndHobbyList dishObject);

        /// <summary>
        /// API lay ma moi cho mon an
        /// Nguyen Ba Hai- 23/12/2022
        /// </summary>
        /// <param name="dishName"></param>
        /// <returns></returns>
        public ResponseService GetNewDishCode(string dishName);

        /// <summary>
        /// API lay so thich phuc vu theo DishID
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public ResponseService GetHobbiesByDishID(string dishID);

        /// <summary>
        /// API xóa mot hoac nhieu bản ghi Dish
        /// Author: Nguyễn Bá Hải
        /// Date:22/12/2022
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public ResponseService DeleteRecord(string[] listID);
    }
}
