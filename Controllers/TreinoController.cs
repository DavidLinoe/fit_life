using fit_life.Data;
using fit_life.DTOs.Treino;
using fit_life.Models;
using fit_life.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace fit_life.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TreinoController : ControllerBase
    {
        private readonly ITreinoService _service;

        public TreinoController(ITreinoService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var treinos = await _service.ObterTodos();
            return Ok(treinos);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var treino = await _service.ObterPorId(id);

            if (treino == null)
                return NotFound(new { Message = "Treino não encontrado." });

            return Ok(treino);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateTreinoRequest request)
        {
            if (request == null) return BadRequest();

            try
            {
                var treino = new Treino(request.Nome, request.Tempo);
                var treinoCriado = await _service.CriarTreino(
                    request.Nome,
                    request.Tempo,
                    request.ExerciciosIds 
                );
                return CreatedAtAction(nameof(GetById), new { id = treinoCriado.Id }, treinoCriado);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}/tempo")]
        public async Task<IActionResult> PutTempo(int id, [FromBody] float tempo)
        {
            var treinoAtualizado = await _service.AtualizarTempo(id, tempo);

            if (treinoAtualizado == null)
                return NotFound(new { Message = "Treino não encontrado." });

            return Ok(new { Message = "Atualizado!", Updated = treinoAtualizado });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var sucesso = await _service.DeletarTreino(id);

            if (!sucesso) return NotFound();

            return Ok(new { Message = "Treino removido com sucesso." });
        }
    }
}