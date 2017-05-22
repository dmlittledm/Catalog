using System;
using System.ComponentModel;
using System.Linq.Expressions;

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

        /// <summary> predicate to filter field by name 
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        /// <remarks>Checks if <see cref="FieldType"/> is <see cref="FieldRoles.Name"/> and compares <paramref name="name"/> with <see cref="Value"/></remarks>
        Expression<Func<IField, bool>> NameIs(string name);

        ///// <summary> predicate to filter field by name 
        ///// </summary>
        ///// <param name="predicate"></param>
        ///// <returns></returns>
        ///// <remarks>Checks if <see cref="FieldType"/> is <see cref="FieldRoles.Name"/> and uses <paramref name="predicate"/> to check <see cref="Value"/></remarks>
        //Func<IField, bool> NameIs(Expression<Func<string, bool>> predicate);

        Expression<Func<IField, bool>> FieldRoleIs(FieldRoles role);
    }
}
