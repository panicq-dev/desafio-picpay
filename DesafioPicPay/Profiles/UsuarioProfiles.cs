using DesafioPicPay.Dto;
using DesafioPicPay.Models;

namespace DesafioPicPay.Profiles;
using AutoMapper;
public class UsuarioProfiles : Profile
{
    public UsuarioProfiles()
    {
        CreateMap<Usuario, UsuarioDtoResposta>();
    }
}