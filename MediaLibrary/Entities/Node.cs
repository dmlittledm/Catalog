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
        public IEnumerable<INode> Descendants(Func<IResource, bool> predicate = null)
        {
            return GetDescendants(false, predicate);
            // NOTE: here is no protection from cycle loop
            //(new[] {this}).Union(
            //    Childs?.Where(w => ReferenceEquals(w.Parent, this)) // protection against usage from multi-parents
            //        .SelectMany(s => s.Descendants)
            //    ?? new List<INode>());

            //XDocument doc;
            //doc.Descendants().o
        }

        public IEnumerable<INode> DescendantsAndSelf(Func<IResource, bool> predicate = null)
        {
            return GetDescendants(true, predicate);
        }

        public void AddChild(INode node)
        {
            throw new NotImplementedException();
        }

        public void RemoveChild(Guid id)
        {
            throw new NotImplementedException();
        }

        public void RemoveChild(INode node)
        {
            throw new NotImplementedException();
        }

        internal IEnumerable<INode> GetDescendants(bool self, Func<IResource, bool> predicate = null)
        {
            INode n = this;
            if (self)
            {
                if(predicate == null || predicate(this)) yield return n; // TODO: check if this works right and gives other descendants
                // m.b. just put this at the end
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
