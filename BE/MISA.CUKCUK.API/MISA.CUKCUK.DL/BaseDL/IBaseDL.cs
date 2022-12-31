using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.DL
{
    public interface IBaseDL<T>
    {
        /// <summary>
        /// Lấy thông tin 1 bản ghi theo id
        /// Author: Nguyễn Bá Hải
        /// Date: 22/12/2022
        /// </summary>
        /// <param name="recordID">ID bản ghi muốn lấy</param>
        /// <returns>Thông tin 1 bản ghi</returns>
        public T GetRecordByID(Guid? recordID);

        /// <summary>
        /// API goi vao db check trung ma
        /// Nguyen Ba Hai- 23/12/2022
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public int CheckDuplicateCode(string code);

    }
}
