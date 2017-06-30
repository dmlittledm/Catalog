using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using MediaLibrary.Annotations;
using MediaLibrary.Infrastructure;
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

        protected IList<IField> FieldsInternal { get; set; }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion

        public void AddField(IField item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            if(Fields.Any(x => x.FieldType.Name == item.FieldType.Name))
                throw new ArgumentException(string.Format(Messages.Resource.AlreadyContainsFieldWithNameXxx, item.FieldType.Name));

            FieldsInternal.Add(item);
        }

        public void AddFields(IEnumerable<IField> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (Fields.Any(x => items.Select(s => s.Name).Contains(x.FieldType.Name)))
                throw new ArgumentException(Messages.Resource.AlreadyContainsFieldWithSameName);

            foreach (var field in items)
                FieldsInternal.Add(field);
        }

        public void RemoveField(IField item)
        {
            FieldsInternal.Remove(item);
        }

        public void ClearFields()
        {
            FieldsInternal.Clear();
        }
    }
}
