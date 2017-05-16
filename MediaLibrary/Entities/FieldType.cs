using System;
using System.Collections.Generic;
using MediaLibrary.Interfaces;
using System.Xml.Serialization;

namespace MediaLibrary.Entities
{
    // WARNING: do not change field names after release.
    // It may cause incompatibility with old version of saved data
    // Or use hardcoded field names instead of nameof(...) before change.

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

        public Type GetDataType()
        {
            switch (FieldDataType)
            {
                case FieldDataTypes.Text:
                case FieldDataTypes.Hyperlink:
                case FieldDataTypes.Path:
                case FieldDataTypes.Notification:
                    return typeof(string);
                case FieldDataTypes.Image:
                    return typeof(object);
                case FieldDataTypes.DateTime:
                    return typeof(DateTime);
                case FieldDataTypes.LinkToItem:
                    return typeof(Guid);
                case FieldDataTypes.ItemOf:
                    return typeof(Tuple<Guid, Guid>);
                case FieldDataTypes.SetOfItems:
                    return typeof(IEnumerable<Tuple<Guid, Guid>>);
                case FieldDataTypes.Tags:
                    return typeof(IEnumerable<string>);
                case FieldDataTypes.Decimal:
                    return typeof(decimal);
                default:
                    throw new ArgumentOutOfRangeException(nameof(FieldDataType));
            }
        }
    }
}
