using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.Entities.DTO
{
    public class DTOPagingData<T>
    {
        #region Properties
        //Danh sach gia tri bieu dien boi mang
        public List<T>? List { get; set; }

        //Tong so ban ghi duoc tim thay khong ke phan trang
        public int? Total { get; set; }
        #endregion

        #region Constructors

        public DTOPagingData()
        {
        }

        public DTOPagingData(List<T> list, int total)
        {
            List = list;
            Total = total;
        } 
        #endregion
    }
}
