using JC.Core.Comuns;
using JC.Core.Entities;
using JC.Core.Entities.Localizacao;

namespace JC.Core.DapperMapping;

public class PermissaoMap : BaseMap<Permissao>
{
    public PermissaoMap()
    {
        ToTable("Permissao", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdCategoria).ToColumn("Categoria_id", false);
    }
}

public class PerfilMap : BaseMap<Perfil>
{
    public PerfilMap()
    {
        ToTable("Perfil", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
    }
}

public class PermissaoPerfilMap : BaseMap<PermissaoPerfil>
{
    public PermissaoPerfilMap()
    {
        ToTable("PermissaoPerfil", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdPerfil).ToColumn("Perfil_Id");
        Map(x => x.IdPermissao).ToColumn("Permissao_Id");
    }
}

public class CategoriaMap : BaseMap<Categoria>
{
    public CategoriaMap()
    {
        ToTable("CategoriaPermissao", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);

    }
}

public class PaisMap : BaseMap<Pais>
{
    public PaisMap()
    {
        ToTable("Pais", Constantes.SCHEMA_NAME_DEFAULT);
    }
}

public class EstadoMap : BaseMap<Estado>
{
    public EstadoMap()
    {
        ToTable("Estado", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdPais).ToColumn("Pais_Id");
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
    }
}

public class CidadeMap : BaseMap<Cidade>
{
    public CidadeMap()
    {
        ToTable("Cidade", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
        Map(x => x.IdEstado).ToColumn("Estado_Id");
    }
}

public class BairroMap : BaseMap<Bairro>
{
    public BairroMap()
    {
        ToTable("Bairro", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
        Map(x => x.IdCidade).ToColumn("Cidade_Id");
    }
}

public class UsuarioMap : BaseMap<Usuario>
{
    public UsuarioMap()
    {
        ToTable("Usuario", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdArquivo).ToColumn("Arquivo_Id");
        Map(x => x.IdEndereco).ToColumn("Endereco_Id");
    }
}

public class PerfilUsuarioMap : BaseMap<PerfilUsuario>
{
    public PerfilUsuarioMap()
    {
        ToTable("PerfilUsuario", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdPerfil).ToColumn("Perfil_Id");
        Map(x => x.IdUsuario).ToColumn("Usuario_Id");
    }
}

public class FornecedorMap : BaseMap<Fornecedor>
{
    public FornecedorMap()
    {
        ToTable("Fornecedor", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
        Map(x => x.IdContato).ToColumn("Contato_Id");
        Map(x => x.IdEndereco).ToColumn("Endereco_Id");
    }
}

public class FabricanteMap : BaseMap<Fabricante>
{
    public FabricanteMap()
    {
        ToTable("Fabricante", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
    }
}

public class GrupoProdutoMap : BaseMap<GrupoProduto>
{
    public GrupoProdutoMap()
    {
        ToTable("GrupoProduto", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
    }
}

public class ProdutoMap : BaseMap<Produto>
{
    public ProdutoMap()
    {
        ToTable("Produto", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdFabricante).ToColumn("Fabricante_Id");
        Map(x => x.IdFornecedor).ToColumn("Fornecedor_Id");
        Map(x => x.IdGrupo).ToColumn("Grupo_Id");
        Map(x => x.IdArquivo).ToColumn("Arquivo_Id");
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
    }
}

public class ArquivoMap : BaseMap<Arquivo>
{
    public ArquivoMap()
    {
        ToTable("Arquivo", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
    }
}

public class ClienteMap : BaseMap<Cliente>
{
    public ClienteMap()
    {
        ToTable("Cliente", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdArquivo).ToColumn("Arquivo_Id");
        Map(x => x.IdUsuario).ToColumn("Usuario_Id");
    }
}

public class PedidoMap : BaseMap<Pedido>
{
    public PedidoMap()
    {
        ToTable("Pedido", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
        Map(x => x.IdCliente).ToColumn("Cliente_Id", false);
        Map(x => x.IdHistoricoAtual).ToColumn("HistoricoAtual_Id", false);

        Map(x => x.FormaPagamento).Ignore();
        Map(x => x.FormaPagamentoStr).ToColumn("FormaPagamento");

        Map(x => x.SituacaoAtual).Ignore();
        Map(x => x.SituacaoAtualStr).ToColumn("SituacaoAtual");

        Map(x => x.Ativo).Ignore();
    }
}

public class ItemPedidoMap : BaseMap<ItemPedido>
{
    public ItemPedidoMap()
    {
        ToTable("ItemPedido", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdPedido).ToColumn("Pedido_Id", false);
        Map(x => x.IdProduto).ToColumn("Produto_Id", false);

        Map(x => x.SituacaoAtual).Ignore();
        Map(x => x.SituacaoAtualStr).ToColumn("SituacaoAtual");

        Map(x => x.Ativo).Ignore();
    }
}
public class HistoricoPedidoMap : BaseMap<HistoricoPedido>
{
    public HistoricoPedidoMap()
    {
        ToTable("HistoricoPedido", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdPedido).ToColumn("Pedido_Id", false);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
    }
}

public class EstoqueProdutoMap : BaseMap<EstoqueProduto>
{
    public EstoqueProdutoMap()
    {
        ToTable("EstoqueProduto", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
        Map(x => x.IdProduto).ToColumn("Produto_Id", false);
    }
}

public class HistoricoEstoqueProdutoMap : BaseMap<HistoricoEstoqueProduto>
{
    public HistoricoEstoqueProdutoMap()
    {
        ToTable("HistoricoEstoqueProduto", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);
        Map(x => x.IdEstoqueProduto).ToColumn("EstoqueProduto_Id", false);
    }
}

public class CaixaMap : BaseMap<Caixa>
{
    public CaixaMap()
    {
        ToTable("Caixa", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuarioAbertura).ToColumn("UsuarioAbertura_Id", false);
        Map(x => x.IdUsuarioFechamento).ToColumn("UsuarioFechamento_Id", false);
        Map(x => x.IdUsuarioConferenciaAbertura).ToColumn("UsuarioConferenciaAbertura_Id", false);
        Map(x => x.IdUsuarioConferenciaFechamento).ToColumn("UsuarioConferenciaFechamento_Id", false);
        Map(x => x.Situacao).Ignore();
        Map(x => x.Ativo).Ignore();
        Map(x => x.SituacaoStr).ToColumn("Situacao");
    }
}
public class HistoricoCaixaMap : BaseMap<HistoricoCaixa>
{
    public HistoricoCaixaMap()
    {
        ToTable("HistoricoCaixa", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdUsuario).ToColumn("Usuario_Id", false);

    }
}

public class UnidadeMap : BaseMap<Unidade>
{
    public UnidadeMap()
    {
        ToTable("Unidade", Constantes.SCHEMA_NAME_DEFAULT);
        Map(x => x.IdBairro).ToColumn("Bairro_Id", false);
    }
}