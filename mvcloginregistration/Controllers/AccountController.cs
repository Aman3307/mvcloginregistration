﻿using MvcLoginRegistration.Models;
using System.Linq;
using System.Web.Mvc;

namespace MvcLoginRegistration.Controllers
{
    public class AccountController : Controller
    {
        // GET: account
        public ActionResult Index()
        {
            using (OurDbContext db = new OurDbContext())
            {
                return View(db.userAccount.ToList());
            }
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(UserAccount account)
        {
            if (ModelState.IsValid)
            {
                using (OurDbContext db = new OurDbContext())
                {
                    db.userAccount.Add(account);
                    db.SaveChanges();
                }
                ModelState.Clear();
                ViewBag.Message = account.FirstName + "" + account.LastName + "successfully registered.";
            }

            return View();
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(UserAccount user)
        {
            using (OurDbContext db = new OurDbContext())
            {
                var usr = db.userAccount.Single(u => u.Username == user.Username && u.Password == user.Password);
                if (usr != null)
                {
                    Session["UserId"] = usr.UserId.ToString();
                    Session["Username"] = usr.Username.ToString();
                    return RedirectToAction("LoggedIN");
                }
                else
                {
                    ModelState.AddModelError("", "username or password is wrong.");
                }
            }
            return View();
        }
        public ActionResult LoggedIn()
        {
            if (Session["UserId"]!= null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Login");
            }
        }

    }
} 