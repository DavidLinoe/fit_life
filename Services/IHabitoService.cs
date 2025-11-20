using fit_life.Models;

namespace fit_life.Services
{
    public interface IHabitoService
    {
        Task<List<Habito>> ObterTodos();
        Task<Habito> ObterPorId(int id);
        Task<Habito> CriarHabito(string nome, string execucao, string recomendacoes, float tempo);
        Task<Habito> AtualizarHabito(int id, string nome, string execucao, string recomendacoes, float tempo);
        Task<bool> DeletarHabito(int id);
    }
}
