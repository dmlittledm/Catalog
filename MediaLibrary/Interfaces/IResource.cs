using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MediaLibrary.Interfaces
{
    /// <summary> Ресурс 
    /// </summary>
    public interface IResource: INotifyPropertyChanged
    {
        /// <summary> Id 
        /// </summary>
        Guid Id { get; }

        /// <summary> Набор свойств 
        /// </summary>
        IEnumerable<IField> Fields { get; }
    }

    /// <summary> Интерфейс для управления ресурсом 
    /// </summary>
    public interface IResourceController : IResource
    {

        void AddField(IField item);
        void RemoveField(IField item);
        void ClearFields();
        
    }
}
