using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Unlity;

namespace FrameWorkWeb.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
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

        public void GreenCarSummaryExport(FormCollection collection)
        {

            WebClient client = new WebClient();
            client.Encoding = System.Text.Encoding.UTF8;
            string returnStr = client.UploadString("https://www.baidu.com/", "");

            string tbBMXXHTML = HttpUtility.UrlDecode(returnStr);
            string tbBMXXTitle = "主标题";
            string FileTitle = "标题测试";

            Dictionary<string, string> dicSheet = new Dictionary<string, string>();
            dicSheet.Add(tbBMXXTitle, tbBMXXHTML);

            //把HTML转换为Excel
           Export.ExportHTMLToExcel(dicSheet, FileTitle);
        }

    }
}