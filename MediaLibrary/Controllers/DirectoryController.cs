using System;
using System.Linq;
using MediaLibrary.Entities;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Controllers
{
    public class DirectoryController: Directory, IDirectoryController
    {
        public void Add(IResource resource)
        {
            _items.Add(resource);
        }

        public void Remove(Guid id)
        {
            var resource = _items.FirstOrDefault(x => x.Id == id);
            if (resource != null)
                _items.Remove(resource);
        }

        public void Remove(IResource resource)
        {
            _items.Remove(resource);
        }

        public void AddFieldType(IFieldType fieldType)
        {
            _fieldTypes.Add(fieldType);
        }

        public void RemoveFieldType(IFieldType fieldType)
        {
            _fieldTypes.Remove(fieldType);
        }
    }
}
