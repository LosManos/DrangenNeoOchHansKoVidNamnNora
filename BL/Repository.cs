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

            RootNode = GetRootNode(Client) ?? AddRootNode(Client);

            var workersNode = GetWorkersRootNode(Client,RootNode) ?? AddWorkersRootNode(Client, RootNode);

        }

        private static Node<PO.Root> AddRootNode( GraphClient client)
        {
            var nodeRef = client.Create(PO.Root.Create(), null,
                new[]{
                    new IndexEntry{
                        Name=NameIndexName, 
                        KeyValues=new[]{
                            new KeyValuePair<string,object>("Name", "Root")
                        }
                    }
                });

            var ret = client.Get<PO.Root>(nodeRef.Id);

            client.Update<PO.Root>(ret.Reference, node => { node.SetID(ret.Reference.Id); });

            ret = client.Get<PO.Root>(nodeRef.Id);

            return ret;
        }

        private static Node<PO.Workers> AddWorkersRootNode(GraphClient client, Node<PO.Root> rootNode)
        {
            var nodeRef = client.Create(PO.Workers.Create("Workers"), null,
                new[]{
                    new IndexEntry{
                        Name=NameIndexName, 
                        KeyValues=new[]{
                            new KeyValuePair<string,object>("Name", "Workers")
                        }
                    }
                });

            client.CreateRelationship<PO.Root, Relationships.RelatedToRelationship>(rootNode.Reference, new Relationships.RelatedToRelationship(nodeRef));

            var ret = client.Get<PO.Workers>(nodeRef.Id);

            client.Update<PO.Workers>(ret.Reference, node => { node.SetID(ret.Reference.Id); });

            ret = client.Get<PO.Workers>(nodeRef.Id);

            Debug.Assert(ret.Reference.Id == ret.Data.ID);
            Debug.Assert(ret.Reference.Id == nodeRef.Id);
            Debug.Assert(ret.Reference.Id == GetWorkersRootNode(client, rootNode).Reference.Id);

            return ret;
        }

    }
}
