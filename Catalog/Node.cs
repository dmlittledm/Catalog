using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog
{
    /// <summary> Catalogue node 
    /// </summary>
    public class Node
    {
        protected Guid Id { get; set; }
        
        public string Name;
        public string Description;
        public Image Image;

        public string Path;

        public bool HasChilds {get { return Childs.Any(); }}
        public bool HasLinks {get { return Links.Any(); }}

        public List<Node> Childs { get; private set; }
        /// <summary> Links to other resources 
        /// </summary>
        public List<Link> Links { get; private set; }

        /// <summary> parent node 
        /// </summary>
        public Node Parent { get; private set; }



    }

    public class Link
    {
        
    }
}
