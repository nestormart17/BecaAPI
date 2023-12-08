using BecarioAPI.Models.Solicitantes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BecarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitanteController : ControllerBase
    {
        //Inyeccion de dependencias
        #region Dependencias
        private readonly ISolicitantesRepository _solicitantesRepository;
        private readonly ILogger _logger;
        public SolicitanteController(ISolicitantesRepository solicitanteRepository, ILogger<SolicitanteController> logger)
        {
            _solicitantesRepository = solicitanteRepository;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos

        [HttpPost("NuevoSolicitante")]
        public ActionResult Agregarsolicitante(Solicitante solicitante)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest("Datos del solicitante invalidos");
                }
                _solicitantesRepository.Agregarsolicitante(solicitante);
                return Ok("Solicitante agregado Existosamente");
                
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Agregarsolicitante)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpPut("Actualizar/{solicitanteId}")]
        public ActionResult Actualizar(int solicitanteId, [FromBody] Solicitante solicitante)
        {
            try
            {
                // Validar si el solicitante existe antes de intentar actualizarlo
                var solicitanteExistente = _solicitantesRepository.ObtenerSolicitante(solicitanteId);   
                if (solicitanteExistente == null)
                {
                    return NotFound($"No se encontró un solicitante con el ID {solicitanteId}");
                }

                // Actualizar el solicitante
                _solicitantesRepository.Actualizar(solicitanteId, solicitante);
                return Ok("Solicitante actualizado Existosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en Actualizar: {ex.Message}");
                return StatusCode(500, "Error interno del servidor");
            }
        }


        [HttpDelete("EliminarSolicitante/{IdSolicitante}")]
        public ActionResult Eliminar(int IdSolicitante)
        {
            try
            {
                //validar si el campo esta vacio
                if (IdSolicitante == 0)
                {
                    return BadRequest("El id del solicitante es requerido");
                }
                //buscar el solicitante
                var solicitante = _solicitantesRepository.ObtenerSolicitante(IdSolicitante);
                //validar si el solicitante existe
                if (solicitante == null)
                {
                    return NotFound($"El solicitante con el id {IdSolicitante} no existe");
                }
                _solicitantesRepository.Eliminarsolicitante(IdSolicitante);
                return Ok("Solicitante eliminado Existosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Eliminar)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("ObtenerSolicitante/{IdSolicitante}")]
        public ActionResult SolicitanteID (int IdSolicitante)
        {
            try
            {
                //validar si el campo esta vacio
                if (IdSolicitante == 0)
                {
                    return BadRequest("El id del solicitante es requerido");
                }
                //buscar el solicitante
                var solicitante = _solicitantesRepository.ObtenerSolicitante(IdSolicitante);
                //validar si el solicitante existe
                if (solicitante == null)
                {
                    return NotFound($"El solicitante con el id {IdSolicitante} no existe");
                }
                return Ok(solicitante);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(SolicitanteID)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [HttpGet("ObtenerTodos")]
        public ActionResult ObtenerTodos()
        {
            try
            {
               var solicitantes = _solicitantesRepository.ObtenerTodos();
                if(solicitantes.Count == 0)
                {
                    return NotFound("No hay solicitantes registrados");
                }
                return Ok(solicitantes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(ObtenerTodos)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
       
    }
}
