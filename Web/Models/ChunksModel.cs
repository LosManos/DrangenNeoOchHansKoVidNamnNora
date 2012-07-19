using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ChunksModel
    {
        public IList<ChunkModel> Items { get; set; }
        public string WorkerName { get; set; }

        public void Set(string workerName, IList<ChunkModel> items)
        {
            this.WorkerName = workerName;
            this.Items = items;
        }
    }
}