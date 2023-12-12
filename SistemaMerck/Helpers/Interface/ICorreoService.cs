namespace SistemaMerck.Helpers.Interface
{
    public interface ICorreoService
    {
        Task EnviarCorreoAsync(string destinatario, string asunto, string cuerpo);
    }
}
