using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Web.Models
{
    public class WorkerModel
    {
        public int ID { get; set; }
        public string Name { get; set; }

        internal static WorkerModel Create(int id, string name)
        {
            var ret = new WorkerModel();
            ret.Set(id, name);
            return ret;
        }

        private void Set(int id, string name)
        {
            this.ID = id;
            this.Name = name;
        }

        internal void Set(BL.DTO.Worker worker)
        {
            Set(worker.ID, worker.Name);
        }
    }
}