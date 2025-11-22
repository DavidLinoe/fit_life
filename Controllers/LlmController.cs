using fit_life.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace fit_life.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class LlmController : ControllerBase
    {
        private readonly ILlmService _llmService;

        public LlmController(ILlmService llmService)
        {
            _llmService = llmService;
        }

        [HttpPost("chat")]
        public async Task<IActionResult> ChatLivre([FromBody] string pergunta)
        {
            var resposta = await _llmService.ObterResposta(pergunta);
            return Ok(new { mensagem = resposta });
        }


        [HttpPost("gerar-treino")]
        public async Task<IActionResult> CriarTreino([FromBody] PedidoTreinoRequest request)
        {
            if (request.DiasPorSemana < 1 || request.DiasPorSemana > 7)
                return BadRequest("Dias por semana deve ser entre 1 e 7.");

            var treino = await _llmService.GerarTreino(
                request.Perfil,
                request.Objetivo,
                request.DiasPorSemana
            );

            return Ok(new { treino_sugerido = treino });
        }
    }

    public class PedidoTreinoRequest
    {
        public string Perfil { get; set; } 
        public string Objetivo { get; set; } 
        public int DiasPorSemana { get; set; }
    }
}