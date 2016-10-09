using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Premium_Web_Client.Controllers
{
    [Authorize]
    public class ClientPanelController : Controller
    {
        // GET: ClientPanel
        [Authorize]
        public ActionResult Index()
        {
            if (Session["UserLoged"] == null)
            {
                return Redirect("/Home/Logout");

            }
            User logeduser = (User)Session["UserLoged"];

            return View(logeduser);
         
        }

        // GET: ClientPanel/Details/5
        public ActionResult Inicio()
        {
            return View();
        }

        // GET: ClientPanel/Create
        public ActionResult Presupuesto()
        {
            return View();
        }

        // POST: ClientPanel/Create
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

        // GET: ClientPanel/Edit/5
        public ActionResult Productos()
        {
            return View();
        }

        // POST: ClientPanel/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: ClientPanel/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ClientPanel/Delete/5
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
