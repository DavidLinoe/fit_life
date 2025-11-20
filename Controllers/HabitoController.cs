using fit_life.Services;
using Microsoft.AspNetCore.Mvc;

namespace fit_life.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HabitoController : ControllerBase
    {
        private readonly IHabitoService _service;

        public HabitoController(IHabitoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var habitos = await _service.ObterTodos();
            return Ok(habitos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var habito = await _service.ObterPorId(id);

            if (habito == null)
                return NotFound(new { Message = "Hábito não encontrado." });

            return Ok(habito);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateHabitoRequest request)
        {
            if (request == null) return BadRequest();

            try
            {
                var novoHabito = await _service.CriarHabito(
                    request.Nome,
                    request.Execucao,
                    request.Recomendacoes,
                    request.Tempo
                );

                return CreatedAtAction(nameof(GetById), new { id = novoHabito.Id }, novoHabito);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateHabitoRequest request)
        {
            var habitoAtualizado = await _service.AtualizarHabito(
                id,
                request.Nome,
                request.Execucao,
                request.Recomendacoes,
                request.Tempo
            );

            if (habitoAtualizado == null)
                return NotFound(new { Message = "Hábito não encontrado." });

            return Ok(habitoAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeletarHabito(id);

            if (!sucesso) return NotFound();

            return Ok(new { Message = "Hábito removido com sucesso." });
        }
    }

    public class CreateHabitoRequest
    {
        public string Nome { get; set; }
        public string Execucao { get; set; }
        public string Recomendacoes { get; set; }
        public float Tempo { get; set; }
    }

    public class UpdateHabitoRequest : CreateHabitoRequest
    {
    }
}