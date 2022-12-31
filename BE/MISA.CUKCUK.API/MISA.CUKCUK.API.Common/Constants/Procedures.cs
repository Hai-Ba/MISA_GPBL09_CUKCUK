using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common
{
    public class Procedures
    {
        /// <summary>
        /// Tên proc lấy tất cả bản ghi
        /// </summary>
        public static string GET_ALL = "Proc_{0}_GetAll";

        /// <summary>
        /// Tên proc lấy tất bản ghi theo ID
        /// </summary>
        public static string GET_BY_ID = "Proc_{0}_GetByID";

        /// <summary>
        /// Tên proc lấy tất bản ghi và phân trang, tìm kiếm
        /// </summary>
        public static string GET_PAGING = "Proc_{0}_GetPaging";

        /// <summary>
        /// Tên proc lấy ID lớn nhất
        /// </summary>
        public static string GET_BIGGEST_ID = "Proc_{0}_GetBiggestID";

        /// <summary>
        /// Tên proc xóa 1 bản ghi theo ID
        /// </summary>
        public static string DELETE = "Proc_{0}_Delete";

        /// <summary>
        /// Tên proc thêm 1 bản ghi
        /// </summary>
        public static string INSERT = "Proc_{0}_Insert";

        /// <summary>
        /// Tên proc thêm 1 bản ghi
        /// </summary>
        public static string UPDATE = "Proc_{0}_Update";

        /// <summary>
        /// Tên proc thêm 1 bản ghi
        /// </summary>
        public static string CHECK_DUPLICATE = "Proc_{0}_CheckDuplicateCode";

        /// <summary>
        /// Lay ban ghi theo ID 1 bang khac
        /// </summary>
        public static string GET_BY_ORTHER_ID = "Proc_{0}_GetBy{1}ID";

        /// <summary>
        /// Xoa ban ghi theo ID 1 bang khac
        /// </summary>
        public static string DELETE_BY_ORTHER_ID = "Proc_{0}_DeleteBy{1}ID";
    }
}
