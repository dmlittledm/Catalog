using System.ComponentModel;

namespace MediaLibrary.Interfaces
{
    /// <summary> поле с данными
    /// </summary>
    public interface IField: INotifyPropertyChanged
    {
        /// <summary> Тип поля 
        /// </summary>
        IFieldType FieldType { get; set; }
        /// <summary> значение поля 
        /// </summary>
        object Value { get; set; }
    }

    /// <summary> контроллер поля  
    /// </summary>
    public interface IFieldController: IField
    {
        /// <summary> изменение значений
        /// </summary>
        /// <param name="name">название поля</param>
        /// <param name="value">значение</param>
        void Update(string name, object value);
        
        /// <summary> изменение значений 
        /// </summary>
        /// <param name="value">значение</param>
        void Update(object value);

        bool IsDirty { get; }
    }
}
