using AutoMapper;
using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;
using DesafioPicPay.Services.Carteiras;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPicPay.Controllers;

[ApiController]
[Route("[controller]")]
public class CarteiraController : ControllerBase
{
    private readonly ICarteiraService _carteiraService;

    public CarteiraController(ICarteiraService carteiraService)
    {
        _carteiraService = carteiraService;
    }

    /// <summary>Lista todas as carteiras cadastradas.</summary>
    /// <response code="200">Lista de carteiras retornada com sucesso.</response>
    [HttpGet]
    [ProducesResponseType(typeof(List<CarteiraResponseDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<CarteiraResponseDto>>> BuscarTodas()
    {
        var response = await _carteiraService.BuscarTodasAsync();
        return Ok(response);
    }

    /// <summary>Cria uma carteira para um usuário ou lojista.</summary>
    /// <param name="carteiraRequestDto">Dados necessários para criar a carteira.</param>
    /// <response code="200">Carteira criada com sucesso.</response>
    /// <response code="400">Dados inválidos ou CPF/CNPJ ou e-mail já cadastrado.</response>
    [HttpPost]
    [ProducesResponseType(typeof(Result<CarteiraResponseDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(Result<CarteiraResponseDto>), StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CriarCarteira(CarteiraRequestDto carteiraRequestDto)
    {
        var response = await _carteiraService.CriarCarteira(carteiraRequestDto);
        if (response.EhSucesso == false) return BadRequest(response); 
        return Ok(response);
    }


}