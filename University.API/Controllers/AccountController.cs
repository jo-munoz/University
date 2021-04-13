using System.Web.Http;
using University.BL.DTOs;

namespace University.API.Controllers
{
    /// <summary>
    /// Controlador No blindado para que pueda ser consumido desde cualquier parte
    /// </summary>
    [AllowAnonymous]
    public class AccountController : ApiController
    {
        /// <summary>
        /// Metodo encargado de realizar la autenticación
        /// </summary>
        /// <param name="loginDTO"></param>
        /// <returns>Objeto de login</returns>
        /// <response code ="200">Ok. Devuelve el token.</response>
        /// <response code ="400">BadRequest. No se cumple con el ModelState.</response>        
        /// <response code ="401">Unauthorized. Autenticación no autorizado.</response>
        [HttpPost]
        public IHttpActionResult httpActionResult(LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            
            //Demo
            bool isCredentialValid = (loginDTO.Password == "123456");

            if (isCredentialValid)
            {
                var token = TokenGenerator.GenerateTokenJwt(loginDTO.UserName);
                return Ok(token);
            }
            else
                return Unauthorized();  //status code 401

        }
    }
}