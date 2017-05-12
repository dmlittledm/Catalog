using System;
using System.Linq;
using MediaLibrary.Entities;
using MediaLibrary.Infrastructure;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Controllers
{
    public class ResourceController: Resource, IResourceController
    {
        public void AddField(IField item)
        {
            if(item == null)
                throw new ArgumentNullException(nameof(item));

            if(FieldsInternal.Any(x => x.FieldType.Name == item.FieldType.Name))
                throw new ArgumentException(string.Format(Messages.Resource.AlreadyContainsFieldWithNameXxx, item.FieldType.Name));

            FieldsInternal.Add(item);
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
