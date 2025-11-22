namespace fit_life.Services
{
    public interface ILlmService
    {
        Task<string> ObterResposta(string prompt);
        Task<string> GerarTreino(string perfil, string objetivo, int diasPorSemana);
    }
}
