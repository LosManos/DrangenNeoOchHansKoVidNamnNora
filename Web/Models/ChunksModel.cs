using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ChunksModel
    {
        public IList<ChunkModel> Chunks { get; set; }
        public int WorkerID { get; set; }
        public string WorkerName { get; set; }

        public static ChunksModel Create(int workerID, string workerName, IList<ChunkModel> chunks)
        {
            return new ChunksModel().Set(workerID, workerName, chunks);
        }

        public ChunksModel Set(int workerID, string workerName, IList<ChunkModel> chunks)
        {
            this.WorkerID = workerID;
            this.WorkerName = workerName;
            this.Chunks = chunks;
            return this;
        }
    }
}