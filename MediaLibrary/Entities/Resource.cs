using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using MediaLibrary.Annotations;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    /// <summary> Resource 
    /// </summary>
    public class Resource : IResource
    {

        public Guid Id { get; }
        
        protected Resource()
        {
            Id = Guid.NewGuid();
        }

        public IEnumerable<IField> Fields { get; protected set; } = new List<IField>();

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion
    }
}
