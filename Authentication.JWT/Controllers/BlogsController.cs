﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Authentication.JWT.AuthHelper;
using Authentication.JWT.Contract;
using Authentication.JWT.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.JWT.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class BlogsController : ControllerBase
    {
        private readonly IAuthorizationService _authorizationService;
        private readonly IBlogService _blogService;

        public BlogsController(IAuthorizationService authorizationService, IBlogService blogService)
        {
            _authorizationService = authorizationService;
            _blogService = blogService;
        }

        [HttpGet("{id}", Name = "Get")]
        public async Task<ActionResult<Blog>> Get(Guid id)
        {
            var blog = _blogService.GetBlogById(id);
            if ((await _authorizationService.AuthorizeAsync(User, blog, ResourceOperations.Read)).Succeeded)
            {
                return Ok(blog);
            }
            else
            {
                return Forbid();
            }
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Guid id, [FromBody] Blog newBlog)
        {
            var blog = _blogService.GetBlogById(id);
            if ((await _authorizationService.AuthorizeAsync(User, blog, ResourceOperations.Update)).Succeeded)
            {
                bool result = _blogService.Update(newBlog);
                return Ok(result);
            }
            else
            {
                return Forbid();
            }
        }
    }
}