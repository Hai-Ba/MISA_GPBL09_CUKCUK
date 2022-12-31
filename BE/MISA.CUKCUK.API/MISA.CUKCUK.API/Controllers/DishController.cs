using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using MySqlConnector;
using System.Resources;
using System;
using MISA.CUKCUK.API.Common.Entities.DTO;
using Dapper;
using MISA.CUKCUK.API.Common.Enums;
using MISA.CUKCUK.API.Common.Entities;
using MISA.CUKCUK.API.Common;
using System.Data;
using MISA.CUKCUK.BL;

namespace MISA.CUKCUK.API.Controllers
{
    [ApiController]
    public class DishController : BasesController<Dish>
    {
        #region Properties
        private IDishBL _dishBL;
        #endregion

        #region Constructor
        public DishController(IDishBL dishBL) : base((IBaseBL<Dish>)dishBL)
        {
            _dishBL = dishBL;
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
        [HttpPost("filter")]
        public IActionResult GetDishByPagingAndFiltering([FromQuery] int limit, [FromQuery] int pageNumber, [FromBody] List<FilterObject> filterList)
        {
            try
            {
                //Goi bao BL de lay key qua
                var blResponse = _dishBL.GetDishByPagingAndFiltering(limit, pageNumber, filterList);
                if (blResponse.IsSuccess) 
                {
                    return StatusCode(StatusCodes.Status200OK, blResponse.Data);
                }
                return StatusCode(StatusCodes.Status404NotFound, new ErrorResult(ErrorCodes.DataNotFound, 
                    Resources.UserMsg_DataNotFound, 
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

        /// <summary>
        /// API Them moi(khi khong truyen ID), Sua ban ghi khi tuyen ID
        /// Nguyen Ba Hai - 22/12/2022
        /// </summary>
        /// <param name="dish"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult InsertUpdateDish([FromBody] DTODishAndHobbyList dishObject) 
        {
            try
            {
                var blResponse = _dishBL.InsertUpdateDish(dishObject);
                int statusCode = StatusCodes.Status201Created; //Default
                var returnObj = new Object();
                if (blResponse.IsSuccess)
                {
                    returnObj = blResponse.Data;
                }
                else 
                {
                    switch (blResponse.Data) 
                    {
                        case ErrorCodes.MissingField:
                            statusCode = StatusCodes.Status400BadRequest;
                            returnObj = new ErrorResult(ErrorCodes.MissingField,
                                                        Resources.UserMsg_MissingInput,
                                                        HttpContext.TraceIdentifier);
                            break;
                        case ErrorCodes.DuplicateCode:
                            statusCode = StatusCodes.Status400BadRequest;
                            returnObj = new ErrorResult(ErrorCodes.DuplicateCode,
                                                        Resources.UserMsg_DuplicateCode,
                                                        HttpContext.TraceIdentifier);
                            break;
                        case ErrorCodes.Max25Characters:
                            statusCode = StatusCodes.Status400BadRequest;
                            returnObj = new ErrorResult(ErrorCodes.Max25Characters,
                                                        Resources.UserMsg_WrongInputFormat,
                                                        HttpContext.TraceIdentifier);
                            break;
                        case ErrorCodes.Max128Characters:
                            statusCode = StatusCodes.Status400BadRequest;
                            returnObj = new ErrorResult(ErrorCodes.Max128Characters,
                                                        Resources.UserMsg_WrongInputFormat,
                                                        HttpContext.TraceIdentifier);
                            break;
                        case ErrorCodes.Max255Characters:
                            statusCode = StatusCodes.Status400BadRequest;
                            returnObj = new ErrorResult(ErrorCodes.Max255Characters,
                                                        Resources.UserMsg_WrongInputFormat,
                                                        HttpContext.TraceIdentifier);
                            break;
                        case ErrorCodes.OnlyUpperCaseLetter:
                            statusCode = StatusCodes.Status400BadRequest;
                            returnObj = new ErrorResult(ErrorCodes.OnlyUpperCaseLetter,
                                                        Resources.UserMsg_WrongInputFormat,
                                                        HttpContext.TraceIdentifier);
                            break;
                        case ErrorCodes.DuplicateItem:
                            statusCode = StatusCodes.Status400BadRequest;
                            returnObj = new ErrorResult(ErrorCodes.DuplicateItem,
                                                        Resources.UserMsg_DuplicateItem,
                                                        HttpContext.TraceIdentifier);
                            break;
                        case ErrorCodes.Exception:
                            statusCode = StatusCodes.Status500InternalServerError;
                            returnObj = new ErrorResult(ErrorCodes.Exception,
                                                        Resources.UserMsg_Exception,
                                                        Resources.DevMsg_Exception,
                                                        HttpContext.TraceIdentifier);
                            break;
                    }
                }
                return StatusCode(statusCode, returnObj);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, new ErrorResult(ErrorCodes.Exception,
                    Resources.UserMsg_Exception,
                    Resources.DevMsg_Exception,
                    HttpContext.TraceIdentifier));
            }
        }

        /// <summary>
        /// API lay ma moi theo ten truyen vao
        /// Nguyen Ba Hai - 24/12/2022
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        [HttpPost("GetNewCode")]
        public IActionResult GetNewDishCode([FromBody] string name) 
        {
            try
            {
                if (name != "" && name != null) 
                {
                    var blResponse = _dishBL.GetNewDishCode(name);
                    return StatusCode(StatusCodes.Status200OK, blResponse.Data);
                }
                return StatusCode(StatusCodes.Status400BadRequest, new ErrorResult(ErrorCodes.MissingField,
                                    Resources.UserMsg_DataNotFound,
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

        /// <summary>
        /// API lay so thich phuc vu the ID
        /// Nguyen Ba Hai - 27/12/2022
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpPost("GetHobbyByID")]
        public IActionResult GetHobbyByDishID([FromBody] string id) 
        {
            try
            {
                var blResponse = _dishBL.GetHobbiesByDishID(id);
                return StatusCode(StatusCodes.Status200OK, blResponse.Data);
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
        /// API xóa mot hoac nhieu bản ghi Dish
        /// Author: Nguyễn Bá Hải
        /// Date:3/11/2022
        /// </summary>
        /// <param name="listID"></param>
        /// <returns></returns>
        [HttpDelete]
        public IActionResult DeleteRecord([FromBody] string[] listID)
        {
            try
            {
                var blResponse = _dishBL.DeleteRecord(listID);
                if (blResponse.IsSuccess)
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
