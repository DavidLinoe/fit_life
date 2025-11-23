using fit_life.Models;

namespace fit_life.Services
{
    public interface IExercicioService
    {
        Task<List<Exercicio>> ObterTodos();
        Task<Exercicio> ObterPorId(int id);
        Task<Exercicio> CriarExercicio(string nome, string instrucoes, string area, int reps, int series, string? imagemBase64);
        Task<Exercicio> AtualizarExercicio(int id, string nome, string instrucoes, string area, int reps, int series, string? imagemBase64);
        Task<bool> DeletarExercicio(int id);
    }
}
