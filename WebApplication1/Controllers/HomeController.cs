using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {

            //路径的几种方式
            var a = Server.MapPath("");

            var b= HttpRuntime.AppDomainAppPath.ToString();

            var c= Request.ApplicationPath;

            var d = AppDomain.CurrentDomain.BaseDirectory;

            var e = Environment.CurrentDirectory;

            var f= HttpRuntime.AppDomainAppPath;

            var g = Directory.GetCurrentDirectory();

            var h= HostingEnvironment.ApplicationPhysicalPath;



            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}