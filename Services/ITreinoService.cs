using fit_life.Models;

namespace fit_life.Services
{
    public interface ITreinoService
    {
        Task<List<Treino>> ObterTodos();

        Task<Treino> ObterPorId(int id);

        Task<Treino> CriarTreino(string nome, float tempo);

        Task<Treino> AtualizarTempo(int id, float novoTempo);

        Task<Treino> AdicionarExercicio(int id, Exercicio exercicio);

        Task<bool> DeletarTreino(int id);
    }
}





