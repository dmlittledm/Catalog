using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Xml.Linq;
using MediaLibrary.Annotations;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Entities
{
    /// <summary> Node 
    /// </summary>
    public class Node : INode
    {

        public Guid Id { get; }
        
        //public Image Image;

        //public string Path;

        protected Node()
        {
            Id = Guid.NewGuid();
        }

        //public Node(string name, string description)
        //    : this()
        //{
        //    Name = name;
        //    Description = description;
        //}

        public bool HasChilds => Childs.Any();
        //public bool HasLinks => Links.Any();

        //public string Name { get; protected set; }
        //public string Description { get; protected set; }
        public IEnumerable<IField> Fields { get; protected set; } = new List<IField>();
        public IEnumerable<INode> Childs { get; protected set; }

        public INode Parent { get; protected set; }
        public INode Root => Parent?.Root ?? this;

        //public IEnumerable<ILink> Links { get; protected set; }

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
                if(n != null)



                foreach (var node in Childs)
                {
                    yield return node;
                }
            }

        }

        #region PropertyChanged

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        } 

        #endregion
    }
}
