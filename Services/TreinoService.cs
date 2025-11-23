using Microsoft.EntityFrameworkCore;
using fit_life.Data;
using fit_life.Models;

namespace fit_life.Services
{
    public class TreinoService : ITreinoService
    {
        private readonly DataContext _context;

        public TreinoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Treino>> ObterTodos()
        {
            return await _context.TreinoTable
                .Include(t => t.Exercicios)
                .ToListAsync();
        }
        public async Task<Treino> ObterPorId(int id)
        {
            return await _context.TreinoTable
                .Include(t => t.Exercicios)
                .FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Treino> CriarTreino(string nome, float tempo, List<int> exerciciosIds)
        {
            var treino = new Treino(nome, tempo);

            if (exerciciosIds != null && exerciciosIds.Any())
            {
                var exerciciosDoBanco = await _context.ExercicioTable
                    .Where(e => exerciciosIds.Contains(e.Id))
                    .ToListAsync();

                foreach (var exercicio in exerciciosDoBanco)
                {
                    treino.AdicionarExercicio(exercicio);
                }
            }

            _context.TreinoTable.Add(treino);
            await _context.SaveChangesAsync();

            return treino;
        }

        public async Task<Treino> AtualizarTempo(int id, float novoTempo)
        {
            var treino = await ObterPorId(id);
            if (treino == null) return null; 

            treino.AtualizarTempo(novoTempo); 
            await _context.SaveChangesAsync();
            return treino;
        }

        public async Task<Treino> AdicionarExercicio(int id, Exercicio exercicio)
        {
            var treino = await ObterPorId(id);
            if (treino == null) return null;

            treino.AdicionarExercicio(exercicio);
            await _context.SaveChangesAsync();

            return treino;
        }

        public async Task<bool> DeletarTreino(int id)
        {
            var treino = await _context.TreinoTable.FirstOrDefaultAsync(x => x.Id == id);
            if (treino == null) return false;

            _context.TreinoTable.Remove(treino);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
