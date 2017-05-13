using System.Collections.Generic;
using MediaLibrary;
using MediaLibrary.Entities;
using MediaLibrary.Interfaces;

namespace Data
{
    public class HardcodedCatalogs
    {
        public static IEnumerable<IFieldType> EnglishCourses = new List<IFieldType>()
        {
            new FieldType
            {
                Name = "Name",
                Role = FieldRoles.Name,
                FieldDataType = FieldDataTypes.Text,
                IsMandatory = true,
                SortOrder = 0,
                IsValueDerived = true,
            },
            new FieldType()
            {
                Name = "Path",
                Role = FieldRoles.Path,
                FieldDataType = FieldDataTypes.Path,
                SortOrder = 1,
                IsValueDerived = true,
            },
            new FieldType()
            {
                Name = "Image",
                Role = FieldRoles.Logo,
                FieldDataType = FieldDataTypes.Image,
                SortOrder = 2,
                IsValueDerived = true,
            }


        };
    }
}
