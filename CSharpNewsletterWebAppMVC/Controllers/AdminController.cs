using CSharpNewsletterWebAppMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CSharpNewsletterWebAppMVC.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Index()
        {
            //Accesses database using Entity Framework.
            using (NewsletterEntities db = new NewsletterEntities())
            {
                var signups = db.SignUps;
                var SignupVms = new List<SignupVm>();
                foreach (var signup in signups)
                {                // Best practice to map to a "view model" and not directly from the database.
                    var signupVm = new SignupVm();
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    SignupVms.Add(signupVm);
                }
                return View(SignupVms);
            }
        }
    }
}