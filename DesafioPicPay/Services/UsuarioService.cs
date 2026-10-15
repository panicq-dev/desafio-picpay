using AutoMapper;
using DesafioPicPay.Dto;
using DesafioPicPay.Models;

namespace DesafioPicPay.Services;

public class UsuarioService
{
    private readonly IMapper _mapper;
    public List<Usuario> Usuarios = new();

    public UsuarioService(IMapper mapper)
    {
        _mapper = mapper;
    }

    public UsuarioDtoResposta? Create(string nome, string cpf, string email, string senha, string tipoUsuario)
    {
        
        if (tipoUsuario != "1" && tipoUsuario != "2")
        {
            // Tipo de usuário inválido
            return null;
        }

        var usuario = new Usuario()
        {
            NomeCompleto = nome,
            Cpf = cpf,
            Email = email,
            Senha = senha,
            TipoUsuario = tipoUsuario == "1"
                ? TipoUsuario.Usuario
                : TipoUsuario.Lojista
        };

        Usuarios.Add(usuario);
        var usuarioDtoResposta = _mapper.Map<UsuarioDtoResposta>(usuario);
        
        return usuarioDtoResposta;
    }

    public List<Usuario> Listar()
    {
        if (Usuarios.Count == 0) return null;
        return Usuarios;
    }

    public Usuario BuscarPorCpf(string cpf)
    {

        var usuario = Usuarios.SingleOrDefault(u => u.Cpf == cpf);
        return usuario;
    }
    
}