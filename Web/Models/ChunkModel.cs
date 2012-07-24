using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class ChunkModel
    {
        public int ID { get; set; }
        public DateTime Start { get; set; }
        public DateTime Stop { get; set; }
        public TimeSpan Duration { get; set; }
        public int OwningWorkerID { get; set; }

        public static ChunkModel Create(int id, DateTime start, DateTime? stop, TimeSpan? duration, int owningWorkerID)
        {
            var ret = new ChunkModel();
            ret.Set(id, start, stop, duration, owningWorkerID);
            return ret;
        }

        public void Set(int id, DateTime start, DateTime? stop, TimeSpan? duration, int owningWorkerID)
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