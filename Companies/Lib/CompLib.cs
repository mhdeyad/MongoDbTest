using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Companies.Lib
{
   public enum FieldDataType
    { 
      Intype,
      stringtype,
      Datetype
    }
    public enum UserCollection
    {
        Company,
        Contact
     }
    public class FieldType
    {
        public string Name { get; set; }
        public FieldDataType DataType { get; set; }
        public string Value { get; set; }
    }
}
