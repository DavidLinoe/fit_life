using fit_life.DTOs.Exercicio;
using fit_life.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace fit_life.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ExercicioController : ControllerBase
    {
        private readonly IExercicioService _service;

        public ExercicioController(IExercicioService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var exercicios = await _service.ObterTodos();
            return Ok(exercicios);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var exercicio = await _service.ObterPorId(id);

            if (exercicio == null)
                return NotFound(new { Message = "Exercício não encontrado." });

            return Ok(exercicio);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateExercicioRequest request)
        {
            if (request == null) return BadRequest();

            try
            {
                var novoExercicio = await _service.CriarExercicio(
                    request.Nome,
                    request.Instrucoes,
                    request.AreaTreinada,
                    request.Repeticoes,
                    request.Series,
                    request.ImagemBase64
                );

                return CreatedAtAction(nameof(GetById), new { id = novoExercicio.Id }, novoExercicio);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateExercicioRequest request)
        {
            var exercicioAtualizado = await _service.AtualizarExercicio(
                id,
                request.Nome,
                request.Instrucoes,
                request.AreaTreinada,
                request.Repeticoes,
                request.Series,
                request.ImagemBase64
            );

            if (exercicioAtualizado == null)
                return NotFound(new { Message = "Exercício não encontrado." });

            return Ok(exercicioAtualizado);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeletarExercicio(id);

            if (!sucesso) return NotFound();

            return Ok(new { Message = "Exercício removido com sucesso." });
        }
    }

}