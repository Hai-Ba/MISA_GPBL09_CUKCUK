using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.Entities
{
    public class HobbyServeDish
    {
        /// <summary>
        /// ID hobbyServeDish
        /// </summary>
        public Guid HobbyServeDishID { get; set; }

        /// <summary>
        /// ID hobby
        /// </summary>
        public Guid HobbyID { get; set; }

        /// <summary>
        /// ID cua mon an
        /// </summary>
        public string? DishID { get; set; }

        /// <summary>
        /// GIa thu them
        /// </summary>
        public string? PriceAdd { get; set; }

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
