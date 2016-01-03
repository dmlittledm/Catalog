using System.ComponentModel;
using Catalog;

namespace MediaLibrary.Interfaces
{
    /// <summary> поле с данными
    /// </summary>
    public interface IField: INotifyPropertyChanged
    {
        string Name { get; }

        object Value { get; }

        int? SortOrder { get; }

        FieldTypes FieldType { get; }
    }

    /// <summary> контроллер поля  
    /// </summary>
    public interface IFieldController: IField
    {
        /// <summary> изменение значений
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="sortOrder">порядок отображения (null - оставить без изменений)</param>
        void Update(string name, object value, int? sortOrder = null);

        bool IsDirty { get; }

        void SetSortOrder(int? sortOrder);
    }
}
