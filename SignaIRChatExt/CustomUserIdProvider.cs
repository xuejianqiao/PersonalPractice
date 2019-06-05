using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace SignaIRChatExt
{
    public class CustomUserIdProvider : IUserIdProvider
    {
        public virtual string GetUserId(HubConnectionContext connection)
        {
            var value= connection.User?.FindFirst(ClaimTypes.Email)?.Value;
            value = "123";
            return value;
        }
    }
}
