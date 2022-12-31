using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.Entities.DTO
{
    public class FilterObject
    {
        #region Properties
        /// <summary>
        /// Truong filter
        /// </summary>
        public string Field { get; set; }

        /// <summary>
        /// Gia tri filter
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Loai filter
        /// </summary>
        public int Type { get; set; }
        #endregion

        #region Constructors
        public FilterObject(string field, string value, int type) 
        {
            Field = field;
            Value = value;
            Type = type;
        }
        #endregion
    }
}
