using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using VyDAL;
using VyDAL.DBModels;
using VyModels;

namespace Vy2.Controllers
{
    public class SecurityController : Controller
    {
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
            // Cheking if logging in is OK
            if (bruker_i_db(innLogget))
            {
                // If Email and Password is OK!
                Session["LoggedIn"] = true;
                ViewBag.Innlogget = true;
                return View();
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

        private static bool bruker_i_db(Admin innBruker)
        {
            using (var db = new DB())
            {
                AdminDb funnetBruker = db.Admins.FirstOrDefault(b => b.Email == innBruker.Email);
                if (funnetBruker != null)
                {
                    byte[] passordForTest = fixHash(innBruker.Password, funnetBruker.Salt);
                    bool riktigBruker = funnetBruker.Password.SequenceEqual(passordForTest);  // merk denne testen!
                    return riktigBruker;
                }
                else
                {
                    return false;
                }
            }
        }
        public ActionResult InnloggetSide()
        {
            if (Session["LoggedIn"] != null)
            {
                bool loggetInn = (bool)Session["LoggedIn"];
                if (loggetInn)
                {
                    return View();
                }
            }
            return RedirectToAction("Index");
        }
        public ActionResult LoggUt()
        {
            Session["LoggedIn"] = false;
            return RedirectToAction("index");
        }

        public ActionResult DecryptHash()
        {
            return View();
        }
    }
}