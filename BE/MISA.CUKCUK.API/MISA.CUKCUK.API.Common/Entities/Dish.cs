using MISA.CUKCUK.API.Common.CustomAttributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.Entities
{
    public class Dish
    {
        /// <summary>
        /// ID món ăn
        /// </summary>
        [MyAttributes(KeyProp = true)]
        public Guid? DishID { get; set; }

        /// <summary>
        /// Mã món ăn
        /// </summary>
        [MyAttributes(RequiredProp = true, MaxLength25 = true, OnlyUpperCaseLetterAndNumber = true)]
        public string? DishCode { get; set; }

        /// <summary>
        /// Tên món ăn
        /// </summary>
        [MyAttributes(RequiredProp = true, MaxLength128 = true)]
        public string? DishName { get; set; }

        /// <summary>
        /// GIá bán
        /// </summary>
        [MyAttributes(RequiredProp = true)]
        public decimal? SalePrice { get; set; }

        /// <summary>
        /// Giá vốn
        /// </summary>
        public decimal? PrimeCost { get; set; }

        /// <summary>
        /// Mô tả
        /// </summary>
        public string? Description { get; set; }

        /// <summary>
        /// không hiên thị trên thực đơn
        /// </summary>
        public int? ShowOnMenu { get; set; }

        /// <summary>
        /// Tự do điều chỉnh giá
        /// </summary>
        public int? FreeModifyPrice { get; set; }

        /// <summary>
        /// Thay đổi theo thời gian
        /// </summary>
        public int? PriceChangeInTime { get; set; }

        /// <summary>
        /// ID loại món
        /// </summary>
        [MyAttributes(RequiredProp = true)]
        public Guid DishTypeID { get; set; }

        /// <summary>
        /// ID Noi nấu
        /// </summary>
        public Guid CookPlaceID { get; set; }

        /// <summary>
        /// ID đơn vị
        /// </summary>
        [MyAttributes(RequiredProp = true)]
        public Guid UnitID { get; set; }

        /// <summary>
        /// ID nhóm thực đơn
        /// </summary>
        public Guid CategoryID { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Người tạo
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        public string? ModifiedBy { get; set; }
    }
}
