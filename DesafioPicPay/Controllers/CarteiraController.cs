using AutoMapper;
using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Services.Carteiras;
using Microsoft.AspNetCore.Mvc;

namespace DesafioPicPay.Controllers;

[ApiController]
[Route("[controller]")]
public class CarteiraController : ControllerBase
{
    private readonly CarteiraService _carteiraService;
    private readonly CarteiraRepositorio _carteiraRepositorio;
    private IMapper _mapper;

    public CarteiraController(CarteiraService carteiraService, CarteiraRepositorio carteiraRepositorio, IMapper mapper)
    {
        _carteiraService = carteiraService;
        _carteiraRepositorio = carteiraRepositorio;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> CriarUsuario(CarteiraRequest carteiraRequest)
    {
        var carteira = _mapper.Map<CarteiraEntidade>(carteiraRequest);
        await _carteiraRepositorio.AddAsync(carteira);
        return Ok();
    }
}