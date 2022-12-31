using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.Enums
{
    public enum ErrorCodes
    {
        //TRường hợp xảy ra ngoại lệ, thông báo cho Misa
        Exception = 1,

        //Trường hợp trùng mã
        DuplicateCode = 2,

        //Trường hợp dữ liệu không hợp lệ
        InvalidData = 3,

        //TRường hợp không tìm thấy dữ liệu
        DataNotFound = 4,

        //Trường dữ liệu chưa được điền
        MissingField = 5,

        //Trường dữ liệu sai định dạng
        OnlyUpperCaseLetter = 6,

        //Trường dữ liệu sai định dạng
        Max25Characters = 7,

        //Trường dữ liệu sai định dạng
        Max128Characters = 8,

        //Trường dữ liệu sai định dạng
        Max255Characters = 9,

        //Trường hợp trùng dữ liệu của mảng
        DuplicateItem = 10,
    }
}
