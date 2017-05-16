using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Interfaces
{
    /// <summary> Интерфейс типа поля 
    /// </summary>
    public interface IFieldType
    {
        /// <summary> Название поля
        /// </summary>
        string Name { get; set; }
        
        /// <summary> Тип поля 
        /// </summary>
        FieldDataTypes FieldDataType { get; set; }
        
        /// <summary> Значение по умолчанию
        /// </summary>
        object DefaultValue { get; set; }
        
        /// <summary> Роль поля 
        /// </summary>
        FieldRoles Role { get; set; }
        
        /// <summary> Порядок отображения на экране
        /// </summary>
        int? SortOrder { get; set; }
        
        /// <summary> обязательное ли поле
        /// </summary>
        bool IsMandatory { get; set; }
        
        /// <summary> Наследовать ли значение поля от родителя (при создании или когда значение не задано)
        /// </summary>
        bool IsValueDerived { get; set; }
        
        /// <summary> не отображать поле 
        /// </summary>
        bool IsHidden { get; set; }
        
        /// <summary> условия отображения скрытого поля 
        /// </summary>
        object ShowConditions { get; set; }
        
        /// <summary> Маска ввода данных (TODO: определиться с форматом) 
        /// </summary>
        string InputMask { get; set; }
        
        /// <summary> Формат отображения 
        /// </summary>
        string ShowFormat { get; set; }
        
        /// <summary> Что отображать при отсутствии у поля значения  
        /// </summary>
        object NullValueReplacement { get; set; }

        /// <summary> Получить тип данных, соответствующий значению <see cref="FieldDataType"/>
        /// </summary>
        /// <returns></returns>
        Type GetDataType();
    }
}
