using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.PO
{
    public class ListRoot<T>
    {
        private string _type;
        public string __Type
        {
            get { return _type; }
            set { _type = value; }
        }

        private string _itemType;
        public string ItemType
        {
            get { return _itemType; }
            set { _itemType = value; }
        }

        public int ID { get; set; }

        public string Name { get; set; }

        public ListRoot()
        {
            _type = this.GetType().ToString();
            _itemType = typeof(T).ToString();
        }

        public static ListRoot<T> Create(string name)
        {
            var ret = new ListRoot<T>();
            ret.Set(name);
            return ret;
        }

        internal ListRoot<T> SetID(int id)
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
