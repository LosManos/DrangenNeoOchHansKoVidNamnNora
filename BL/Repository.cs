using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace BL
{
    public class Repository : ObjectBase
    {
        private const string NameIndexName = "NameIndex";

        public void Setup()
        {
            Connect();

            RootNode = GetRootNode() ?? AddRootNode();

            var workersNode = GetWorkersRootNode() ?? AddWorkersRootNode();

        }

        private Node<PO.Root> AddRootNode()
        {
            var nodeRef = Client.Create(PO.Root.Create(), null,
                new[]{
                    new IndexEntry{
                        Name=NameIndexName, 
                        KeyValues=new[]{
                            new KeyValuePair<string,object>("Name", "Root")
                        }
                    }
                });

            var ret = Client.Get<PO.Root>(nodeRef.Id);

            Client.Update<PO.Root>(ret.Reference, node => { node.SetID(ret.Reference.Id); });

            ret = Client.Get<PO.Root>(nodeRef.Id);

            return ret;
        }

        private Node<PO.Workers> AddWorkersRootNode()
        {
            var nodeRef = Client.Create(PO.Workers.Create("Workers"), null,
                new[]{
                    new IndexEntry{
                        Name=NameIndexName, 
                        KeyValues=new[]{
                            new KeyValuePair<string,object>("Name", "Workers")
                        }
                    }
                });

            Client.CreateRelationship<PO.Root, Relationships.RelatedToRelationship>(RootNode.Reference, new Relationships.RelatedToRelationship(nodeRef));

            var ret = Client.Get<PO.Workers>(nodeRef.Id);

            Client.Update<PO.Workers>(ret.Reference, node => { node.SetID(ret.Reference.Id); });

            ret = Client.Get<PO.Workers>(nodeRef.Id);

            Debug.Assert(ret.Reference.Id == ret.Data.ID);
            Debug.Assert(ret.Reference.Id == nodeRef.Id);
            Debug.Assert(ret.Reference.Id == GetWorkersRootNode().Reference.Id);

            return ret;
        }

    }
}
