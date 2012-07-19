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
        public DateTime? Stop { get; set; }
        public TimeSpan Duration { get; set; }

        public void Set(int id, DateTime start, DateTime? stop, TimeSpan? duration)
        {
            this.ID = id;
            this.Start = start;

        }
    }
}