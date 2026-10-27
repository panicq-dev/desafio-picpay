using AutoMapper;
using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;
using DesafioPicPay.Services.Carteiras;
using Microsoft.AspNetCore.Mvc;
using DesafioPicPay.Services.Transferencias;
namespace DesafioPicPay.Controllers;

[ApiController]
[Route("[controller]")]
public class TransferenciaController : ControllerBase
{

    private readonly ITransferenciaService _transferenciaService;
    public TransferenciaController(ITransferenciaService transferenciaService)
    {
        _transferenciaService = transferenciaService;
    }

    /// <summary>Transfere saldo de uma carteira de usuário para outra carteira.</summary>
    /// <param name="transferenciaDto">Valor e identificadores das carteiras de origem e destino.</param>
    /// <response code="200">Transferência realizada com sucesso.</response>
    /// <response code="400">Dados inválidos ou transferência não autorizada pelas regras de negócio.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<TransferenciaResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<TransferenciaResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> Transferir (TransferenciaRequestDto transferenciaDto)
    {
        var response = await _transferenciaService.ExecutarAsync(transferenciaDto);
        if (response.EhSucesso == false) return BadRequest(response);
        return Ok(response);
    }

}