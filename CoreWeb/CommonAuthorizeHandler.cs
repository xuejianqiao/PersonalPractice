using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using System.Net;
using System.Threading.Tasks;
namespace CoreWeb
{
    public class CommonAuthorizeHandler : AuthorizationHandler<CommonAuthorize>
    {
        /// <summary>
        /// 常用自定义验证策略，模仿自定义中间件JwtCustomerauthorizeMiddleware的验证范围
        /// </summary>
        /// <param name="context"></param>
        /// <param name="requirement"></param>
        /// <returns></returns>
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, CommonAuthorize requirement)
        {
            var httpContext = (context.Resource as AuthorizationFilterContext).HttpContext;
            //var userContext = httpContext.RequestServices.GetService(typeof(UserContext)) as UserContext;

            #region 身份验证，并设置用户Ruser值

            var result = httpContext.Request.Headers.TryGetValue("Authorization", out StringValues authStr);
            if (!result || string.IsNullOrEmpty(authStr[0].ToString()))
            {
                return Task.CompletedTask;
            }
            result = TokenContext.Validate(authStr[0].ToString(), payLoad =>
            {
                var success = true;
                //可以添加一些自定义验证，用法参照测试用例
                //验证是否包含aud 并等于 roberAudience
                success = success && payLoad["aud"]?.ToString() == "Audience";
               
                return success;
            });
            if (!result)
            {
                HandleExceptionAsync(httpContext, 401, "123");
                return Task.CompletedTask;
            }
          
            #endregion
            #region 权限验证
            //if (!userContext.Authorize(httpContext.Request.Path))
            //{
            //    return Task.CompletedTask;
            //}
            #endregion


            context.Succeed(requirement);
            return Task.CompletedTask;
        }

        private static Task HandleExceptionAsync(HttpContext context, int statusCode, string msg)
        {
            var data = new { code = statusCode.ToString(), is_success = false, msg = msg };
            var result = JsonConvert.SerializeObject(new { data = data });
            context.Response.ContentType = "application/json;charset=utf-8";
            return context.Response.WriteAsync(result);
        }
    }


    public class CommonAuthorize : IAuthorizationRequirement
    {

    }
}
