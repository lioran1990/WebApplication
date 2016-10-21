using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ShaulisBlog.Models;

namespace ShaulisBlog.Controllers
{
    public class StatisticsController : Controller
    {
        Statistics statistics = new Statistics();

        // GET: Statistics
        public ActionResult Index()
        {
            try
            {
                statistics.GenerateStatisticFiles(); //Generate new statistic files
            }
            catch
            {

            }

            return View();
        }

        public string ReGenerateStatisticFiles()
        {
            try
            {
                statistics.GenerateStatisticFiles(); //Generate new statistic files
                return "OK";
            }
            catch (Exception e)
            {
                return "ERROR";
            }
        }
    }
}