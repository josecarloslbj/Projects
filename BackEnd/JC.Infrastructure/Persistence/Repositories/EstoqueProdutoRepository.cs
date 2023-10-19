using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class EstoqueProdutoRepository : BaseRepository<EstoqueProduto>, IEstoqueProdutoRepository
    {
        private readonly IHistoricoEstoqueProdutoRepository _historicoEstoqueProdutoRepository;

        public EstoqueProdutoRepository(DbSession session, 
            IHistoricoEstoqueProdutoRepository historicoEstoqueProdutoRepository) : base(session)
        {
            _historicoEstoqueProdutoRepository = historicoEstoqueProdutoRepository;
        }

        public async Task<EstoqueProduto> Salvar(EstoqueProduto record)
        {
            var retorno = await this.SaveOrUpdate(record);

            if (record.Historico!.Any())            
                foreach (var hist in record.Historico!)                
                    await _historicoEstoqueProdutoRepository.SaveOrUpdate(hist);

            return retorno;
        }
    }
}
