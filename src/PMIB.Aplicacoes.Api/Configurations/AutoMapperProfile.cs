using AutoMapper;
using PMIB.Aplicacoes.Api.Dtos;
using PMIB.Core.Business.Models;

namespace PMIB.Aplicacoes.Api.Configurations;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<CategoriaDto, Categoria>().ReverseMap();
        CreateMap<ProdutoDto, Produto>().ReverseMap();
        CreateMap<VendedorDto, Vendedor>().ReverseMap();
        CreateMap<ClienteDto, Cliente>().ReverseMap();

    }
}
