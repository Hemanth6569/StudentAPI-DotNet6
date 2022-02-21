using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentAPI.Controllers.Data;

namespace StudentAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private static List<Student> students = new List<Student>
            {
                new Student {
                Id = 1,
                Name = "hemanth",
                Age = 21,
                Address = "andhra",
                Dept = "MME"
                },
                new Student
                {
                    Id = 2,
                    Name = "medala",
                    Age = 21,
                    Address = "Pradesh",
                    Dept = "MME"
                }
            };
        private readonly DataContext _context;

        public StudentController(DataContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<List<Student>>> Get()
        {
            return Ok(await _context.Students.ToListAsync());
        }
        [HttpPost]
        public async Task<ActionResult<List<Student>>> AddStudent(Student student)
        {
            _context.Students.Add(student);
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }
        [HttpDelete]
        public async Task<ActionResult<List<Student>>> Delete(int id)
        {
            var dbStudent = await _context.Students.FindAsync(id);
            if (dbStudent == null)
                return BadRequest("Student not Found");
            _context.Students.Remove(dbStudent);
            await _context.SaveChangesAsync();
            return Ok(await _context.Students.ToListAsync());
        }
    }
}
