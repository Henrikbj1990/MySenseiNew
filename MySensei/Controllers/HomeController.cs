﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Web;
using System.Web.Mvc;
using MySensei.Infrastructure;
using MySensei.Models;

namespace MySensei.Controllers
{

    public class HomeController : Controller
    {
        private readonly Repository _repository = new Repository();
        // GET: Home
        public ActionResult Index()
        {
            return View(_repository.GetCourses().Reverse().Take(4).ToList());
        }
    }
}