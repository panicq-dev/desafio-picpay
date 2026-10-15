using System.Text.Json;
using DesafioPicPay.Models;
using DesafioPicPay.Services;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPicPay.Controllers;
[ApiController]
[Route("[controller]")]
public class UsuarioController : ControllerBase
{
    private static readonly HttpClient Client = new HttpClient();
    private static UsuarioService _usuarioService;

    [HttpPost]
    public IActionResult CreateUsuarios(string nome, string cpf, string email, string senha, string tipoUsuario)
    {
        var resposta = _usuarioService.Create(nome, cpf, email, senha, tipoUsuario);
        if (resposta is null) return BadRequest("Tipo de usuário inválido. Valores disponíveis: '1' e '2'.");
        return Ok($"Usuário criado com sucesso {resposta}");
    }

    public IActionResult ListarUsuarios()
    {
        var usuarios = _usuarioService.Listar();
        return Ok(usuarios);
    }

    // Teste
    [HttpGet("testeAutorizacao")]
    public async Task<IActionResult> GetAutorizacao()
    {
        var request = await Client.GetStringAsync("https://util.devi.tools/api/v2/authorize");
        using var json = JsonDocument.Parse(request);
        var authorization = json.RootElement.GetProperty("data").GetProperty("authorization").GetBoolean();
        return Ok(authorization);
    }
}