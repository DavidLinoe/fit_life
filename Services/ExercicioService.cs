using fit_life.Data;
using fit_life.Models;
using Microsoft.EntityFrameworkCore;

namespace fit_life.Services
{
    public class ExercicioService : IExercicioService
    {
        private readonly DataContext _context;

        public ExercicioService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Exercicio>> ObterTodos()
        {
            return await _context.ExercicioTable.ToListAsync();
        }

        public async Task<Exercicio> ObterPorId(int id)
        {
            return await _context.ExercicioTable.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task<Exercicio> CriarExercicio(string nome, string instrucoes, string area, int reps, int series, string? imagemBase64)
        {
            var exercicio = new Exercicio(nome, instrucoes, area, reps, series, imagemBase64);

            _context.ExercicioTable.Add(exercicio);
            await _context.SaveChangesAsync();

            return exercicio;
        }

        public async Task<Exercicio> AtualizarExercicio(int id, string nome, string instrucoes, string area, int reps, int series, string? imagemBase64)
        {
            var exercicio = await ObterPorId(id);
            if (exercicio == null) return null;

            exercicio.Atualizar(nome, instrucoes, area, reps, series, imagemBase64);

            await _context.SaveChangesAsync();
            return exercicio;
        }

        public async Task<bool> DeletarExercicio(int id)
        {
            var exercicio = await ObterPorId(id);
            if (exercicio == null) return false;

            _context.ExercicioTable.Remove(exercicio);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}