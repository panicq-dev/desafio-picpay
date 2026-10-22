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

    [HttpPost]
    public async Task<IActionResult> Transferir (TransferenciaRequestDto transferenciaDto)
    {
        var response = await _transferenciaService.ExecutarAsync(transferenciaDto);
        if (response.EhSucesso == false) return BadRequest(response);
        return Ok(response);
    }

}