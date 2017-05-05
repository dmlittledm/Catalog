using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MediaLibrary.Interfaces
{
    public interface INode: INotifyPropertyChanged
    {
        /// <summary> Id 
        /// </summary>
        Guid Id { get; }

        // moved to Fields
        ///// <summary> Name 
        ///// </summary>
        //string Name { get; }
        ///// <summary> Description 
        ///// </summary>
        //string Description { get; }

        IEnumerable<IField> Fields { get; }

        IEnumerable<INode> Childs { get; }

        bool HasChilds { get; }

        /// <summary> parent node 
        /// </summary>
        INode Parent { get; }

        /// <summary> returns root node 
        /// </summary>
        INode Root { get; }

        // moved to Fields
        ///// <summary> Links to other resources 
        ///// </summary>
        //IEnumerable<ILink> Links { get; }

        /// <summary> all descendand nodes 
        /// </summary>
        IEnumerable<INode> Descendants(Guid? id = null);

        IEnumerable<INode> DescendantsAndSelf(Guid? id = null);
    }

    public interface INodeController : INode
    {

        void AddField(IField item);
        void RemoveField(IField item);
        void ClearFields();
        
    }
}
