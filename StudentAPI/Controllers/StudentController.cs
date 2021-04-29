using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.DataTransferObjects;
using StudentAPI.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace StudentAPI.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        public const int x = 7;
        public readonly studentsContext _context;
        private readonly IMapper _mapper;
        public StudentController(studentsContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        //Action
        [Route("GetStudents")]
        [HttpGet]

        public async Task<IActionResult> GetStudents()
        {
            //int a = 5;
            try
            {

                //var s = _context.StudentsModel.();
                var r = await (from s in _context.StudentsModel select s).ToListAsync();

                //var result = await _context.StudentsModel.ToListAsync();

                if (r != null)
                {
                    return Ok(r);
                }
                else
                {
                    return BadRequest();
                }
            }
            catch (Exception e)
            {
                return Unauthorized(e.StackTrace);
            }

        }

        [HttpGet]
        [Route("GetStudents/{id}")]
        public async Task<IActionResult> GetStudent(int id)
        {

            var r = await (from s in _context.StudentsModel
                           where s.Id == id
                           select new
                           {
                               s.Name,
                               s.Contact
                           }).ToListAsync();


            var result = await _context.StudentsModel.Where(w => w.Id == id).FirstOrDefaultAsync();
            if (r != null)
            {
                return Ok(r);
            }
            return NotFound();
        }

        [HttpPost]
        [Route("CreateNewStudent")]
        public async Task<IActionResult> CreateNewStudent([FromBody] StudentDto student)
        {
            try
            {

                //var r = student.SocialMediaAccount;
                await _context.StudentsModel.AddAsync(_mapper.Map<Student>(student));
                await _context.SaveChangesAsync();
                return Ok(student);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteStudent/{id}")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            var student = await _context.StudentsModel.FindAsync(id);
            if (student == null)
            {
                return BadRequest("No Student with id " + id);
            }
            else
            {
                _context.StudentsModel.Remove(student);
                await _context.SaveChangesAsync();
                return Ok("Delete Success");

            }
        }

        [HttpPut]
        [Route("UpdateStudent/{id}")]
        public async Task<IActionResult> UpadteStudent(int id, [FromBody] Student student)
        {
            var st = await _context.StudentsModel.FindAsync(id);
            if (st == null)
            {
                return BadRequest("No Student with id " + id);
            }
            else
            {
                st.Name = student.Name;
                st.Area = student.Area;
                st.Age = student.Age;
                st.Contact = student.Contact;

                _context.StudentsModel.Update(st);
                await _context.SaveChangesAsync();
                return Ok("Updated Successfully");

            }
        }

    }
}
