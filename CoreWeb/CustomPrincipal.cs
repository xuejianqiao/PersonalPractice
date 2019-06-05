using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace CoreWeb
{

    public class SecurityHelper
    {
        public static IHttpContextAccessor _accessor;
      
        public static CustomPrincipal CurrentPrincipal
        {
            get
            {
               return _accessor.HttpContext.User as CustomPrincipal;
            }
        }
    }
    public class CustomPrincipal : ClaimsPrincipal
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public int RoleType { get; set; }
       
        public CustomPrincipal(ClientUserData clientUserData)
        {
            this.UserId = clientUserData.UserId;
            this.OrgId = clientUserData.OrgId;
            this.EmpId = clientUserData.EmpId;
        }
       
    }

    public class ClientUserData
    {
        /// <summary>
        /// 用户Id
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// 账号
        /// </summary>
        public string Account { get; set; }
        /// <summary>
        /// 员工ID
        /// </summary>
        public int EmpId { get; set; }

        /// <summary>
        /// 机构ID
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// 角色类型
        /// </summary>
        public int RoleType { get; set; }

        
        
    }
}
