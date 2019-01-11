using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using MobileStore.Models;

namespace MobileStore.Controllers
{
    public class HomeController : Controller
    {
        PhoneContext db = new PhoneContext();
        ApplicationContext db2 = new ApplicationContext();
        public ActionResult MainView()
        {
            ViewBag.IPhoneX = db.Phones.Where(x => x.Name.StartsWith("IPhone X")).FirstOrDefault().Id;
            ViewBag.SamsungGalaxy9 = db.Phones.Where(x => x.Name.StartsWith("Samsung Galaxy 9"))
                                    .FirstOrDefault().Id;
            ViewBag.IPhone9 = db.Phones.Where(x => x.Name.StartsWith("IPhone 9")).FirstOrDefault().Id;
            ViewBag.SamsungNote9 = db.Phones.Where(x => x.Name.StartsWith("Samsung Note 9")).FirstOrDefault().Id;
            return View();
        }

        public ActionResult AboutUs()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }

        [Authorize(Roles = "user")]
        public ActionResult Buy(int id)
        {
            Purchase purchase = new Purchase { PhoneId = id};
            return View(purchase);
        }
        
         [HttpPost]
        public ActionResult Buy(Purchase purchase)
        {
            purchase.Date = DateTime.Now;
            db.Purchases.Add(purchase);
            db.SaveChanges();
            return RedirectToAction("PostBuy", "Home", new Purchase { PersonEmail = purchase.PersonEmail });
        }
        
        public ActionResult PostBuy(Purchase purchase)
        {
            return View(purchase);
        }
        public ActionResult Query(string query)
        {
            IEnumerable<Phone> PhoneList = db.Phones.Where(x => x.Name.StartsWith(query));
            return View(PhoneList);
        }
        public ActionResult Details(int id)
        {
            Phone phone = db.Phones.Find(id);
            return View(phone);
        }

        public ActionResult PhoneList()
        {
            var list = db.Phones.ToList();
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public ActionResult PhoneListForAdmin()
        {
            var list = db.Phones.ToList();
            return View(list);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Create()
        {
            return View();

        }

        [HttpPost]
        public ActionResult Create(Phone phone)
        {
            if (ModelState.IsValid)
            {
                db.Phones.Add(phone);
                db.SaveChanges();
            }
            return RedirectToAction("PhoneListForAdmin");
        }
        [Authorize(Roles = "admin")]
        public ActionResult GetUsers()
        {
            IEnumerable<ApplicationUser> result = db2.Users.ToList();
            
            return View(result);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Delete(int? id)
        {
            Phone phone = db.Phones.Find(id);
            return View(phone);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            Phone phone = db.Phones.Find(id);
            if(phone != null)
            {
                db.Phones.Remove(phone);
                db.SaveChanges();
                return RedirectToAction("PhoneListForAdmin");
            }
            return HttpNotFound();
        }

        [Authorize(Roles = "admin")]
        public ActionResult DetailsAd(int id)
        {
            Phone phone = db.Phones.Find(id);
            return View(phone);
        }

        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            Phone phone = db.Phones.Find(id);
            return View(phone);
        }
        [HttpPost]
        public ActionResult Edit(Phone phone)
        {
            if (phone != null)
            {
                db.Entry(phone).State = EntityState.Modified;
                db.SaveChanges();
            }
            return RedirectToAction("PhoneListForAdmin");
        }
    }
}