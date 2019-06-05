using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb
{
    public class AgeRequireHandler : AuthorizationHandler<AgeRequireMent>
    {
        public IHttpContextAccessor _accessor;
        public AgeRequireHandler(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, AgeRequireMent requirement)
        {
           
            ClientUserData clientUserData = new ClientUserData()
            {
                UserId = 1,
                OrgId = 2,
                EmpId = 3
            };
            //AppSettings.Instance._accessor = _accessor;
             SecurityHelper._accessor = _accessor;
             SecurityHelper._accessor.HttpContext.User= new CustomPrincipal(clientUserData);
             context.Succeed(requirement);//标识验证成功
            
            return Task.CompletedTask;
        }
    }
    public class AgeRequireMent : IAuthorizationRequirement
    {
        public int Age { get; set; }
        public AgeRequireMent(int age) => this.Age = age;
    }
}
