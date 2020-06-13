﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace C2C_MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            if(init() == false)
                return RedirectToAction("Login","Auth");    
            return View();
        }

        public bool init()
        {
            if (Session["UserId"] == null)
                return false;
            return true;
        } 
    }
}