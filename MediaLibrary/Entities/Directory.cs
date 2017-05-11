using System.Collections.Generic;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    /// <summary> Dicrectory 
    /// </summary>
    public class Directory: IDirectory
    {
        protected readonly IList<IResource> _items = new List<IResource>();
        protected readonly IList<IFieldType> _fieldTypes = new List<IFieldType>();

        public IEnumerable<IResource> Items
        {
            get { return _items; }
        }

        public IEnumerable<IFieldType> FieldTypes
        {
            get { return _fieldTypes; }
        }
    }
}
