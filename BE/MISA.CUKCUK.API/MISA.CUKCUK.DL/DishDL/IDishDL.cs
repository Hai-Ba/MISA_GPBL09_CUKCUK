using MISA.CUKCUK.API.Common.Entities;
using MISA.CUKCUK.API.Common.Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.DL
{
    public interface IDishDL : IBaseDL<Dish>
    {
        /// <summary>
        /// API lay danh sach DTODish hien thi va tong so ban ghi khong ke phan trang
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="limit"></param>
        /// <param name="pageNumber"></param>
        /// <param name="filterAndSortQuery">Query lay danh sach ban ghi bang filter, paging</param>
        /// <param name="totalQuery"> Query lay tong so ban ghi</param>
        /// <returns></returns>
        public DTOPagingData<DTODish> GetDishByPagingAndFiltering(int limit, int pageNumber, string filterAndSortQuery, string totalQuery);

        /// <summary>
        /// API them moi hoac sua ban ghi mon an
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        public Guid? InsertUpdateDish(DTODishAndHobbyList dishObject);

        /// <summary>
        /// API lay so thich phuc vu theo ID
        /// Nguyen Ba Hai - 26/12/2022
        /// </summary>
        /// <param name="dishID"></param>
        /// <returns></returns>
        public List<Hobby> GetHobbiesByDishID(string dishID);

        /// <summary>
        /// API xóa mot hoac nhieu bản ghi
        /// Author: Nguyễn Bá Hải
        /// Date:22/12/2022
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        public int DeleteRecord(string[] listID);
    }
}
