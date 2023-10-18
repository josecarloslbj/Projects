using AutoMapper;
using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Entities;
using JC.Core.Entities.Localizacao;

namespace JC.Core.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CriarMapsDomain();
    }

    private void CriarMapsDomain()
    {
        CreateMap<Categoria, CategoriaDTO>().ReverseMap();
        CreateMap<Perfil, PerfilDTO>().ReverseMap();
        CreateMap<Permissao, PermissaoDTO>().ReverseMap();
        CreateMap<Usuario, UsuarioDTO>().ReverseMap();
        CreateMap<PerfilUsuario, PerfilUsuarioDTO>().ReverseMap();
        CreateMap<Fornecedor, FornecedorDTO>().ReverseMap();
        CreateMap<Fabricante, FabricanteDTO>().ReverseMap();
        CreateMap<GrupoProduto, GrupoProdutoDTO>().ReverseMap();
        CreateMap<Produto, ProdutoDTO>().AfterMap((p, pDTO) =>
        {
            pDTO.Situacao = pDTO.Ativo ? "Ativo" : "Inativo";
        }).ReverseMap();
        CreateMap<Arquivo, ArquivoDTO>().ReverseMap();
        CreateMap<Pedido, PedidoDTO>().ReverseMap();
        CreateMap<ItemPedido, ItemPedidoDTO>().ReverseMap();
        CreateMap<Cliente, ClienteDTO>().ReverseMap();
        CreateMap<EstoqueProduto, EstoqueProdutoDTO>().ReverseMap();
        CreateMap<Caixa, CaixaDTO>()
            .ReverseMap()
            .AfterMap((pDTO, p) =>
            {
                p.Situacao = pDTO.Situacao;
                p.DataAbertura = pDTO.DataAbertura;
                p.IdUsuarioAbertura = pDTO.IdUsuarioAbertura;
            });
        CreateMap<Pais, PaisDTO>().ReverseMap();
        CreateMap<Estado, EstadoDTO>().ReverseMap();
        CreateMap<Cidade, CidadeDTO>().ReverseMap();
        CreateMap<Bairro, BairroDTO>().ReverseMap();
        CreateMap<Unidade, UnidadeDTO>().ReverseMap();

        //CreateMap<HistoricoCaixa, HistoricoCaixaDTO>().ReverseMap();

        CreateMap<PagedResultDTO<Usuario>, PagedResultDTO<UsuarioDTO>>().ReverseMap();
        CreateMap<PagedResultDTO<Fornecedor>, PagedResultDTO<FornecedorDTO>>().ReverseMap();
        CreateMap<PagedResultDTO<Fabricante>, PagedResultDTO<FabricanteDTO>>().ReverseMap();
        CreateMap<PagedResultDTO<GrupoProduto>, PagedResultDTO<GrupoProdutoDTO>>().ReverseMap();
        CreateMap<PagedResultDTO<Produto>, PagedResultDTO<ProdutoDTO>>().ReverseMap();
        CreateMap<PagedResultDTO<Pedido>, PagedResultDTO<PedidoDTO>>().ReverseMap();
        CreateMap<PagedResultDTO<ItemPedido>, PagedResultDTO<ItemPedidoDTO>>().ReverseMap();
        CreateMap<PagedResultDTO<Cliente>, PagedResultDTO<ClienteDTO>>().ReverseMap();

        CreateMap<PagedResultDTO<Unidade>, PagedResultDTO<UnidadeDTO>>().ReverseMap();
    }
}
