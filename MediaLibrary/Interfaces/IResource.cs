using System;
using System.Collections.Generic;
using System.ComponentModel;
using MediaLibrary.Annotations;

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
        void AddField([NotNull] IField item);

        void RemoveField([NotNull] IField item);

        void ClearFields();
    }
}
