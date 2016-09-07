using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Premium_Web_Client.Models;

namespace Premium_Web_Client.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Login()
        {


            return View();

        }
        public ActionResult Registro()
        {


            return View();

        }
        public JsonResult Validate(string LoginName)
        {

            pilogicEntities dbcontext = new pilogicEntities();
            User userget = dbcontext.User.Where(x => x.username.Equals(LoginName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (userget != null)
            {

                return Json("El Usuario Ya Existe", JsonRequestBehavior.AllowGet);
               
            }
            else
            {
                return Json(true, JsonRequestBehavior.AllowGet);
               
            }
            dbcontext.Dispose();
        }
        [HttpPost]
        public ActionResult Registrado(RegisterModel registro)
        {
            try
            {
                pilogicEntities dbcontext = new pilogicEntities();
            User newuser = new User();
            newuser.tokenid = registro.LoginName.Trim();
            newuser.username = registro.LoginName.Trim();
            newuser.password = registro.Password;
            newuser.email = registro.Email;
            newuser.pubkey = "pub-c-358c0c48-3aa1-45d2-b23c-f4a789ab414e";
            newuser.subkey = "sub-c-c930c986-cdc4-11e5-8a35-0619f8945a4f";
            newuser.singupdate = DateTime.Now;
            newuser.expireddate = DateTime.Now.AddDays(30);
            newuser.cantusers = 1;
            dbcontext.User.Add(newuser);
          
                dbcontext.SaveChanges();
                ViewBag.Msg = "Registrado...";
            }
            catch (Exception)
            {
                ViewBag.Msg = "Error de conexion con la base de datos intente mas tarde";
            }

               
          
          

            return View();

        }

        [HttpPost]
        public ActionResult Login(LoginModel userlogin)
        {
            if (ModelState.IsValid)

            {
                pilogicEntities dbcontext = new pilogicEntities();
                
                User userget = dbcontext.User.Where(x => x.username.Equals(userlogin.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                if (userlogin.PassWord == userget.password)
                {
                    FormsAuthentication.SetAuthCookie(userget.username, true);
                    return View("Ingreso");

                }
                else
                {
                    return View("Login");
                }
                dbcontext.Dispose();
            }
            else
            {
                return View("Login");
            }
            
        }

        // GET: Home/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
        public ActionResult Logout()

        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }
        [Authorize]
        public ActionResult Ingreso(LoginModel userlogin)
        {


            return View();

        }

        // GET: Home/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Home/Create
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

        // GET: Home/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Home/Edit/5
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

        // GET: Home/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Home/Delete/5
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