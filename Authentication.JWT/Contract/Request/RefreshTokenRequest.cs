﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Authentication.JWT.Contract.Request
{
    public class RefreshTokenRequest
    {
        [Required(ErrorMessage = "{0} is required.")]
        public string UserName { get; set; }

        [Required(ErrorMessage = "{0} is required.")]
        public string RefreshToken { get; set; }
    }
}