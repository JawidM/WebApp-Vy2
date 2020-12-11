using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using VyModels;
using VyBLL;

namespace Vy2.Controllers
{
    public class LoginController : Controller
    {
        private IAdminLogic _adminBLL;

        public LoginController()
        {
            _adminBLL = new AdminBLL();
        }

        public LoginController(IAdminLogic stub)
        {
            _adminBLL = stub;
        }

        // GET: Security
        public ActionResult Index()
        {
            // Showing innlogging
            if (Session["LoggedIn"] == null)
            {
                Session["LoggedIn"] = false;
                ViewBag.Innlogget = false;
            }
            else
            {
                ViewBag.Innlogget = (bool)Session["LoggedIn"];
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(Admin innLogget)
        {
            var AdminLogic = new AdminLogic();
            // Cheking if logging in is OK
            if (AdminLogic.UserExsist(innLogget))
            {
                // If Email and Password is OK!
                Session["LoggedIn"] = true;
                ViewBag.Innlogget = true;
                return RedirectToAction("Index", "Routes", new { });
            }
            else
            {
                // If Email and Password is NOT OK!
                Session["LoggedIn"] = false;
                ViewBag.Innlogget = false;
                return View();
            }
        }

        private static byte[] fixHash(string innPassord, byte[] innSalt)
        {
            const int keyLength = 24;
            var pbkdf2 = new Rfc2898DeriveBytes(innPassord, innSalt, 1000); // 1000 angir hvor mange ganger hash funskjonen skal utføres for økt sikkerhet
            return pbkdf2.GetBytes(keyLength);
        }

        private static byte[] fixSalt()
        {
            var csprng = new RNGCryptoServiceProvider();
            var salt = new byte[24];
            csprng.GetBytes(salt);
            return salt;
        }

 

        public ActionResult LoggUt()
        {
            Session["LoggedIn"] = false;
            return RedirectToAction("index");
        }
    }
}