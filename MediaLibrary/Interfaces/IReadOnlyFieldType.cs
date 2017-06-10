using System;

namespace MediaLibrary.Interfaces
{
    /// <summary> Интерфейс типа поля 
    /// </summary>
    public interface IReadOnlyFieldType
    {
        /// <summary> Id типа поля 
        /// </summary>
        Guid? Id { get; }

        /// <summary> Название поля
        /// </summary>
        string Name { get; }
        
        /// <summary> Тип поля 
        /// </summary>
        FieldDataTypes FieldDataType { get; }
        
        /// <summary> Значение по умолчанию
        /// </summary>
        object DefaultValue { get; }
        
        /// <summary> Роль поля 
        /// </summary>
        FieldRoles Role { get; }
        
        /// <summary> Порядок отображения на экране
        /// </summary>
        int? SortOrder { get; }
        
        /// <summary> обязательное ли поле
        /// </summary>
        bool IsMandatory { get; }
        
        /// <summary> Наследовать ли значение поля от родителя (при создании или когда значение не задано)
        /// </summary>
        bool IsValueDerived { get; }
        
        /// <summary> не отображать поле 
        /// </summary>
        bool IsHidden { get; }
        
        /// <summary> условия отображения скрытого поля 
        /// </summary>
        object ShowConditions { get; }
        
        /// <summary> Маска ввода данных (TODO: определиться с форматом) 
        /// </summary>
        string InputMask { get; }
        
        /// <summary> Формат отображения 
        /// </summary>
        string ShowFormat { get; }
        
        /// <summary> Что отображать при отсутствии у поля значения  
        /// </summary>
        object NullValueReplacement { get; }

        /// <summary> Получить тип данных, соответствующий значению <see cref="FieldDataType"/>
        /// </summary>
        /// <returns></returns>
        Type GetDataType();
    }
}
