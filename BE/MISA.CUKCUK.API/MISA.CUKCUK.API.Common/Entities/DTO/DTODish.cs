using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.Entities.DTO
{
    public class DTODish
    {
        /// <summary>
        /// ID món ăn
        /// </summary>
        public Guid DishID { get; set; }

        /// <summary>
        /// Mã món ăn
        /// </summary>
        public string? DishCode { get; set; }

        /// <summary>
        /// Tên món ăn
        /// </summary>
        public string? DishName { get; set; }

        /// <summary>
        /// Giá bán
        /// </summary>
        public decimal? SalePrice { get; set; }

        /// <summary>
        /// Giá vốn
        /// </summary>
        public decimal? PrimeCost { get; set; }

        /// <summary>
        /// Không hiển thị trên thực đơn
        /// </summary>
        public int? ShowOnMenu { get; set; }

        /// <summary>
        /// Tư do chỉnh sửa giá
        /// </summary>
        public int? FreeModifyPrice { get; set; }

        /// <summary>
        /// Thay đổi theo thời giá
        /// </summary>
        public int? PriceChangeInTime { get; set; }

        /// <summary>
        /// Tên loại món
        /// </summary>
        public string? DishTypeName { get; set; }

        /// <summary>
        /// ID loại món
        /// </summary>
        public Guid? DishTypeID { get; set; }

        /// <summary>
        /// Tên đơn vị
        /// </summary>
        public string? UnitName { get; set; }

        /// <summary>
        /// ID đơn vị
        /// </summary>
        public Guid? UnitID { get; set; }

        /// <summary>
        /// Tên nhóm
        /// </summary>
        public string? CategoryName { get; set; }

        /// <summary>
        /// ID nhóm
        /// </summary>
        public Guid? CategoryID { get; set; }

        /// <summary>
        /// Tên nơi chế biến
        /// </summary>
        public string? CookPlaceName { get; set; }

        /// <summary>
        /// ID nơi chế biến
        /// </summary>
        public Guid? CookPlaceID { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }
    }
}
