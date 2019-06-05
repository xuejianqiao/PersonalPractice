using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "administrator")]
    public class AdministrationController : ControllerBase
    {

        [HttpGet("[action]")]
        public ActionResult<IEnumerable<string>> GetValueByAdminRole()
        {
            return new string[] { "use Roles = administrator" };
        }
    }
}