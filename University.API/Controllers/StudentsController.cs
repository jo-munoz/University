using AutoMapper;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using University.BL.Data;
using University.BL.DTOs;
using University.BL.Models;
using University.BL.Repositories.Implements;
using University.BL.Services.Implements;

namespace University.API.Controllers
{
    [RoutePrefix("api/Students")]
    public class StudentsController : ApiController
    {
        private IMapper mapper;
        private readonly StudentService studentService = new StudentService(new StudentRepository(UniversityContext.Create()));

        public StudentsController()
        {
            this.mapper = WebApiApplication.MapperConfiguration.CreateMapper();
        }

        /// <summary>
        /// Obtiene los objetos de estudiantes
        /// </summary>
        /// <returns></returns>
        /// <response code ="200">Ok. Devuelve el objeto solicitado.</response>
        [HttpGet]
        [ResponseType(typeof(IEnumerable<StudentDTO>))]
        public async Task<IHttpActionResult> GetStudents()
        {
            var studens = await studentService.GetAll();
            var studentsDTO = studens.Select(x => mapper.Map<StudentDTO>(x));

            return Ok(studentsDTO);
        }

        /// <summary>
        /// Ontiene un objeto de estudiante por ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Objeto de estudiante</returns>
        /// <response code ="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code ="404">NotFount. No se ha encontrado el objeto solicitado.</response>
        [HttpGet]
        [ResponseType(typeof(StudentDTO))]
        public async Task<IHttpActionResult> GetStudentsById(int id)
        {
            var studen = await studentService.GetById(id);

            if (studen == null)
            {
                return NotFound();
            }

            var studentDTO = mapper.Map<StudentDTO>(studen);

            return Ok(studentDTO);
        }

        /// <summary>
        /// Crear un objeto de estudiante
        /// </summary>
        /// <param name="studentDTO">Objeto del estudiante</param>
        /// <returns>Objeto del estudiante</returns>
        /// <response code ="200">Ok. Devuelve el objeto solicitado.</response>
        /// <response code ="404">NotFount. No se cumple con la validacion del modelo.</response>
        /// <response code ="500">InternalServerError. Se ha presentado un error.</response>
        [HttpPost]
        public async Task<IHttpActionResult> PostStudent(StudentDTO studentDTO)
        {
            if (!ModelState.IsValid)            
                return BadRequest(ModelState);            

            try
            {
                var student = mapper.Map<Student>(studentDTO);
                student = await studentService.Insert(student);
                studentDTO.ID = student.ID;
                return Ok(studentDTO);
            }
            catch (System.Exception ex)
            {
                return InternalServerError(ex);
            }

        }
    }
}
