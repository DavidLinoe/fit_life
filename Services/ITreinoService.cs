namespace fit_life.Services
{
    public interface ITreinoService
    {
        void CriarTreino(string nome);
        void AdicionarExercicioAoTreino(int treinoId, int exercicioId);
    }
}
