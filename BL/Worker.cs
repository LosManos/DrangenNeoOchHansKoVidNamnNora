using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Neo4jClient;
using Neo4jClient.Cypher;

namespace BL
{
    public class Worker : ObjectBase
    {
        public DTO.Worker AddWorker(string name)
        {
            Connect();
            
            var worker = AddWorker(name, Client, GetWorkersRootNode(Client, RootNode));
            return DTO.Worker.Create(worker.Data);
        }

        public void Delete(int id)
        {
            Connect();
            
            Delete(id, Client);
        }

        public DTO.Worker Get(int id)
        {
            Connect();
            
            var worker = Get(id, Client);
            return DTO.Worker.Create(worker.Data);
        }

        public IList<DTO.Worker> GetAll()
        {
            Connect();
            
            var query = new CypherQuery("start R=node({0}) match R-[:RELATED_TO]->N-[:HAS_ITEM]->W where N.Name=\"Workers\" return W;", new Dictionary<string, object>() { { "0", RootNode.Reference.Id } }, CypherResultMode.Set);

            var workers = Client.ExecuteGetCypherResults<Node<PO.Worker>>(
                query)
                .Select(n => DTO.Worker.Create(n.Data));
            return workers.ToList();
        }

        public DTO.Worker Update(DTO.Worker worker)
        {
            Connect();

            var workerNode = Client.Get<PO.Worker>(worker.ID);
            Client.Update<PO.Worker>(workerNode.Reference, node =>
            {
                node.ID = worker.ID;
                node.Name = worker.Name;
            });

            var getWorker = Get(worker.ID, Client);
            return DTO.Worker.Create(getWorker.Data);
        }


        private static Node<PO.Worker> AddWorker(string name, GraphClient Client, Node<PO.Workers>WorkersRootNode)
        {
            var workerNodeRef = Client.Create(
                PO.Worker.Create(name), 
                null,
                new[]
                {
                    new IndexEntry
                    {
                        Name = "my_index",
                        KeyValues = new[]
                        {
                            /* new KeyValuePair<string, object>("key", "value"), new KeyValuePair<string, object>("key2", ""), */ new KeyValuePair<string, object>("key3", "value3")
                        }
                    }
                }
            );

            Client.Update<PO.Worker>(workerNodeRef, node => { node.SetID(workerNodeRef.Id); });

            var workersRootNode = WorkersRootNode;

            var rel = Client.CreateRelationship<PO.Workers, Relationships.HasItemRelationship>(
                workersRootNode.Reference,
                new Relationships.HasItemRelationship(workerNodeRef)
                );

            var workerNode = Get(workerNodeRef.Id, Client);

            return workerNode;
        }

        private static void Delete(int id, GraphClient client)
        {
            var worker = Get(id, client);
            client.Delete(worker.Reference, DeleteMode.NodeAndRelationships);
        }

        internal static Node<PO.Worker> Get(int id, GraphClient client)
        {
            var worker = client.Get<PO.Worker>(id);
            return worker;
        }

        private static Node<PO.Worker> UpdateWorker(DTO.Worker worker, GraphClient client)
        {
            var workerNode = client.Get<PO.Worker>(worker.ID);
            //workerNode.Data.Set(worker.Name);
            client.Update<PO.Worker>(workerNode.Reference, node =>
            {
                node.ID = worker.ID;
                node.Name = worker.Name;
            });

            return Get(worker.ID, client);
        }

    }
}
