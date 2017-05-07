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
        IEnumerable<INode> Descendants(Guid? id = null);

        /// <summary> all descendant nodes inculuding this self 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        IEnumerable<INode> DescendantsAndSelf(Guid? id = null);
    }

    /// <summary> Интерфейс управления разделом 
    /// </summary>
    public interface INodeController : INode, IResourceController
    {
        /// <summary> Add child node 
        /// </summary>
        /// <param name="node"></param>
        void AddChild(INode node);

        /// <summary> Remove child node 
        /// </summary>
        /// <param name="id"></param>
        void RemoveChild(Guid id);
    }
}
