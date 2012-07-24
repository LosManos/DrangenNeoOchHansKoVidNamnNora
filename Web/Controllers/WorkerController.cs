using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Web.Controllers
{
    public class WorkerController : Controller
    {
        public ActionResult Index()
        {
            ViewBag.Message = "Workers work.";

            var bl = new BL.Worker();
            var lst = bl.GetAll();

            var model = new Models.WorkersModel();
            model.WorkerList = lst.Select(x => Models.WorkerModel.Create(x.ID, x.Name)).ToList();

            return View(model);
        }

        //
        // GET: /Worker/Details/5

        public ActionResult Details(int id)
        {
            var bl = new BL.Worker();
            var worker = bl.Get(id);

            var model = Models.WorkerModel.Create(worker.ID, worker.Name);

            return View( model);
        }

        //
        // GET: /Worker/Create

        public ActionResult Create()
        {
            throw new NotImplementedException();
        }

        //
        // POST: /Worker/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Worker/Edit/5

        //public ActionResult Edit()
        //{
        //    var model = Models.WorkerModel.Create(0, "");
        //    return View(model);
        //}

        public ActionResult Edit(int? id)
        {
            var bl = new BL.Worker();
            if (id.HasValue)
            {
                var worker = bl.Get(id.Value);
                var model = Models.WorkerModel.Create(worker.ID, worker.Name);
                return View(model);
            }
            else
            {
                var model = Models.WorkerModel.Create(0, string.Empty);
                return View(model);
            }
        }

        //
        // POST: /Worker/Edit/5

        [HttpPost]
        public ActionResult Edit(Models.WorkerModel model)
        {
            var bl = new BL.Worker();

            if (model.ID == 0)
            {
                var worker = bl.AddWorker(model.Name);
                model.Set(worker);
                return View("Details", model);
            }
            else
            {
                var worker = bl.Get(model.ID);
                worker.Set(model.Name);
                bl.Update(worker);
                model.Set(worker);
                return View("Details", model);
            }
        }

        //
        // GET: /Worker/Delete/5

        public ActionResult Delete(int id)
        {
            var bl = new BL.Worker();
            bl.Delete(id);

            return RedirectToAction("Index");
        }

        //
        // POST: /Worker/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
