using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Infrastructure;
using MediaLibrary.Interfaces;
using static MediaLibrary.Infrastructure.Tools;

namespace MediaLibrary.Entities
{
    public class Library: ILibrary
    {
        protected IList<INode> NodesInternal { get; set; }

        public Guid Id { get; }

        public string Name { get; protected set; }

        public string Description { get; protected set; }

        public DateTime CreateDate { get; protected set; }

        public DateTime UpdateDate { get; protected set; }

        public IEnumerable<INode> Nodes => NodesInternal;


        public Library(string name, string description)
        {
            Id = Guid.NewGuid();
            NodesInternal = new List<INode>();
            CreateDate = DateTime.Now;
            UpdateDate = CreateDate;

            Name = name;
            Description = description;
        }

        public void Update(string name, string description)
        {
            Name = name;
            Description = description;
            UpdateDate = DateTime.Now;
        }

        public void AddNode(INode node)
        {
            if(NodesInternal.Any(x => x.Id == node.Id))
                throw new ArgumentException(string.Format(Messages.Library.AlreadyContainsNodeWithIdXxx, node.Id));

            NodesInternal.Add(node);
        }

        public void RemoveNode(Guid id, bool removeLinks = true)
        {
            var node = Descendants(x => x.Id == id).FirstOrDefault();

            if (node != null)
                NodesInternal.Remove(node);
        }

        public void RemoveNode(INode node, bool removeLinks = true)
        {
            if(node == null)
                return;

            if (Nodes.Contains(node))
                NodesInternal.Remove(node);
            else
                node.Parent?.RemoveChild(node);

            if (removeLinks)
                RemoveLinks(Nodes, node, true);
        }

        public void MoveTo(INode source, INode target)
        {
            if (source == null || target == null)
                throw new ArgumentNullException(source == null ? nameof(source) : nameof(target));

            RemoveNode(source, false);

            target.AddChild(source);
        }

        public IEnumerable<INode> Descendants(Func<INode, bool> predicate)
        {
            return Nodes.SelectMany(s => s.DescendantsAndSelf(predicate));
        }
    }
}
