using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [Route("error")]
    [ApiController]
    public class ABController : ControllerBase
    {
        public readonly studentsContext ctx;
        public ABController(studentsContext ctx)
        {
            this.ctx = ctx;
        }

        [Route("index")]
        [HttpGet]
        public void Get()
        {
            new RedirectToActionResult("Index", "Error", null);

        }
    }
}
