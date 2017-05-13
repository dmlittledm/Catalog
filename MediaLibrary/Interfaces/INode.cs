using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace MediaLibrary.Interfaces
{
    /// <summary> Раздел 
    /// </summary>
    public interface INode: IResource, INotifyPropertyChanged
    {
        /// <summary> Child nodes 
        /// </summary>
        IEnumerable<INode> Childs { get; }

        bool HasChilds { get; }

        /// <summary> parent node 
        /// </summary>
        INode Parent { get; }

        /// <summary> returns root node 
        /// </summary>
        INode Root { get; }

        /// <summary> all descendant nodes 
        /// </summary>
        IEnumerable<INode> Descendants(Func<IResource, bool> predicate = null);

        /// <summary> all descendant nodes inculuding this self 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<INode> DescendantsAndSelf(Func<IResource, bool> predicate = null);

        /// <summary> Add child node 
        /// </summary>
        /// <param name="node"></param>
        void AddChild(INode node);

        /// <summary> Remove child node 
        /// </summary>
        /// <param name="id"></param>
        void RemoveChild(Guid id);

        /// <summary> Remove child node 
        /// </summary>
        /// <param name="node"></param>
        void RemoveChild(INode node);
    }
}
