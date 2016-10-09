using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Premium_Web_Client.Models;
using System.Security.Cryptography;
using System.Text;

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
        public JsonResult ValidateEmail(string email)
        {

            pilogicEntities dbcontext = new pilogicEntities();
            User userget = dbcontext.User.Where(x => x.email.Equals(email, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
            if (userget != null)
            {

                return Json("El email ya se encuentra Registrado", JsonRequestBehavior.AllowGet);

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
            newuser.password = sha256(registro.Password);
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
        private string sha256(string password)
        {
            SHA256Managed crypt = new SHA256Managed();
            string hash = String.Empty;
            byte[] crypto = crypt.ComputeHash(Encoding.ASCII.GetBytes(password), 0, Encoding.ASCII.GetByteCount(password));
            foreach (byte theByte in crypto)
            {
                hash += theByte.ToString("x2");
            }
            return hash.Substring(0,11);
        }

        [HttpPost]
        public ActionResult Login(LoginModel userlogin)
        {

            if (ModelState.IsValid)

            {

                pilogicEntities dbcontext = new pilogicEntities();

                User userget = dbcontext.User.Where(x => x.username.Equals(userlogin.UserName, StringComparison.CurrentCultureIgnoreCase)).FirstOrDefault();
                userget.tokenid = userget.username + DateTime.Now.TimeOfDay.ToString();
                if (sha256(userlogin.PassWord) == userget.password)
                {
                    FormsAuthentication.SetAuthCookie(userget.username, true);
                   
                    if (Session["UserLoged"] == null && userget != null)
                    {
                        Session["UserLoged"] = userget;

                    }
                   
                    return Redirect("/ClientPanel/Index");

                }
                else
                {
                    ViewBag.errokey = "Contraseña Invalida";
                    return View("Login");
                }
                dbcontext.Dispose();
            }
            else
            {
                return View("Login");
            }
            
        }
        
        public ActionResult Inicio()
        {
            return View();
        }
       
        public ActionResult Logout()

        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
  
   /*     public ActionResult ClientPanel()
        {

        
           
        }*/

       

      

        // GET: Home/Edit/5
      
    }
}