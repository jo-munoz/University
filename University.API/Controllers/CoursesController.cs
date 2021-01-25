using AutoMapper;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;

namespace University.API.Controllers
{
    [RoutePrefix("api/Courses")]
    public class CoursesController : ApiController
    {
        private IMapper mapper;
        private readonly CourseService courseService = new CourseService(new CouseRepository(UniversityContext.Create()));

        public CoursesController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var courses = await courseService.GetAll();
            var coursesDTO = courses.Select(x => mapper.Map<CourseDTO>(x));

            return Ok(coursesDTO);
        }

        [HttpGet]
        public async Task<IHttpActionResult> GetById(int Id)
        {
            var course = await courseService.GetById(Id);

            if (course == null)
                return NotFound();

            var courseDTO = mapper.Map<CourseDTO>(course);

            return Ok(courseDTO);
        }

        [HttpPost]
        public async Task<IHttpActionResult> Post(CourseDTO courseDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var course = mapper.Map<Course>(courseDTO);
                course = await courseService.Insert(course);
                return Ok(course);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpPut]
        public async Task<IHttpActionResult> Put(CourseDTO courseDTO, int Id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (courseDTO.CourseID != Id)
                return BadRequest(ModelState);

            try
            {
                var flag = await courseService.GetById(Id);

                if (flag == null)
                    return NotFound();

                var course = mapper.Map<Course>(courseDTO);
                course = await courseService.Update(course);
                return Ok(course);
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }

        [HttpDelete]
        public async Task<IHttpActionResult> Delete(int Id)
        {
            var flag = await courseService.GetById(Id);

            if (flag == null)
                return NotFound();

            try
            {
                if (!await courseService.DeleteCheckOnEntity(Id))
                    await courseService.Delete(Id);
                else
                    throw new Exception("ForeignKeys");

                return Ok();
            }
            catch (Exception ex)
            {

                return InternalServerError(ex);
            }
        }
    }
}
