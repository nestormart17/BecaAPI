namespace BecarioAPI.Models.Solicitudes
{
    public interface ISolicitudesRepository
    {
        void AgregarSolicitud(Solicitud solicitud);
        void ActualizarSolicitud(Solicitud solicitud);
        void EliminarSolicitud(int IdSolicitud);
        Solicitud ObtenerSolicitud(int IdSolicitud);
        IEnumerable<Solicitud> ObtenerSolicitudes();
    }
}
