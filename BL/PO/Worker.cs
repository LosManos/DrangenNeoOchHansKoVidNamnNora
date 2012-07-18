using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.PO
{
    public class Worker
    {
        //[Neo4jClient.Serializer]
        public string Name { get; set; }

        private string _type;
        public string __Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int ID { get; set; }

        public Worker()
        {
            _type = this.GetType().ToString();
        }

        public static Worker Create(string name)
        {
            var ret = new Worker();
            ret.Set(name);
            return ret;
        }

        internal PO.Worker SetID(int id)
        {
            this.ID = id;
            return this;
        }

        internal void Set(string name)
        {
            this.Name = name;
        }

    }
}
