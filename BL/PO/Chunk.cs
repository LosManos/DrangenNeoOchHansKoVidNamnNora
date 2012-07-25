using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BL.PO
{
    public class Chunk
    {
        private string _type;
        public string __Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int ID { get; set; }

        private DateTimeOffset _start;

        //  http://stackoverflow.com/questions/637933/net-how-to-serialize-a-timespan-to-xml
        [XmlIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public DateTimeOffset StartDateTimeOffset
        { 
            get{return _start;}
            set{ _start = value;}
        }

        [XmlElement("Start")]
        public string Start
        {
            get { return _start.ToString("o", System.Globalization.CultureInfo.InvariantCulture); }
            set { _start = string.IsNullOrWhiteSpace(value) ? DateTimeOffset.MinValue : DateTimeOffset.Parse(value); }
        }

        private DateTimeOffset _stop;

        [XmlIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public DateTimeOffset StopDateTimeOffset
        {
            get { return _stop; }
            set { _stop = value; }
        }

        [XmlElement("Stop")]
        public string Stop
        {
            get { return _stop.ToString("o", System.Globalization.CultureInfo.InvariantCulture); }
            set { _stop = string.IsNullOrWhiteSpace(value) ? DateTime.MinValue : DateTime.Parse(value); }
        }

        private TimeSpan _duration;

        [XmlIgnore]
        [Newtonsoft.Json.JsonIgnore]
        public TimeSpan DurationTimeSpan
        {
            get { return _duration; }
            set { _duration = value; }
        }

        [XmlElement("Duration")]
        public string Duration
        {
            get { return _duration.ToString(); }
            set { _duration = string.IsNullOrWhiteSpace(value) ? TimeSpan.Zero : TimeSpan.Parse(value); }
        }


        public Chunk()
        {
            _type = this.GetType().ToString();
        }

        public static Chunk Create(DateTimeOffset start, DateTimeOffset stop, TimeSpan duration)
        {
            return new Chunk().Set(start, stop, duration);
        }

        internal Chunk SetID(int id)
        {
            this.ID = id;
            return this;
        }

        internal Chunk Set(DateTimeOffset start, DateTimeOffset stop, TimeSpan duration)
        {
            this.StartDateTimeOffset = start;
            this.StopDateTimeOffset = stop;
            this.DurationTimeSpan = duration;
            return this;
        }

        internal Chunk Set(int id, DateTimeOffset start, DateTimeOffset stop, TimeSpan duration)
        {
            this.ID = id;
            Set(start, stop, duration);
            return this;
        }
    }
}
