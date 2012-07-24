using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Neo4jClient;

namespace BL.Relationships
{
    public class HasWorkedRelationship :
        Relationship, 
        IRelationshipAllowingSourceNode<PO.Worker>, 
        IRelationshipAllowingTargetNode<PO.Chunk>
    {
        public HasWorkedRelationship(NodeReference otherNode)
            : base(otherNode)
        { }

        public override string RelationshipTypeKey
        {
            get { return "HAS_WORKED"; }
        }
    }
}
