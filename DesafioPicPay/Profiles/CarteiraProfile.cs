using AutoMapper;
using DesafioPicPay.Models;
using DesafioPicPay.Models.Request;
using DesafioPicPay.Models.Response;

namespace DesafioPicPay.Profiles;

public class CarteiraProfile : Profile
{
    public CarteiraProfile()
    {
        CreateMap<CarteiraRequestDto, CarteiraEntidade>();
        CreateMap<CarteiraEntidade, CarteiraRequestDto>();
        CreateMap<CarteiraEntidade, CarteiraResponseDto>();
        CreateMap<CarteiraResponseDto, CarteiraEntidade>();
    }
}