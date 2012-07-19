using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace BL
{
    public abstract class ObjectBase
    {
        private GraphClient _client;
        private Node<PO.Root> _rootNode;
        private const string RootNodeQuery = "start a=node:NameIndex(Name='Root') return a;";

        protected GraphClient Client
        {
            get { return _client; }
        }

        protected Node<PO.Root> RootNode
        {
            get
            {
                _rootNode = _rootNode ?? GetRootNode(_client);
                return _rootNode;
            }
            set
            {
                _rootNode = value;
            }
        }

        protected void Connect()
        {
            if (null == _client)
            {
                //var client = new GraphClient(new Uri("http://nidhugg.mydomain.local:7474/db/data"));
                _client = new GraphClient(new Uri("http://nidhugg:7474/db/data"));
                _client.Connect();
            }
        }

        protected static Node<PO.Root> GetRootNode(GraphClient client)
        {
            var node = client.ExecuteGetCypherResults<Node<PO.Root>>(
                new CypherQuery(RootNodeQuery
                , null, CypherResultMode.Set))
            .SingleOrDefault();
            return node;
        }

        protected static Node<PO.Workers> GetWorkersRootNode(GraphClient client, Node<PO.Root> rootNode)
        {
            //var rootNode = GetRootNode(client);
            //var query = new CypherQuery("start R=node({p0}) match R-[:RELATED_TO]->N where N.__Type=\"Workers\" return N;", new Dictionary<string, object>() { { "p0", rootNode.Reference.Id } }, CypherResultMode.Set); 

            var query = new CypherQuery("start R=node({p0}) match R-[:RELATED_TO]->WR where WR.Name='Workers' return WR;", new Dictionary<string, object>() { { "p0", rootNode.Reference.Id } }, CypherResultMode.Set);
            var node = client.ExecuteGetCypherResults<Node<PO.Workers>>(
                query
                );
            return node.SingleOrDefault();
        }

    }
}
