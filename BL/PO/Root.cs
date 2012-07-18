using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.PO
{
    public class Root
    {
        private string _type;
        public string __Type
        {
            get { return _type; }
            set { _type = value; }
        }
        public int ID { get; set; }

        public Root()
        {
            _type = this.GetType().ToString();
        }

        public static Root Create()
        {
            var ret = new Root();
            return ret;
        }

        internal PO.Root SetID(int id)
        {
            this.ID = id;
            return this;
        }

        internal void Set()
        {
        }

    }
}
