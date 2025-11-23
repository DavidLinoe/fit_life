using fit_life.DTOs.HistoricoTreino;
using fit_life.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace fit_life.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HistoricoTreinoController : ControllerBase
    {
        private readonly IHistoricoService _service;

        public HistoricoTreinoController(IHistoricoService service)
        {
            _service = service;
        }

        [HttpGet("meus-treinos")]
        public async Task<IActionResult> ObterMeusUltimosTreinos()
        {
            var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
            var historico = await _service.ObterHistoricoDoUsuario(userId);

            return Ok(historico);
        }

        [HttpPost("concluir")]
        public async Task<IActionResult> ConcluirTreino([FromBody] ConcluirTreinoRequest request)
        {
            try
            {
                var userId = int.Parse(User.FindFirst(System.Security.Claims.ClaimTypes.NameIdentifier).Value);
                var registro = await _service.RegistrarConclusao(userId, request.TreinoId, request.TempoGasto);

                return Ok(new { mensagem = "Treino concluído com sucesso!", registro });
            }
            catch (ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = "Erro ao salvar histórico: " + ex.Message });
            }
        }
    }
}
