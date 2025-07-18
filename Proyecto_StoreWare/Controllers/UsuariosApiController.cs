using Microsoft.AspNetCore.Identity.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Proyecto_StoreWare.Interfaces;
using Proyecto_StoreWare.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Proyecto_StoreWare.Controllers.Api
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsuariosApiController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        private readonly IConfiguration _configuration;

        // Solo inyectamos el servicio, no el contexto directo
        public UsuariosApiController(IUsuarioService usuarioService, IConfiguration configuration)
        {
            _usuarioService = usuarioService;
            _configuration = configuration;
        }
        /*
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Usuario>>> GetAllUsuarios()
        {
            var usuarios = await _usuarioService.GetAllUsuariosAsync(); // Nuevo método a implementar
            return Ok(usuarios);
        }
        */
        [HttpGet("{id}")]
        public async Task<ActionResult<Usuario>> GetUsuarioById(int id)
        {
            var usuario = await _usuarioService.GetUsuarioByIdAsync(id);
            return usuario != null ? Ok(usuario) : NotFound();
        }

        [HttpPost]
        public async Task<ActionResult<Usuario>> PostUsuario(Usuario usuario)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var result = await _usuarioService.RegisterAsync(usuario);
            return result ? CreatedAtAction(nameof(GetUsuarioById), new { id = usuario.Id }, usuario) : BadRequest("Error al registrar");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUsuario(int id, Usuario usuario)
        {
            if (id != usuario.Id || !ModelState.IsValid)
                return BadRequest();

            var result = await _usuarioService.UpdatePerfilAsync(usuario);
            return result ? NoContent() : NotFound();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUsuario(int id)
        {
            var result = await _usuarioService.DeleteUsuarioAsync(id);
            return result ? NoContent() : NotFound();
        }

        [HttpPost("login")]
        public async Task<ActionResult<Usuario>> Login([FromBody] LoginRequest request)
        {
            var usuario = await _usuarioService.LoginAsync(request.Email, request.Password);

            if (usuario == null)
                return Unauthorized("Credenciales incorrectas"); 

            return Ok(usuario); 
        }
    }

    public class LoginRequest
    {
        public required string Email { get; set; }
        public required string Password { get; set; } 
    }
}