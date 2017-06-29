using System;
using System.Collections.Generic;
using System.Linq;

namespace MediaLibrary.Interfaces
{
    /// <summary> Коллекция 
    /// </summary>
    public interface ILibrary
    {
        /// <summary> Id 
        /// </summary>
        Guid Id { get; }

        /// <summary> library name 
        /// </summary>
        string Name { get; }

        /// <summary> library description 
        /// </summary>
        string Description { get; }

        /// <summary> create date & time 
        /// </summary>
        DateTime CreateDate { get; }

        /// <summary> last update date & time 
        /// </summary>
        DateTime UpdateDate { get; }

        /// <summary> collection of library items 
        /// </summary>
        IEnumerable<INode> Nodes { get; }

        /// <summary> update library name and description 
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="description">Description</param>
        void Update(string name, string description);

        /// <summary> add new item to library 
        /// </summary>
        /// <param name="node"></param>
        void AddNode(INode node);

        /// <summary> remove item by id 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="removeLinks">удалять ли ссылки на этот узел и его наследников</param>
        void RemoveNode(Guid id, bool removeLinks = true);

        /// <summary> remove item 
        /// </summary>
        /// <param name="node"></param>
        /// <param name="removeLinks">удалять ли ссылки на этот узел и его наследников</param>
        void RemoveNode(INode node, bool removeLinks = true);

        /// <summary> Moves current node to another parent 
        /// </summary>
        /// <param name="target">node that became parent for source node</param>
        /// <param name="source">node to be moved</param>
        void MoveTo(INode target, INode source);

        /// <summary>Получить список узлов-наследников 
        /// </summary>
        /// <param name="predicate">фильтр</param>
        /// <returns></returns>
        IEnumerable<INode> Descendants(Func<INode, bool> predicate = null);
    }
}