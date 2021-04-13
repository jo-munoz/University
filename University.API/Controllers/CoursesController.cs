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
    /// <summary>
    /// Controlador ApiController
    /// </summary>
    [RoutePrefix("api/Courses")]
    public class CoursesController : ApiController
    {
        private IMapper mapper;
        private readonly CourseService courseService = new CourseService(new CouseRepository(UniversityContext.Create()));

        /// <summary>
        /// Se instancia mapper
        /// </summary>
        public CoursesController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Obtiene los objetos de cursos
        /// </summary>
        /// <remarks>
        /// Aqui una descripción mas larga si se requiere
        /// </remarks>
        /// <returns>Objeto de curso</returns>
        /// <response code ="200">Ok. Devuelve el objeto solicitado.</response>
        [HttpGet]
        public async Task<IHttpActionResult> GetAll()
        {
            var courses = await courseService.GetAll();
            var coursesDTO = courses.Select(x => mapper.Map<CourseDTO>(x));

            return Ok(coursesDTO);
        }

        /// <summary>
        /// Obtiene un objeto de cursos por ID
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Objeto de curso</returns>
        /// <response code ="200">Ok. Devuelve el objeto solicitado.</response>        
        /// <response code ="404">NotFount. No se cumple con la validacion del modelo.</response>        
        [HttpGet]
        public async Task<IHttpActionResult> GetById(int Id)
        {
            var course = await courseService.GetById(Id);

            if (course == null)
                return NotFound();

            var courseDTO = mapper.Map<CourseDTO>(course);

            return Ok(courseDTO);
        }

        /// <summary>
        /// Crea un objeto de curso
        /// </summary>
        /// <param name="courseDTO"></param>
        /// <returns>Objeto de curso</returns>
        /// <response code ="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code ="400">BadRequest. No se cumple con el ModelState.</response>        
        /// <response code ="500">InternalServerError. Se ha presentado un error.</response>
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

        /// <summary>
        /// Edita un objeto de curso
        /// </summary>
        /// <param name="courseDTO"></param>
        /// <param name="Id"></param>
        /// <returns>Objeto de curso</returns>
        /// <response code ="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code ="400">BadRequest. No se cumple con el ModelState.</response>
        /// <response code ="404">NotFount. No se cumple con la validacion del modelo.</response>
        /// <response code ="500">InternalServerError. Se ha presentado un error.</response>
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

        /// <summary>
        /// Elimina un objeto de curso
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Objeto de curso</returns>
        /// <response code ="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code ="404">NotFount. No se cumple con la validacion del modelo.</response>
        /// <response code ="500">InternalServerError. Se ha presentado un error.</response>
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