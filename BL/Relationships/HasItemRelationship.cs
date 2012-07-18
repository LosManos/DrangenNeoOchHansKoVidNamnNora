using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;

namespace BL.Relationships
{
    public class HasItemRelationship :
        Relationship,
        IRelationshipAllowingSourceNode<PO.Workers>,
        IRelationshipAllowingTargetNode<PO.Worker>
    {
        public HasItemRelationship(NodeReference otherNode)
            : base(otherNode)
        { }

        public override string RelationshipTypeKey
        {
            get { return "HAS_ITEM"; }
        }
    }
}
