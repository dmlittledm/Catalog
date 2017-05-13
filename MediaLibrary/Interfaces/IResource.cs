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

        void AddField([NotNull] IField item);

        void RemoveField(IField item);

        void ClearFields();
    }
}
