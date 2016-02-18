using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Interfaces;

namespace MediaLibrary
{
    public class TestClass
    {
        private IEnumerable<INode> nodes;

        public TestClass()
        {
            nodes.Any(a => a.HasChilds);
            
            Dictionary<Guid, INode> dictionary = new Dictionary<Guid, INode>();

            (new List<INode>()).AsQueryable();


        }

    }
}