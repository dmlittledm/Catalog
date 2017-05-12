using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
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
        
        public Resource()
        {
            Id = Guid.NewGuid();
            FieldsInternal = new List<IField>();
        }

        public IEnumerable<IField> Fields => FieldsInternal;

        public Expression<Func<IResource, bool>> ResourseIdIs(Guid id)
        {
            return x => x.Id == Id;
        }

        protected IList<IField> FieldsInternal { get; set; }

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
