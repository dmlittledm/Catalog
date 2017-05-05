using MediaLibrary.Annotations;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    // TODO: зачем этот класс? Недостаточно FieldBase?
    public class Field<T> : FieldBase 
    {
        public Field([NotNull] IFieldType fieldType, T value)
        {
            // ? задействовать IoC ?

            FieldType = fieldType;
            Value = value;
        }
    }
}
