using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.CUKCUK.API.Common.CustomAttributes
{
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field)]
    public class MyAttributes : Attribute
    {
        public MyAttributes() { }
        public bool KeyProp { get; set; }
        public bool RequiredProp { get; set; }
        public bool MaxLength128 { get; set; }
        public bool MaxLength255 { get; set; }
        public bool MaxLength25 { get; set; }
        public bool OnlyUpperCaseLetterAndNumber { get; set; }
    }
}
