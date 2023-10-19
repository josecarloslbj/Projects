using Dapper;
using JC.Core.Base;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        // private DbSession _session;
        public PedidoRepository(DbSession session) : base(session)
        {
            //_session = session;
        }

        public async Task<Pedido> Criar(Pedido record)
        {
            return await SaveOrUpdate(record);
        }

        public async Task<Pedido> Obter(int id)
        {
            string sql = $@"SELECT p.*,it.* FROM Pedido p 
                            LEFT JOIN ItemPedido it ON p.Id = it.Pedido_id
                            WHERE p.id = @id";

            var pedidoDictionary = new Dictionary<int, Pedido>();
            var itemPedidoDictionary = new Dictionary<int, ItemPedido>();

            var retorno = await _session.Connection.QueryAsync<Pedido, ItemPedido, Pedido>(
               sql
               , (p, item) =>
               {
                   if (!pedidoDictionary.TryGetValue(p.Id, out var pedido))
                   {
                       pedido = p;
                       pedido.ItensPedido = new List<ItemPedido>();
                   }

                   if (item != null)
                   {
                       if (!itemPedidoDictionary.TryGetValue(item.Id, out var itemPedido))
                       {
                           if (pedido.ItensPedido == null)
                               pedido.ItensPedido = new List<ItemPedido>();

                           pedido.ItensPedido.Add(item);
                       }
                   }

                   return p;
               },
               param: new
               {
                   id = id
               }, transaction: this._session.Transaction, splitOn: "Id");

            return retorno == null || retorno?.FirstOrDefault() == null ? new Pedido() : retorno?.FirstOrDefault();

        }

        public async Task<PagedResultDTO<Pedido>> ObterPaginado(FilterDTO filters)
        {
            PagedResultDTO<Pedido> pagedResult = new PagedResultDTO<Pedido>();

            string sql = $@"SELECT
                                p.id,p.codigo,p.FormaPagamento as FormaPagamentoStr, p.SituacaoAtual as SituacaoAtualStr , p.DataCadastro, p.ValorDesconto, p.ValorPedido, p.ValorTotal, p.Usuario_id as IdUsuario
                                ,it.id, it.Pedido_id as IdPedido, it.Produto_id as IdProduto, it.Descricao ,it.Quantidade, it.SituacaoAtual  as SituacaoAtualStr , it.valorUnitario, it.ValorDesconto, it.valorTotal
                                ,u.id, u.nome
                                ,prod.*,a.*
                                FROM Pedido p 
                            INNER JOIN ItemPedido it ON p.Id = it.Pedido_id
                            INNER JOIN Usuario u ON p.usuario_id = u.id
                            INNER JOIN Produto prod ON it.produto_id = prod.id
                            LEFT JOIN Arquivo a ON prod.arquivo_id  =a.id
                            WHERE 1=1";

            if (filters.Filtros == null)
                filters.Filtros = new List<ParametrosFilters>();

            if (filters.DataInicio.HasValue)
            {
                sql += " AND  p.dataCadastro >= @dataInicio ";
            }

            if (filters.DataFim.HasValue)
            {
                sql += " AND  p.dataCadastro < @dataFim ";
            }

            if (!string.IsNullOrEmpty(filters.SortField))
                sql += $" ORDER BY p.{filters.SortField} ";

            sql += filters.SortOrder == 1 ? " ASC" : " DESC ";


            List<Pedido> pedidos = new List<Pedido>();
            List<ItemPedido> itemPedidos = new List<ItemPedido>();
            List<Usuario> usuarios = new List<Usuario>();
            List<Produto> produtos = new List<Produto>();
            List<Arquivo> arquivos = new List<Arquivo>();

            var retorno = await _session.Connection.QueryAsync<Pedido, ItemPedido, Usuario, Produto, Arquivo, Pedido>(
                sql
                , (p, item, user, prod, arq) =>
                {
                    if (p != null && !pedidos.Where(q => q.Id == p.Id).Any())
                        pedidos.Add(p);

                    if (item != null && !itemPedidos.Where(q => q.Id == item.Id).Any())
                        itemPedidos.Add(item);

                    if (user != null && !usuarios.Where(q => q.Id == user.Id).Any())
                        usuarios.Add(user);

                    if (prod != null && !produtos.Where(q => q.Id == prod.Id).Any())
                        produtos.Add(prod);

                    if (arq != null && !arquivos.Where(q => q.Id == arq.Id).Any())
                        arquivos.Add(arq);

                    return p!;
                },
               param: new
               {
                   dataInicio = filters.DataInicio.HasValue ? filters.DataInicio.Value.Date : DateTime.MinValue,
                   dataFim = filters.DataFim.HasValue ? filters.DataFim.Value.AddDays(1) : DateTime.MaxValue
               },
               transaction: this._session.Transaction, splitOn: "Id");


            foreach (var pedido in pedidos)
            {
                pedido.Usuario = (from u in usuarios where u.Id == pedido.IdUsuario select u).FirstOrDefault()!;
                pedido.ItensPedido = (from i in itemPedidos where i.IdPedido == pedido.Id select i).ToList();

                foreach (var itemPedido in pedido.ItensPedido)
                {
                    itemPedido.Produto = (from p in produtos where p.Id == itemPedido.IdProduto select p).FirstOrDefault()!;

                    if (itemPedido.Produto != null)
                        itemPedido.Produto.Arquivo = (from a in arquivos where a.Id == itemPedido?.Produto?.IdArquivo select a).FirstOrDefault()!;
                }
            }

            pagedResult.Items = pedidos;



            return pagedResult;
        }
    }
}
