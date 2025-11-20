using fit_life.Data;
using fit_life.Models;
using Microsoft.EntityFrameworkCore;

namespace fit_life.Services
{
    public class HabitoService: IHabitoService
    {
        private readonly DataContext _context;

        public HabitoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Habito>> ObterTodos()
        {
            return await _context.HabitoTable.ToListAsync();
        }

        public async Task<Habito> ObterPorId(int id)
        {
            return await _context.HabitoTable.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Habito> CriarHabito(string nome, string execucao, string recomendacoes, float tempo)
        {
            var habito = new Habito(nome, execucao, recomendacoes, tempo);

            _context.HabitoTable.Add(habito);
            await _context.SaveChangesAsync();

            return habito;
        }

        public async Task<Habito> AtualizarHabito(int id, string nome, string execucao, string recomendacoes, float tempo)
        {
            var habito = await ObterPorId(id);
            if (habito == null) return null;
            habito.Atualizar(nome, execucao, tempo);
            await _context.SaveChangesAsync();
            return habito;
        }

        public async Task<bool> DeletarHabito(int id)
        {
            var habito = await ObterPorId(id);
            if (habito == null) return false;

            _context.HabitoTable.Remove(habito);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
