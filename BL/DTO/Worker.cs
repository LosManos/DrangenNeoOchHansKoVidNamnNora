using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTO
{
    public class Worker
    {
        public string Name { get; set; }

        public int ID { get; set; }

        public static Worker Create(string name)
        {
            var ret = new Worker();
            ret.Set(name);
            return ret;
        }

        public static Worker Create(int id, string name)
        {
            var ret = new Worker();
            ret.Set(id, name);
            return ret;
        }

        //internal static Worker Create(int id, PO.Worker worker)
        //{
        //    return Create(id, worker.Name);
        //}

        internal static Worker Create(PO.Worker worker)
        {
            return Create(worker.ID, worker.Name);
        }

        public void Set(string name)
        {
            this.Name = name;
        }

        public void Set(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        internal DTO.Worker SetID(int id)
        {
            this.ID = id;
            return this;
        }
    }
}
