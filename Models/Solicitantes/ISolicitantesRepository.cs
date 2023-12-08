namespace BecarioAPI.Models.Solicitantes
{
    public interface ISolicitantesRepository
    {
        void Agregarsolicitante(Solicitante solicitante);
        void Actualizar(int solicitanteid,Solicitante solicitante);
        void Eliminarsolicitante(int IdSolicitante);
        Solicitante ObtenerSolicitante(int IdSolicitante);
        List<Solicitante> ObtenerTodos();
    }
}
