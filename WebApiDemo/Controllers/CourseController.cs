using Microsoft.AspNetCore.Mvc;
using WebApiDemo.Models;
using WebApiDemo.Services;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApiDemo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseController : ControllerBase
    {
        private readonly ICourseService courseService;

        public CourseController(ICourseService courseService)
        {
            this.courseService = courseService;
        }

        // GET: api/<StudentsController>
        [HttpGet]
        public ActionResult<List<Course>> Get()
        {
            return courseService.Get();
        }

        // GET api/<StudentsController>/5
        [HttpGet("{id}")]
        public ActionResult<Course> Get(string id)
        {
            var course = courseService.Get(id);
            if (course == null)
            {
                return NotFound($"Student with Id = {id} not found");

            }
            return course;
        }

        // POST api/<StudentsController>
        [HttpPost]
        public ActionResult<Course> Post([FromBody] Course course)
        {
            courseService.Create(course);
            return CreatedAtAction(nameof(Get), new { id = course.Id }, course);
        }

        // PUT api/<StudentsController>/5
        [HttpPut("{id}")]
        public ActionResult Put(string id, [FromBody] Course course)
        {
            var existingStudent = courseService.Get(id);
            if (existingStudent == null)
            {
                return NotFound($"Student with Id = {id} not found");

            }
            courseService.Update(id, course);
            return NoContent();
        }

        // DELETE api/<StudentsController>/5
        [HttpDelete("{id}")]
        public ActionResult Delete(string id)
        {
            var student = courseService.Get(id);
            if (student == null)
            {
                return NotFound($"Student with Id = {id} not found");

            }
            courseService.Remove(student.Id);
            return Ok($"Student with Id = {id} deleted");
        }
    }
}




