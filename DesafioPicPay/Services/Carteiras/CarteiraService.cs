using DesafioPicPay.Infra.Repositorios.Carteiras;
using DesafioPicPay.Infra.Result;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;
using AutoMapper;

namespace DesafioPicPay.Services.Carteiras;

public class CarteiraService : ICarteiraService
{
    private readonly IMapper _mapper;
    private readonly ICarteiraRepositorio _repositorio;

    public CarteiraService(ICarteiraRepositorio repositorio, IMapper mapper)
    {
        _repositorio = repositorio;
        _mapper = mapper;
    }

    public async Task<Result<CarteiraEntidade>> BuscarCarteira(string cpfcnpj, string email)
    {
        var usuario = await _repositorio.BuscarCpfCnpjEmail(cpfcnpj, email);
        if (usuario is null)
        {
            return Result<CarteiraEntidade>.Falha("Carteira não encontrada. Verifique o CPF, CNPJ ou e-mail.");
        }
        return Result<CarteiraEntidade>.Sucesso(usuario, "Sucesso: carteira encontrada.");

    }

    public async Task<Result<CarteiraResponseDto>> CriarCarteira(CarteiraRequestDto carteiraRequest)
    {
        if (carteiraRequest is null)
        {
            return Result<CarteiraResponseDto>.Falha("Erro no preenchimento da carteira. Verifique novamente,");
        }

        var carteira = _mapper.Map<CarteiraEntidade>(carteiraRequest);
        await _repositorio.AddAsync(carteira);
        var response = _mapper.Map<CarteiraResponseDto>(carteira);
        return Result<CarteiraResponseDto>.Sucesso(response,"Carteira criada com sucesso!");
    }

}