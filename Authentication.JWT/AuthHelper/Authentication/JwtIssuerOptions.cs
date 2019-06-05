using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.JWT.AuthHelper.Authentication
{
    public class JwtIssuerOptions
    {
        /// <summary>
        /// jwt签发者
        /// </summary>
        public string Issuer { get; set; }

        /// <summary>
        /// jwt所面向的用户
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// 接收jwt的一方
        /// </summary>
        public string Audience { get; set; }


        /// <summary>
        /// jwt的过期时间，这个过期时间必须要大于签发时间
        /// </summary>
        public DateTime Expiration => IssuedAt.Add(ValidFor);

        /// <summary>
        /// 定义在什么时间之前，该jwt都是不可用的.
        /// </summary>
        public DateTime NotBefore => DateTime.UtcNow;

        /// <summary>
        ///  jwt的签发时间
        /// </summary>
        public DateTime IssuedAt => DateTime.UtcNow;

       
        public TimeSpan ValidFor { get; set; } = TimeSpan.FromMinutes(10);

        /// <summary>
        /// jwt的唯一身份标识，主要用来作为一次性token,从而回避重放攻击。
        /// </summary>
        public Func<Task<string>> JtiGenerator =>
          () => Task.FromResult(Guid.NewGuid().ToString());

       /// <summary>
       /// token 签名配置
       /// </summary>
        public SigningCredentials SigningCredentials { get; set; }
    }
}
