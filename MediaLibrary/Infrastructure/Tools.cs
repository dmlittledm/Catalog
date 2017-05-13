using System;
using System.Collections.Generic;
using System.Linq;
using MediaLibrary.Interfaces;

namespace MediaLibrary.Infrastructure
{
    internal static class Tools
    {
        /// <summary> удалить ссылки на узел и его наследников 
        /// </summary>
        /// <param name="sourceNodes">набор узлов, в котором надо удалить ссылки</param>
        /// <param name="node">узел, ссылки на который надо удалить</param>
        /// <param name="proceedDescendants">удалять ли ссылки из наследников набора узлов</param>
        /// <remarks>Если в исходном наборе присутствует узел, ссылки на который надо удалить, он пропускается</remarks>
        public static void RemoveLinks(IEnumerable<INode> sourceNodes, INode node, bool proceedDescendants = false)
        {
            var ids = node.DescendantsAndSelf().Select(x => x.Id).ToList();

            Func<IField, bool> filter =
                field => field.FieldType.FieldDataType == FieldDataTypes.LinkToItem
                         && ids.Contains((Guid) field.Value);

            IEnumerable<INode> linkedNodes;

            if (proceedDescendants)
                linkedNodes = sourceNodes.Where(x => x.Id != node.Id)
                    .SelectMany(x => x.DescendantsAndSelf(n => n.Fields.Any(filter)));
            else
                linkedNodes = sourceNodes.Where(x => x.Id != node.Id && x.Fields.Any(filter));

            foreach (var linkedNode in linkedNodes)
                linkedNode.RemoveField(linkedNode.Fields.FirstOrDefault(filter));
        }
    }
}
