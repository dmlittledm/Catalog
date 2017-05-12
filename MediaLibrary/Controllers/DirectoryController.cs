using System;
using System.Linq;
using MediaLibrary.Entities;
using MediaLibrary.Infrastructure;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Controllers
{
    public class DirectoryController: Directory, IDirectoryController
    {
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
