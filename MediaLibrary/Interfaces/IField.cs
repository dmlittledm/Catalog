using System.ComponentModel;

namespace MediaLibrary.Interfaces
{
    /// <summary> поле с данными
    /// </summary>
    public interface IField: INotifyPropertyChanged
    {
        /// <summary> Тип поля 
        /// </summary>
        IFieldType FieldType { get; }
        
        /// <summary> Название поля 
        /// </summary>
        string Name { get; }

        /// <summary> значение поля 
        /// </summary>
        object Value { get; }

        bool IsDirty { get; }

        /// <summary> изменение значения 
        /// </summary>
        /// <param name="value">значение</param>
        void Update(object value);
    }

    public interface IField<T> : IField
    {
        new T Value { get; }

        void Update(T value);
    }
}
