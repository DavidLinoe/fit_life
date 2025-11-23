using fit_life.Data;
using fit_life.Models;
using Microsoft.EntityFrameworkCore;

namespace fit_life.Services
{
    public class HistoricoService : IHistoricoService
    {
        private readonly DataContext _context;

        public HistoricoService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<HistoricoTreino>> ObterHistoricoDoUsuario(int usuarioId)
        {
            return await _context.HistoricoTreinoTable
                .Where(h => h.UsuarioId == usuarioId)
                .OrderByDescending(h => h.DataConclusao)
                .Take(5)
                .ToListAsync();
        }

        public async Task<HistoricoTreino> RegistrarConclusao(int usuarioId, int treinoId, int tempo)
        {
            bool treinoExiste = await _context.TreinoTable.AnyAsync(t => t.Id == treinoId);
            if (!treinoExiste)
            {
                throw new ArgumentException("O Treino informado não existe.");
            }

            var historico = new HistoricoTreino(usuarioId, treinoId, tempo);
            _context.HistoricoTreinoTable.Add(historico);
            await _context.SaveChangesAsync();
            return historico;
        }
    }
}

