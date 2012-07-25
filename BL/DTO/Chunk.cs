using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BL.DTO
{
    public class Chunk
    {
        public int ID { get; set; }
        public DateTimeOffset Start { get; set; }
        public DateTimeOffset Stop { get; set; }
        public TimeSpan Duration { get; set; }

        public static Chunk Create(int id, DateTimeOffset start, DateTimeOffset stop, TimeSpan duration)
        {
            return new Chunk().Set(id, start, stop, duration);
        }

        internal static Chunk Create(PO.Chunk chunk)
        {
            return new Chunk().Set(chunk.ID, chunk.StartDateTimeOffset, chunk.StopDateTimeOffset, chunk.DurationTimeSpan);
        }

        private Chunk Set(int id, DateTimeOffset start, DateTimeOffset stop, TimeSpan duration)
        {
            this.ID = id;
            this.Start = start;
            this.Stop = stop;
            this.Duration = duration;
            return this;
        }
    }
}
