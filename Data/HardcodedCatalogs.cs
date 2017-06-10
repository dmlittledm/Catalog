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
            new FieldType("Name", FieldDataTypes.Text)
            {
                Role = FieldRoles.Name,
                IsMandatory = true,
                SortOrder = 0,
                IsValueDerived = true
            },
            new FieldType("Path", FieldDataTypes.Path)
            {
                Role = FieldRoles.Path,
                SortOrder = 1,
                IsValueDerived = true
            },
            new FieldType("Image", FieldDataTypes.Image)
            {
                Role = FieldRoles.Logo,
                SortOrder = 2,
                IsValueDerived = true
            }
        };
    }
}
