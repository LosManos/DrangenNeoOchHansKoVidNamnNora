using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class ChunkController : Controller
    {
        public ActionResult Index(int id)
        {
            Debug.Assert(0 != id);

            var bl = new BL.Worker();
            var worker = bl.Get(id);

            //  HACK:   Test data.
            var chunks = new List<Models.ChunkModel>() { Models.ChunkModel.Create(-1, DateTime.Now, null, new TimeSpan(0, 30, 0), id) };

            var model = Models.ChunksModel.Create(
                id,
                worker.Name, 
                chunks);

            return View(model);
        }

        //public ActionResult Details(int id)
        //{
        //    Debug.Assert(0 != id);

        //    var bl = new BL.Worker();
        //    var worker = bl.Get(id);

        //    //  HACK:   Test data.
        //    var chunks = new List<Models.ChunkModel>() { Models.ChunkModel.Create(-1, DateTime.Now, null, new TimeSpan(0, 30, 0), id) };

        //    var model = Models.ChunksModel.Create(
        //        id,
        //        worker.Name,
        //        chunks);

        //    return View(model);
        //}
    }
}
