using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ChunkModel
    {
        public int ID { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset Stop { get; set; }
        public TimeSpan Duration { get; set; }
        public int OwningWorkerID { get; set; }

        public static ChunkModel Create(int id, DateTimeOffset start, DateTimeOffset? stop, TimeSpan? duration, int owningWorkerID)
        {
            var ret = new ChunkModel();
            ret.Set(id, start, stop, duration, owningWorkerID);
            return ret;
        }

        internal static ChunkModel Create(BL.DTO.Chunk chunk, int owningWorkerID)
        {
            return Create(chunk.ID, chunk.Start, chunk.Stop, chunk.Duration, owningWorkerID);
        }

        public void Set(int id, DateTimeOffset start, DateTimeOffset? stop, TimeSpan? duration, int owningWorkerID)
        {
            this.ID = id;
            this.Start = start;
            if (stop.HasValue)
            {
                duration = stop.Value - start;
            }
            else
            {
                stop = start + duration.Value;
            }
            this.Stop = stop.Value;
            this.Duration = duration.Value;
            this.OwningWorkerID = owningWorkerID;
        }

    }
}