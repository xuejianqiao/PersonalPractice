using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreWeb.Models;
using System.Text;
using System.Web;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace CoreWeb.Controllers
{
    public class HomeController : Controller
    {

        //[Authorize(Policy = "common")]
        public IActionResult Index()
        {
            Dictionary<string, object> payLoad = new Dictionary<string, object>();
            payLoad.Add("sub", "rober");
            payLoad.Add("jti", Guid.NewGuid().ToString());
            payLoad.Add("nbf", null);
            payLoad.Add("exp", null);
            payLoad.Add("iss", "roberIssuer");
            payLoad.Add("aud", "roberAudience");
            payLoad.Add("age", 30);
            var encodeJwt = TokenContext.CreateToken(payLoad, 30,null);
            return View();
        }
        [Authorize(Policy = "common")]
        public IActionResult About(int id)
        {
            ViewData["Message"] = "Your application description page.";
            return View();
        }

        //[Authorize(Policy = "multiClaim")]
        public IActionResult Contact(BaseRequest request)
        {

            //var validator = new BaseRequestValidate();

            //var results = validator.Validate(request);
            //bool validationSucceeded = results.IsValid;
            //if (!validationSucceeded)
            //{
            //    var errorMessages = results.Errors.ToList();
            //    string errorMsg = errorMessages.FirstOrDefault().ErrorMessage;
            //}
          
            return View();
        }

        //[Authorize(Policy = "onlyRober")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public async Task<IActionResult>  Login()
        {
            var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Name, "OpenId"),
            new Claim(ClaimTypes.Role, "Admin"), //如果是管理员
            new Claim("aud", "roberAudience")
        };
          var claimsIdentity = new ClaimsIdentity(claims, "Bearer");//“,"Mp"”不能省略，因为不是缺省名

           var authProperties = new AuthenticationProperties
            {
                AllowRefresh = true,
                IsPersistent = false,
                RedirectUri ="/Home/Contact"
           };
            await HttpContext.SignInAsync("Bearer", new ClaimsPrincipal(claimsIdentity), authProperties);
            return RedirectToAction("Contact");
        }



        /// <summary>
        /// 导出HTML为Excel文件
        /// </summary>
        /// <param name="dicSheet">导出内容：key是SheetName，Value是HTML代码</param>
        /// <param name="fileTitle">文件名</param>






    }
}
