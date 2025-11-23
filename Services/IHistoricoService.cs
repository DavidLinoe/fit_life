using fit_life.Models;

namespace fit_life.Services
{
    public interface IHistoricoService
    {
        Task<List<HistoricoTreino>> ObterHistoricoDoUsuario(int usuarioId);
        Task<HistoricoTreino> RegistrarConclusao(int usuarioId, int treinoId, int tempo);
    }
}
