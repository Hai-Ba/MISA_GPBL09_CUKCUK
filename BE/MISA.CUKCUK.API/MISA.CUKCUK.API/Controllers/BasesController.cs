using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MISA.CUKCUK.API.Common.Entities.DTO;
using MISA.CUKCUK.API.Common.Enums;
using MISA.CUKCUK.API.Common;
using MISA.CUKCUK.BL;
using System;

namespace MISA.CUKCUK.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BasesController<T> : ControllerBase
    {
        #region Field
        private IBaseBL<T> _baseBL;
        #endregion

        #region Constructor
        public BasesController(IBaseBL<T> baseBL)
        {
            _baseBL = baseBL;
        }
        #endregion

        #region Methods
        /// <summary>
        /// API Lấy ra bản ghi theo ID
        /// Author: Nguyễn Bá Hải
        /// Date: 3/11/2022 
        /// </summary>
        /// <param name="recordID"></param>
        /// <returns></returns>
        [HttpGet("{recordID}")]
        public IActionResult GetRecordByID([FromRoute] Guid recordID)
        {
            try
            {
                //Thuc hien goi sang BL
                var record = _baseBL.GetRecordByID(recordID);

                if (record != null)
                {
                    return StatusCode(StatusCodes.Status200OK, record);
                }
                return StatusCode(StatusCodes.Status404NotFound);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(ErrorCodes.Exception,
                    Resources.UserMsg_Exception,
                    Resources.DevMsg_Exception,
                    HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// API check trung ma
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        [HttpPost("CheckDuplicateCode")]
        public IActionResult CheckDuplicateCode([FromBody] string code) 
        {
            try
            {
                var blResponse = _baseBL.CheckDuplicateCode(code);
                if (blResponse != null) 
                {
                    return StatusCode(StatusCodes.Status200OK, blResponse.Data);
                }
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(ErrorCodes.Exception,
                    Resources.UserMsg_Exception,
                    Resources.DevMsg_Exception,
                    HttpContext.TraceIdentifier));
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(ErrorCodes.Exception,
                    Resources.UserMsg_Exception,
                    Resources.DevMsg_Exception,
                    HttpContext.TraceIdentifier));
            }

        }
        #endregion
    }
}
