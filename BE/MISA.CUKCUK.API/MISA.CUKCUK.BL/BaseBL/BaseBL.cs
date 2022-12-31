using MISA.CUKCUK.API.Common;
using MISA.CUKCUK.API.Common.Enums;
using MISA.CUKCUK.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MISA.CUKCUK.BL
{
    public class BaseBL<T> : IBaseBL<T>
    {
        #region Field
        private IBaseDL<T> _baseDL;
        #endregion

        #region Constructor
        public BaseBL(IBaseDL<T> baseDL)
        {
            _baseDL = baseDL;
        }

        #endregion

        #region Methods
    

        /// <summary>
        /// Lấy thông tin 1 ban ghi theo id
        /// Author: Nguyễn Bá Hải
        /// Date: 22/12/2022
        /// </summary>
        /// <param name="employeeID">ID nhân viên muốn lấy</param>
        /// <returns>Thông tin 1 nhân viên</returns>
        public T GetRecordByID(Guid recordID)
        {
            return _baseDL.GetRecordByID(recordID);
        }

        /// <summary>
        /// API check trung ma
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public ResponseService CheckDuplicateCode(string code) 
        {
            var dlResponse = _baseDL.CheckDuplicateCode(code);
            return new ResponseService(true, dlResponse);
        }

        /// <summary>
        /// Hàm check số input khong qua 128 kí tự
        /// Nguyen Ba Hai - 25/12/2022
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool MaxLength128Validation(string? str) 
        {
            if (str != null && str.Length <= 128)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hàm check số input khong qua 255 kí tự
        /// Nguyen Ba Hai - 25/12/2022
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool MaxLength255Validation(string? str)
        {
            if (str != null && str.Length <= 255)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hàm check số input khong qua 25 kí tự
        /// Nguyen Ba Hai - 25/12/2022
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool MaxLength25Validation(string? str)
        {
            if (str != null && str.Length <= 25)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Hàm check chi bao gom chu viet hoa va so
        /// Nguyen Ba Hai - 25/12/2022
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public bool OnlyUpperCaseLetterAndNumberValidation(string? str)
        {
            //Regex codeRG = new Regex(@"^[A-Z0-9]$");
            //if (str != null)
            //{
            //    return codeRG.IsMatch(str);
            //}
            //return true;
            if (str.Any(char.IsUpper)) 
            {
                return true;
            }
            return false;
        }
        #endregion
    }
}
