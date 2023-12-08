using BecarioAPI.Models.Solicitudes;
using Microsoft.EntityFrameworkCore;

namespace BecarioAPI.Models.Solicitantes
{
    public class SolicitantesRepository : ISolicitantesRepository
    {
        //Inyeccion de dependencias
        #region Dependencias
        private readonly BecarioDBContext _db;
        private readonly ILogger _logger;
        public SolicitantesRepository(BecarioDBContext db, ILogger<SolicitantesRepository> logger)
        {
            _db = db;
            _logger = logger;
        }

       
        #endregion

        public void Agregarsolicitante(Solicitante solicitante)
        {
            try
            {
                _db.Solicitantes.Add(solicitante);
                _db.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Agregarsolicitante)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void Actualizar(int solicitanteid, Solicitante solicitante)
        {
            try
            {
                //buscar el solicitante que se quiere actualizar
                var solicitanteActual =_db.Solicitantes.Find(solicitanteid);
                if (solicitanteActual != null)
                {
                    //actualizar campos que se requieran
                    solicitanteActual.IdSolicitante = solicitanteid;
                    solicitanteActual.Nombre = solicitante.Nombre ?? solicitanteActual.Nombre;
                    solicitanteActual.Apellido = solicitante.Apellido ?? solicitanteActual.Apellido;
                    solicitanteActual.FechaNacimiento = solicitante.FechaNacimiento != default ? solicitante.FechaNacimiento : solicitanteActual.FechaNacimiento;
                    solicitanteActual.EstadoCivil = solicitante.EstadoCivil ?? solicitanteActual.EstadoCivil;
                    solicitanteActual.Dui = solicitante.Dui ?? solicitanteActual.Dui;
                    solicitanteActual.Pasaporte = solicitante.Pasaporte ?? solicitanteActual.Pasaporte;
                    solicitanteActual.FechaExpedicionPasaporte = solicitante.FechaExpedicionPasaporte != default ? solicitante.FechaExpedicionPasaporte : solicitanteActual.FechaExpedicionPasaporte;
                    solicitanteActual.LugardeExpedicion = solicitante.LugardeExpedicion ?? solicitanteActual.LugardeExpedicion;
                    solicitanteActual.Direccion = solicitante.Direccion ?? solicitanteActual.Direccion;
                    solicitanteActual.Email = solicitante.Email ?? solicitanteActual.Email;
                    solicitanteActual.Telefono = solicitante.Telefono ?? solicitanteActual.Telefono;

                    _db.Entry(solicitanteActual).State = EntityState.Modified;
                    _db.SaveChangesAsync();

                }
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Solicitante actualizado: ID {solicitanteid}, Nombre: {solicitante.Nombre}");
                _logger.LogError($"Error en {nameof(Actualizar)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

       

        public void Eliminarsolicitante(int IdSolicitante)
        {
            try
            {
                var solicitante = _db.Solicitantes.Find(IdSolicitante);
                if (solicitante != null)
                {
                    _db.Solicitantes.Remove(solicitante);
                    _db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(Eliminarsolicitante)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public Solicitante ObtenerSolicitante(int IdSolicitante)
        {
            try
            {
                var solicitante = _db.Solicitantes.Find(IdSolicitante);
                if (solicitante != null)
                {
                    return solicitante;
                }else
                {
                    return new Solicitante();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(ObtenerSolicitante)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public List<Solicitante> ObtenerTodos()
        {
            try
            {
                return _db.Solicitantes.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error en {nameof(ObtenerTodos)}: " + ex.Message);
                throw new Exception(ex.Message);
            }
        }

        
    }
}
