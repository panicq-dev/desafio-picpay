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
    private readonly ICarteiraRepositorio _carteiraRepositorio;
    private readonly ICarteiraService _carteiraService;
    private readonly IMapper _mapper;

    public CarteiraController(ICarteiraRepositorio carteiraRepositorio, IMapper mapper, ICarteiraService carteiraService)
    {
        _carteiraRepositorio = carteiraRepositorio;
        _mapper = mapper;
        _carteiraService = carteiraService;
    }

    [HttpPost]
    public async Task<IActionResult> CriarEntidade(CarteiraRequestDto carteiraRequestDto)
    {
        // Ponto legal para implementar depois: verificar se o usuário já existe na hora da criação.
        var carteira = _mapper.Map<CarteiraEntidade>(carteiraRequestDto);
        await _carteiraRepositorio.AddAsync(carteira);
        var response = _mapper.Map<CarteiraResponseDto>(carteira);
        return Ok(Result<CarteiraResponseDto>.Sucesso(response,"Carteira criada com sucesso!"));
    }


}