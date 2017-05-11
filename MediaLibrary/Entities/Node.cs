using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    /// <summary> Node 
    /// </summary>
    public class Node : Resource, INode
    {
        public bool HasChilds => Childs.Any();

        public IEnumerable<INode> Childs { get; protected set; }

        public INode Parent { get; protected set; }
        public INode Root => Parent?.Root ?? this;

        // TODO: look at XDocument's Descendants realization - m.b. better to apply filters instead of just take all the data
        public IEnumerable<INode> Descendants(Guid? id = null)
        {
            return GetDescendants(false, id);
            // NOTE: here is no protection from cycle loop
            //(new[] {this}).Union(
            //    Childs?.Where(w => ReferenceEquals(w.Parent, this)) // protection against usage from multi-parents
            //        .SelectMany(s => s.Descendants)
            //    ?? new List<INode>());

            //XDocument doc;
            //doc.Descendants().o
        }

        public IEnumerable<INode> DescendantsAndSelf(Guid? id = null)
        {
            return GetDescendants(true, id);
        }

        internal IEnumerable<INode> GetDescendants(bool self, Guid? id = null)
        {
            INode n = this;
            if (self)
            {
                if(id == null || n.Id == id.Value) yield return n;
            }
            while (true)
            {
                foreach (var node in Childs)
                {
                    yield return node;
                }
            }
        }
    }
}
