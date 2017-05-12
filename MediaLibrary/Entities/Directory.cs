using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    }
}
