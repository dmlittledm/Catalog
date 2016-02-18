using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MediaLibrary.Interfaces
{
    public interface IMediaLibraryController
    {
        IEnumerable<INode> Nodes { get; }

        void AddNode(INode node);

        void UpdateNode(INode node, string name = null, string description = null);

        void RemoveNode(INode node);

        /// <summary> Moves current node to another parent 
        /// </summary>
        /// <param name="target"></param>
        void MoveTo(INodeController target);

    }
}
