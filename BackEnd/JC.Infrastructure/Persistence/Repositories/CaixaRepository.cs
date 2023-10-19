using Dapper;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Base;
using JC.Infrastructure.Shared.Uow;

namespace JC.Repository.Repositories
{
    public class CaixaRepository : BaseRepository<Caixa>, ICaixaRepository
    {
        private readonly IHistoricoCaixaRepository _historicoCaixaRepository;

        public CaixaRepository(DbSession session,
            IHistoricoCaixaRepository historicoCaixaRepository) : base(session)
        {
            _historicoCaixaRepository = historicoCaixaRepository;
        }

        public async Task<Caixa> Obter(int id)
        {
            string sql = $@"SELECT Id
                        ,DataAbertura
                        ,DataFechamento
                        ,ValorInicial
                        ,ValorFinal
                        ,UsuarioAbertura_Id as IdUsuarioAbertura
                        ,UsuarioFechamento_Id as IdUsuarioFechamento
                        ,UsuarioConferenciaAbertura_Id as IdUsuarioConferenciaAbertura
                        ,UsuarioConferenciaFechamento_Id as IdUsuarioConferenciaFechamento
                        ,Situacao as SituacaoStr
                        ,ObservacaoAbertura
                        ,ObservacaoFechamento
                        FROM  Caixa WHERE id = @id  ";


            var retorno = await _session.Connection.QueryAsync<Caixa>(sql, new
            {
                id
            });

            return retorno.FirstOrDefault()!;
        }

        public async Task<Caixa> ObterCaixaDia(DateTime data)
        {
            var sql = @$"SELECT Id
                        ,DataAbertura
                        ,DataFechamento
                        ,ValorInicial
                        ,ValorFinal
                        ,UsuarioAbertura_Id as IdUsuarioAbertura
                        ,UsuarioFechamento_Id as IdUsuarioFechamento
                        ,UsuarioConferenciaAbertura_Id as IdUsuarioConferenciaAbertura
                        ,UsuarioConferenciaFechamento_Id as IdUsuarioConferenciaFechamento
                        ,Situacao as SituacaoStr
                        ,ObservacaoAbertura
                        ,ObservacaoFechamento
                        FROM Caixa WHERE DataAbertura >= @dataInicio && DataAbertura < @dataFinal ";

            var retorno = await _session.Connection.QueryAsync<Caixa>(sql, new
            {
                dataInicio = data.Date,
                dataFinal = data.Date.AddDays(1)
            });

            return retorno.FirstOrDefault()!;
        }

        public async Task<Caixa> Salvar(Caixa record)
        {
            var caixa = await SaveOrUpdate(record);

            if (record.HistoricoCaixas != null && record.HistoricoCaixas.Any())
            {
                foreach (var item in record.HistoricoCaixas)
                {
                    await _historicoCaixaRepository.SaveOrUpdate(item);
                }
            }
            return caixa;
        }
    }
}
