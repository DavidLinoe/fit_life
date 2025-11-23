using fit_life.Data;
using fit_life.Models;
using fit_life.Services;
using fit_life.DTOs.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using fit_life.DTOs.Auth;

namespace fit_life.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly DataContext _context;
        private readonly TokenService _tokenService;

        public AuthController(DataContext context, TokenService tokenService)
        {
            _context = context;
            _tokenService = tokenService;
        }

        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegisterDto request)
        {
            if (await _context.UsuarioTable.AnyAsync(u => u.Email == request.Email))
                return BadRequest("Email já cadastrado.");

            var usuario = new Usuario(request.Name, request.Email);
            usuario.DefinirSenha(request.Senha); 

            _context.UsuarioTable.Add(usuario);
            await _context.SaveChangesAsync();

            return Ok("Usuário registrado com sucesso!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto request)
        {

            var usuario = await _context.UsuarioTable.FirstOrDefaultAsync(u => u.Email == request.Email);

            if (usuario == null || !usuario.VerificarSenha(request.Senha))
                return Unauthorized("Email ou senha inválidos.");

            var token = _tokenService.GerarToken(usuario);
            return Ok(new { token = token, usuario = usuario.Name });
        }
    }



}