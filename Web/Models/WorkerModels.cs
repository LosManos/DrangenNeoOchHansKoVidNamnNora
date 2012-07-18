using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Security;

namespace Web.Models
{

    public class WorkersModel
    {
        public List<WorkerModel> WorkerList { get; set; }

        //[Required]
        //[DataType(DataType.EmailAddress)]
        //[Display(Name = "Email address")]
        //public string Email { get; set; }

        //[Required]
        //[StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        //[DataType(DataType.Password)]
        //[Display(Name = "Password")]
        //public string Password { get; set; }

        //[DataType(DataType.Password)]
        //[Display(Name = "Confirm password")]
        //[Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        //public string ConfirmPassword { get; set; }
    }

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
