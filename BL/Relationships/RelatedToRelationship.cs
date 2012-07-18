using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;

namespace BL.Relationships
{
    public class RelatedToRelationship :
        Relationship,
        IRelationshipAllowingSourceNode<PO.Root>,
        IRelationshipAllowingTargetNode<PO.ListRoot<PO.Worker>>
    {
        public RelatedToRelationship(NodeReference otherNode)
            : base(otherNode)
        { }

        public override string RelationshipTypeKey
        {
            get { return "RELATED_TO"; }
        }
    }
}
