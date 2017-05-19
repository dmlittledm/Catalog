using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Infrastructure;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    /// <summary> Node 
    /// </summary>
    public class Node : Resource, INode
    {
        public bool HasChilds => Childs.Any();

        public bool HasParent => Parent != null;

        public IEnumerable<INode> Childs => ChildsInternal;

        protected IList<INode> ChildsInternal { get; set; } = new List<INode>();

        public INode Parent { get; protected set; }

        public INode Root => Parent?.Root ?? this;

        public string Name => Fields.FirstOrDefault(x => x.FieldType.Role == FieldRoles.Name)?.Value.ToString();

        public IEnumerable<INode> Descendants(Func<INode, bool> predicate = null)
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

        public IEnumerable<INode> DescendantsAndSelf(Func<INode, bool> predicate = null)
        {
            return GetDescendants(true, predicate);
        }

        public void SetParent(INode parent)
        {
            if(parent == null)
                throw new ArgumentNullException(nameof(parent));

            if (!parent.Childs.Contains(this))
                throw new InvalidOperationException(Messages.Node.CantSetParent + " " + Messages.Node.ParentDoesntContainsThisNode);

            Parent = parent;
        }

        public void ClearParent()
        {
            if(Parent?.Childs.Contains(this) ?? false)
                throw new InvalidOperationException(Messages.Node.CantClearParent + " " + Messages.Node.ParentContainsThisNode);

            Parent = null;
        }

        public void AddChild(INode node)
        {
            if (node == null)
                throw new ArgumentNullException(nameof(node));

            if(Childs.Any(x => x.Id == node.Id))
                throw new ArgumentException(string.Format(Messages.Node.AlreadyContainsNodeWithIdXxx, node.Id));

            ChildsInternal.Add(node);
            node.SetParent(this);
        }

        public void RemoveChild(Guid id)
        {
            var node = Childs.FirstOrDefault(x => x.Id == id);
            if (node == null)
                return;

            RemoveChild(node);
        }

        public void RemoveChild(INode node)
        {
            if (node == null)
                return;

            ChildsInternal.Remove(node);
            node.SetParent(null);
        }

        internal IEnumerable<INode> GetDescendants(bool self, Func<INode, bool> predicate = null)
        {
            INode n = this;
            if (self)
            {
                if(predicate == null || predicate(this)) yield return n; // TODO: check if this works right and gives other descendants
                // m.b. just put this at the end
            }

            foreach (var node in Childs)
            {
                var subChilds = node.DescendantsAndSelf(predicate);
                foreach (var child in subChilds)
                    yield return child;
            }
        }
    }
}
