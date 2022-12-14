using MISA.CUKCUK.API.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.Entities.DTO
{
    public class ErrorResult
    {
        #region Property

        /// <summary>
        /// Định danh của mã lỗi nội bộ
        /// </summary>
        public ErrorCodes ErrorCode { get; set; }

        /// <summary>
        /// Thông báo cho user. Không bắt buộc, tùy theo đặc thù từng dịch vụ và client tích hợp
        /// </summary>
        public string? UserMsg { get; set; }

        /// <summary>
        /// Thông báo cho Dev. Thông báo ngắn gọn để thông báo cho Dev biết vấn đề đang gặp phải
        /// </summary>
        public string? DevMsg { get; set; }

        /// <summary>
        /// Thông tin chi tiết hơn về vấn đề. Ví dụ: Đường dẫn mô tả về mã lỗi
        /// </summary>
        public string? MoreInfo { get; set; }

        /// <summary>
        /// Mã để tra cứu thông tin log: ELK, file log,...
        /// </summary>
        public string? TraceId { get; set; }

        #endregion

        #region Constructors
        public ErrorResult(ErrorCodes errorCode, string? userMsg, string? devMsg, string? moreInfo, string? traceId)
        {
            ErrorCode = errorCode;
            UserMsg = userMsg;
            DevMsg = devMsg;
            MoreInfo = moreInfo;
            TraceId = traceId;
        }

        public ErrorResult(ErrorCodes errorCode, string? userMsg, string? traceId)
        {
            ErrorCode = errorCode;
            UserMsg = userMsg;
            TraceId = traceId;
        }

        public ErrorResult(ErrorCodes errorCode, string? userMsg, string? devMsg, string? traceId)
        {
            ErrorCode = errorCode;
            UserMsg = userMsg;
            DevMsg = devMsg;
            TraceId = traceId;
        } 
        #endregion
    }
}
