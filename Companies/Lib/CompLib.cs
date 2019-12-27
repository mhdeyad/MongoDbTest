using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.Lib
{
   public enum PropertyDataType
    { 
      Intype,
      stringtype,
      Datetype
    }
    public class PropertyType
    {
        public string Name { get; set; }
        public PropertyDataType DataType { get; set; }
        public string Value { get; set; }
    }
}
