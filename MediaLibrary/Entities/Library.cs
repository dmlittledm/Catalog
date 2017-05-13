using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Infrastructure;
using MediaLibrary.Interfaces;

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

        public void RemoveNode(Guid id)
        {
            // TODO: apply Descendants method in Library to.
            var node = Nodes.Select(x => x.DescendantsAndSelf().FirstOrDefault()).FirstOrDefault();

            // TODO: remove links to this resource and its descendants
            if (node != null)
                NodesInternal.Remove(node);
        }

        public void RemoveNode(INode node)
        {
            if(node == null)
                return;

            if (Nodes.Contains(node))
            {
                // TODO: remove links to this resource and its descendants
                NodesInternal.Remove(node);
                return;
            }

            var parent = NodesInternal.FirstOrDefault(x => x.Id == node.Id)?.Parent;

            // TODO: remove links to this resource and its descendants
            if (parent != null)
                parent.RemoveChild(parent);
        }

        public void MoveTo(INode source, INode target)
        {
            throw new NotImplementedException();
        }

        private void RemoveLinks(INode node)
        {
            var ids = node.DescendantsAndSelf().Select(x => x.Id);

            var linkedNodes = NodesInternal.Where(x => x.Id != node.Id)
                .SelectMany(x =>
                    x.DescendantsAndSelf()
                        .Where(n => n.Id != node.Id
                                    && n.Fields.Any(
                                        field => field.FieldType.FieldDataType == FieldDataTypes.LinkToItem
                                                 && (Guid) field.Value == Id)));

            foreach (var link in linkedNodes)
            {
                
            }
        }
    }
}
