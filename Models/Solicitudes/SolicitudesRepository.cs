
using Microsoft.EntityFrameworkCore;

namespace BecarioAPI.Models.Solicitudes
{
    public class SolicitudesRepository : ISolicitudesRepository
    {
        //inyeccion de dependencias
        #region Dependencias
        private readonly BecarioDBContext _db;
        private readonly ILogger _logger;
        public SolicitudesRepository(BecarioDBContext db, ILogger<SolicitudesRepository> logger)
        {
            _db = db;
            _logger = logger;
        }
        #endregion

        public void AgregarSolicitud(Solicitud solicitud)
        {
            try
            {
                _db.Solicitudes.Add(solicitud);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(AgregarSolicitud)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarSolicitud(Solicitud solicitud)
        {
            try
            {
                // Buscar la solicitud en la base de datos utilizando su identificador
                var solicitudActual = _db.Solicitudes.Find(solicitud.IdSolicitud);

                // Verificar si se encontró la solicitud con el identificador dado
                if (solicitudActual != null)
                {
                    // Actualizar campos
                    solicitudActual.Nombre = solicitud.Nombre ?? solicitudActual.Nombre;
                    solicitudActual.IdSolicitante = solicitud.IdSolicitante > 0 ? solicitud.IdSolicitante : solicitudActual.IdSolicitante;
                    solicitudActual.FechadeCreacion = solicitud.FechadeCreacion != default ? solicitud.FechadeCreacion : solicitudActual.FechadeCreacion;
                    solicitudActual.IdEstado = solicitud.IdEstado > 0 ? solicitud.IdEstado : solicitudActual.IdEstado;

                    // Marcar el objeto como modificado para que Entity Framework realice las actualizaciones en la base de datos
                    _db.Entry(solicitudActual).State = EntityState.Modified;

                    // Guardar los cambios en la base de datos
                    _db.SaveChanges();
                }
                else
                {
                    // Si no se encuentra la solicitud, lanzar una excepción
                    throw new Exception("No se encontró la solicitud a actualizar.");
                }
            }
            catch (Exception ex)
            {
                // Manejar otras excepciones
                _logger.LogError(ex, "Error inesperado al actualizar la solicitud.");
                throw new Exception("Error inesperado al actualizar la solicitud.", ex);
            }
        }

        public void EliminarSolicitud(int IdSolicitud)
        {
            var solicitudelminar = _db.Solicitudes.Find(IdSolicitud);
            if (solicitudelminar != null)
            {
                _db.Solicitudes.Remove(solicitudelminar);
                try
                {
                    _db.SaveChanges();
                }
                catch (Exception ex)
                {
                    _logger.LogError($"Error en {nameof(EliminarSolicitud)}: " + ex.Message);
                    throw new Exception(ex.Message);
                }
            }
            else
            {
                throw new Exception("No se encontro la solicitud a eliminar");
            }

        }

        public Solicitud ObtenerSolicitud(int IdSolicitud)
        {
            var solicitudUnica = _db.Solicitudes.Find(IdSolicitud);
            if (solicitudUnica != null)
            {
                return solicitudUnica;
            }
            else
            {
                throw new Exception("No se encontro la solicitud");
            }
        }

        public IEnumerable<Solicitud> ObtenerSolicitudes()
        {
            return _db.Solicitudes.ToList();
        }
    }
}
