using System;
using System.Collections.Generic;
using System.ComponentModel;
using MediaLibrary.Annotations;

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

        bool HasParent { get; }

        /// <summary> parent node 
        /// </summary>
        INode Parent { get; }

        /// <summary> returns root node 
        /// </summary>
        INode Root { get; }

        /// <summary> all descendant nodes 
        /// </summary>
        IEnumerable<INode> Descendants(Func<INode, bool> predicate = null);

        /// <summary> all descendant nodes inculuding this self 
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns></returns>
        IEnumerable<INode> DescendantsAndSelf(Func<INode, bool> predicate = null);

        /// <summary> Set another parent node 
        /// </summary>
        /// <param name="parent"></param>
        void SetParent([NotNull] INode parent);

        /// <summary> Set parent to null 
        /// </summary>
        void ClearParent();

        /// <summary> Add child node 
        /// </summary>
        /// <param name="node"></param>
        void AddChild([NotNull] INode node);

        /// <summary> Remove child node 
        /// </summary>
        /// <param name="id">id удаляемого узла</param>
        void RemoveChild(Guid id);

        /// <summary> Remove child node 
        /// </summary>
        /// <param name="node">удаляемый узел</param>
        void RemoveChild(INode node);
    }
}
