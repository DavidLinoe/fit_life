using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fit_life.Data;
using fit_life.Models;

namespace fit_life.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TreinoController : ControllerBase
    {
        private readonly DataContext _context;

        public TreinoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/treino
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var treinos = await _context.TreinoTable
                .Include(t => t.Exercicios) // inclui os exercícios vinculados
                .ToListAsync();

            return Ok(treinos);
        }

        // GET api/treino/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var treino = await _context.TreinoTable
                .Include(t => t.Exercicios)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (treino == null)
                return NotFound(new { Message = $"Treino com Id={id} não encontrado." });

            return Ok(treino);
        }

        // POST api/treino
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTreinoRequest request)
        {
            if (request == null)
                return BadRequest("O corpo da requisição é inválido.");

            try
            {
                var treino = new Treino(request.Nome, request.Tempo);

                _context.TreinoTable.Add(treino);
                await _context.SaveChangesAsync();

                // Retorna o treino criado com os exercícios inclusos
                var createdTreino = await _context.TreinoTable
                    .Include(t => t.Exercicios)
                    .FirstOrDefaultAsync(t => t.Id == treino.Id);

                return CreatedAtAction(nameof(GetById), new { id = treino.Id }, createdTreino);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/treino/5/tempo
        [HttpPut("{id}/tempo")]
        public async Task<IActionResult> PutTempo(int id, [FromBody] float tempo)
        {
            var treino = await _context.TreinoTable.FirstOrDefaultAsync(x => x.Id == id);
            if (treino == null)
                return NotFound(new { Message = $"Treino com Id={id} não encontrado." });

            treino.AtualizarTempo(tempo);
            _context.TreinoTable.Update(treino);
            await _context.SaveChangesAsync();

            var updated = await _context.TreinoTable
                .Include(t => t.Exercicios)
                .FirstOrDefaultAsync(t => t.Id == id);

            return Ok(new
            {
                Message = "Tempo do treino atualizado com sucesso.",
                Updated = updated
            });
        }

        // POST api/treino/5/exercicios
        [HttpPost("{id}/exercicios")]
        public async Task<IActionResult> PostExercicio(int id, [FromBody] Exercicio exercicio)
        {
            if (exercicio == null)
                return BadRequest("O corpo da requisição é inválido.");

            var treino = await _context.TreinoTable
                .Include(t => t.Exercicios)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (treino == null)
                return NotFound(new { Message = $"Treino com Id={id} não encontrado." });

            treino.AdicionarExercicio(exercicio);
            _context.TreinoTable.Update(treino);
            await _context.SaveChangesAsync();

            var updated = await _context.TreinoTable
                .Include(t => t.Exercicios)
                .FirstOrDefaultAsync(t => t.Id == id);

            return Ok(new
            {
                Message = "Exercício adicionado ao treino com sucesso.",
                Updated = updated
            });
        }

        // DELETE api/treino/5/exercicios
        [HttpDelete("{id}/exercicios")]
        public async Task<IActionResult> DeleteExercicio(int id, [FromBody] Exercicio exercicio)
        {
            if (exercicio == null)
                return BadRequest("O corpo da requisição é inválido.");

            var treino = await _context.TreinoTable
                .Include(t => t.Exercicios)
                .FirstOrDefaultAsync(x => x.Id == id);

            if (treino == null)
                return NotFound(new { Message = $"Treino com Id={id} não encontrado." });

            treino.RemoverExercicio(exercicio);
            _context.TreinoTable.Update(treino);
            await _context.SaveChangesAsync();

            var updated = await _context.TreinoTable
                .Include(t => t.Exercicios)
                .FirstOrDefaultAsync(t => t.Id == id);

            return Ok(new
            {
                Message = "Exercício removido do treino com sucesso.",
                Updated = updated
            });
        }

        // DELETE api/treino/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var treino = await _context.TreinoTable.FirstOrDefaultAsync(x => x.Id == id);
            if (treino == null)
                return NotFound(new { Message = $"Treino com Id={id} não encontrado." });

            _context.TreinoTable.Remove(treino);
            await _context.SaveChangesAsync();

            return Ok(new { Message = $"Treino '{treino.Nome}' removido com sucesso." });
        }
    }

    // DTOs para as requisições
    public class CreateTreinoRequest
    {
        public string Nome { get; set; }
        public float Tempo { get; set; }
    }
}