using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    public class FieldType: IFieldType
    {
        public string Name { get; set; }
        public FieldDataTypes FieldDataType { get; set; }
        public object DefaultValue { get; set; }
        public FieldRoles Role { get; set; }
        public int? SortOrder { get; set; }
        public bool IsMandatory { get; set; }
        public bool IsValueDerived { get; set; }
        public bool IsHidden { get; set; }
        public object ShowConditions { get; set; }
        public string InputMask { get; set; }
        public string ShowFormat { get; set; }
        public object NullValueReplacement { get; set; }
    }
}
