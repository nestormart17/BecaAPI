using BecarioAPI.Models.Solicitudes;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BecarioAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SolicitudController : Controller
    {
        //inyeccion de dependencias
        #region Dependencias
        private readonly ISolicitudesRepository _solicitudesRepository;
        private readonly ILogger _logger;
        public SolicitudController(ISolicitudesRepository solicitudesRepository, ILogger<SolicitudController> logger)
        {
            _solicitudesRepository = solicitudesRepository;
            _logger = logger;
        }
        #endregion

        //Implementacion de los metodos

        #region Nueva Solicitud
        [HttpPost("NuevaSolicitud")]
        public ActionResult AgregarSolicitud([FromBody] Solicitud solicitud)
        {
            try
            {
                if (!ModelState.IsValid || solicitud == null)
                {
                    return BadRequest("Datos de solicitud invalidos");
                }
                _solicitudesRepository.AgregarSolicitud(solicitud);
                return Ok("Solicitud Creada Exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AgregarSolicitud)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region Actualizar Solicitud
        [HttpPut("ActualizarSolicitud")]
        public ActionResult ActualizarSolicitud([FromBody] Solicitud solicitud)
        {
            try
            {
                if (!ModelState.IsValid || solicitud == null)
                {
                    return BadRequest("Datos de solicitud invalidos");
                }
                _solicitudesRepository.ActualizarSolicitud(solicitud);
                return Ok("Solicitud Actualizada Exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(ActualizarSolicitud)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region Eliminar Solicitud
        [HttpDelete("EliminarSolicitud/{id}")]
        public ActionResult EliminarSolicitud(int id)
        {
            try
            {
                if (id <= 0)
                {
                    return BadRequest("Id de solicitud invalido");
                }
                _solicitudesRepository.EliminarSolicitud(id);
                return Ok("Solicitud Eliminada Exitosamente");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(EliminarSolicitud)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region Obtener Solicitud
        [HttpGet("ObtenerSolicitud/{id}")]
        public ActionResult ObtenerSolicitud(int id)
        {
            try
            {
                //Validar que el id no sea nulo
                if (id <= 0)
                {
                    return BadRequest("Id de solicitud invalido");
                }
                var solicitud = _solicitudesRepository.ObtenerSolicitud(id);
                if (solicitud == null)
                {
                    return NotFound("No se encontro la solicitud");
                }
                return Ok(solicitud);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(ObtenerSolicitud)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion

        #region Obtener Solicitudes
        [HttpGet("ObtenerSolicitudes")]
        public ActionResult ObtenerSolicitudes()
        {
            try
            {
                var solicitudes = _solicitudesRepository.ObtenerSolicitudes();
                if (solicitudes.Count() == 0)
                {
                    return NotFound("No se encontraron solicitudes");
                }
                return Ok(solicitudes);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(ObtenerSolicitudes)}: " + ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
        #endregion
    }
}
