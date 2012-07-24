using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using Neo4jClient;

namespace BL
{
    public class Chunk:ObjectBase
    {
        public DTO.Chunk Add(DateTimeOffset start, DateTimeOffset stop, TimeSpan duration, int owningWorkerID)
        {
            Debug.Assert(0 != owningWorkerID);

            Connect();

            var worker = Worker.Get(owningWorkerID, Client);
            Debug.Assert(null != worker);

            var chunk = Add(start, stop, duration,  worker, Client, RootNode);
            Debug.Assert(null != chunk);
            Debug.Assert(chunk.Data.ID >= 1);

            return DTO.Chunk.Create(chunk.Data);
        }

        public void Delete(DTO.Chunk chunk)
        {
            Debug.Assert(null != chunk);
            Debug.Assert(0 != chunk.ID);

            Connect();

            Delete(chunk, Client);
        }

        public DTO.Chunk Get(int id)
        {
            Debug.Assert(id >= 1);
            Connect();
            return DTO.Chunk.Create(Get(id, Client).Data);
        }

        public DTO.Chunk Update(DTO.Chunk chunk)
        {
            Debug.Assert(null != chunk);
            Debug.Assert(chunk.ID >= 1);

            Connect();

            var updatedChunk = Update(chunk, Client);

            Debug.Assert(chunk.ID == updatedChunk.Reference.Id);
            Debug.Assert(chunk.ID == updatedChunk.Data.ID);

            return DTO.Chunk.Create(updatedChunk.Data);
        }

        private Node<PO.Chunk> Add(DateTimeOffset start, DateTimeOffset stop, TimeSpan duration, Node<PO.Worker> owningWorkerNode, Neo4jClient.GraphClient Client, Neo4jClient.Node<PO.Root> RootNode)
        {
            var chunkNodeRef = Client.Create(
                PO.Chunk.Create(start, stop, duration),
                null,
                new[]
                {
                    new IndexEntry
                    {
                        Name = "NameIndex", 
                        KeyValues = new[]
                        {
                            new KeyValuePair<string,object>("Name", "Chunk")
                        }
                    }   
                }
            );

            //  I update all fields again because I have a problem with serialising/deserialising DateTimeOffset - I lose the fractions of a second.  Not that it matters in my project but rätt skall vara rätt.
            Client.Update<PO.Chunk>(chunkNodeRef, node => { node.Set(chunkNodeRef.Id, start, stop, duration); });

            var rel = Client.CreateRelationship<PO.Worker, Relationships.HasWorkedRelationship>(
                owningWorkerNode.Reference, 
                new Relationships.HasWorkedRelationship( chunkNodeRef)
                );

            var ret = Get( chunkNodeRef.Id, Client );

            return ret;
        }

        private static void Delete(DTO.Chunk chunk, GraphClient Client)
        {
            var node = Get(chunk.ID, Client);
            Client.Delete(node.Reference, DeleteMode.NodeAndRelationships);
        }

        private static Node<PO.Chunk> Get(int id, GraphClient client)
        {
            return client.Get<PO.Chunk>(id);
        }

        private static Node<PO.Chunk> Update(DTO.Chunk chunk, GraphClient client)
        {
            Debug.Assert(chunk.ID >= 1);

            var chunkNode = client.Get<PO.Chunk>(chunk.ID);

            client.Update<PO.Chunk>( chunkNode.Reference, node =>
                node.Set(chunk.Start, chunk.Stop, chunk.Duration)
            );

            return Get( chunk.ID, client );
        }

    }
}
