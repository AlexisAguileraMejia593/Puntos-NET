using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Usuarios : ControllerBase
    {
        [Route("GetAll")]
        [HttpGet]
        public IActionResult GetAll()
        {
            var list = BL.Usuario.GetAll();
            if (list != null)
            {
                return Ok(list);
            }
            else
            {
                return NotFound();
            }

        }
        [Route("GetById/{idUsuarios?}")]
        [HttpGet]
        public IActionResult GetById(int idUsuarios)
        {
            var list = BL.Usuario.GetById(idUsuarios);
            if (list != null)
            {
                return Ok(list);

            }
            else
            {
                return NotFound();
            }
        }
        [Route("Add")]
        [HttpPost]
        public IActionResult Add([FromBody] ML.Usuario usuarios)
        {
            var result = BL.Usuario.Add(usuarios);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [Route("Delete/{idUsuarios?}")]
        [HttpDelete]
        public IActionResult Delete(int idUsuarios)
        {
            var result = BL.Usuario.Delete(idUsuarios);
            if (result.Correct)
            {
                return Ok(result.Correct);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("Update/{idUsuarios?}")]
        [HttpPut]
        public IActionResult Update(int idUsuarios, [FromBody] ML.Usuario usuarios)
        {
            usuarios.IdUsuario = idUsuarios;
            var result = BL.Usuario.Update(usuarios);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
