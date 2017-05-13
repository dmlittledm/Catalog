using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Infrastructure;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    /// <summary> Dicrectory 
    /// </summary>
    public class Directory: IDirectory
    {
        protected IList<IResource> ItemsInternal { get; set; }

        protected IList<IFieldType> FieldTypesInternal { get; }


        public IEnumerable<IResource> Items => ItemsInternal;

        public IEnumerable<IFieldType> FieldTypes => FieldTypesInternal;


        public Directory()
        {
            ItemsInternal = new List<IResource>();
            FieldTypesInternal = new List<IFieldType>();
        }

        public void Add(IResource resource)
        {
            if(resource == null)
                throw new ArgumentNullException(nameof(resource));

            if (Items.Any(x => x.Id == resource.Id))
                throw new ArgumentException(
                    string.Format(Messages.Directory.AlreadyContainsResourceWithIdXxx, resource.Id));

            ItemsInternal.Add(resource);
        }

        public void Remove(Guid id)
        {
            var resource = ItemsInternal.FirstOrDefault(x => x.Id == id);
            if (resource != null)
                ItemsInternal.Remove(resource);
        }

        public void Remove(IResource resource)
        {
            ItemsInternal.Remove(resource);
        }

        public void AddFieldType(IFieldType fieldType)
        {
            if(FieldTypes.Any(x => x.Name == fieldType.Name))
                throw new ArgumentException(string.Format(Messages.Directory.AlreadyContainsFieldTypeWithNameXxx, fieldType.Name));

            FieldTypesInternal.Add(fieldType);
        }

        public void RemoveFieldType(IFieldType fieldType)
        {
            FieldTypesInternal.Remove(fieldType);
        }
    }
}
