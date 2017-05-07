using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediaLibrary.Interfaces;
using System.Xml.Serialization;

namespace MediaLibrary.Entities
{
    [Serializable]
    public class FieldType: IFieldType
    {
        [XmlElement(nameof(Name))]
        public string Name { get; set; }

        [XmlElement(nameof(FieldDataType))]
        public FieldDataTypes FieldDataType { get; set; }

        [XmlElement(nameof(DefaultValue))]
        public object DefaultValue { get; set; }

        [XmlElement(nameof(Role))]
        public FieldRoles Role { get; set; }

        [XmlElement(nameof(SortOrder))]
        public int? SortOrder { get; set; }

        [XmlElement(nameof(IsMandatory))]
        public bool IsMandatory { get; set; }

        [XmlElement(nameof(IsValueDerived))]
        public bool IsValueDerived { get; set; }

        [XmlElement(nameof(IsHidden))]
        public bool IsHidden { get; set; }

        [XmlElement(nameof(ShowConditions))]
        public object ShowConditions { get; set; }

        [XmlElement(nameof(InputMask))]
        public string InputMask { get; set; }

        [XmlElement(nameof(ShowFormat))]
        public string ShowFormat { get; set; }

        [XmlElement(nameof(NullValueReplacement))]
        public object NullValueReplacement { get; set; }
    }
}
