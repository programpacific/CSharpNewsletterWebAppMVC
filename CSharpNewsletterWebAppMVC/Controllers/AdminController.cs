using CSharpNewsletterWebAppMVC.Models;
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
                //Using Lambda syntax we have eliminated "removed" branded columns from the display!
                //var signups = db.SignUps.Where(x => x.Removed == null).ToList();

                //Using Linq we have eliminated "removed branded columns from the display!
                var signups = (from c in db.SignUps
                               where c.Removed == null
                               select c).ToList();


                var SignupVms = new List<SignupVm>();
                foreach (var signup in signups)
                {                // Best practice to map to a "view model" and not directly from the database. Especially to protect sensitive data now or in the future when that sensitive data set were to be added.
                    var signupVm = new SignupVm();
                    signupVm.Id = signup.Id;
                    signupVm.FirstName = signup.FirstName;
                    signupVm.LastName = signup.LastName;
                    signupVm.EmailAddress = signup.EmailAddress;
                    SignupVms.Add(signupVm);
                }
                return View(SignupVms);
            }
        }

        public ActionResult Unsubscribe(int Id)
        {
            using (NewsletterEntities db = new NewsletterEntities())
            {
                var signup = db.SignUps.Find(Id);
                // Removed is actually a column, this is adding the current time of removal, when searching for people that have been removed we can see if it is not a null and has a date, then they have asked to be removed.
                // This data can be especially valuable in business models and analitics.
                signup.Removed = DateTime.Now;
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}