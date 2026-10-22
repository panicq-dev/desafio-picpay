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

    [HttpGet]
    public async Task<ActionResult<List<CarteiraResponseDto>>> BuscarTodas()
    {
        var response = await _carteiraService.BuscarTodasAsync();
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> CriarEntidade(CarteiraRequestDto carteiraRequestDto)
    {
        // Ponto legal para implementar depois: verificar se o usuário já existe na hora da criação.
        var response = await _carteiraService.CriarCarteira(carteiraRequestDto);
        if (response.EhSucesso == false) return BadRequest(response);
        return Ok(response);
    }


}