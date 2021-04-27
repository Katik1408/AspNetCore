using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentAPI.Models;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ABController : ControllerBase
    {
        public readonly studentsContext ctx;
        public ABController(studentsContext ctx)
        {
            this.ctx = ctx;
        }

        [Route("getstudents")]
        [HttpGet]
        public IActionResult Get()
        {
            return Ok(ctx.StudentsModel.ToList());
        }
    }
}
