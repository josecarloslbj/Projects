using Dapper;
using Dommel;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Infrastructure.Shared.Uow;
using JC.Infrastructure.Base;

namespace JC.Repository.Repositories
{
    public class ArquivoRepository : BaseRepository<Arquivo>, IArquivoRepository
    {
        private DbSession _session;
        public ArquivoRepository(DbSession session) : base(session)
        {
            _session = session;
        }

        public async Task<List<Arquivo>> Deletar(List<int> ids)
        {
            var imgs = new List<Arquivo>();

            foreach (var id in ids)
            {
                var img = await this.ObterPorId(id);
                if (img != null)
                {
                    imgs.Add(img);
                    await _session.Connection.DeleteAsync(id, _session.Transaction);
                }
            }

            return imgs;
        }

        public async Task<Arquivo> ObterPorId(int id)
        {
            return await _session.Connection.GetAsync<Arquivo>(id, _session.Transaction);
        }

        public async Task<Arquivo> Salvar(Arquivo imagem, bool? removerArquivoSemUso = false)
        {
            await SaveOrUpdate(imagem);

            if (removerArquivoSemUso.GetValueOrDefault(false))
                await this.DeletarArquivosSemUso(imagem.Id);



            return imagem;
        }

        public async Task<List<Arquivo>> DeletarArquivosSemUso(int idArquivo)
        {
            var sql = "SELECT * FROM Arquivo WHERE (Entidade IS NULL || IdEntidade IS NULL)";

            var arquivos = await this._session.Connection.QueryAsync<Arquivo>(sql, _session.Transaction);
            if (arquivos != null && arquivos.Any())
            {
                foreach (var item in arquivos)
                {
                    if (idArquivo == item.Id)
                        continue;
                    try
                    {
                        await DeleteAsync(item.Id);
                    }
                    catch (Exception ex)
                    {
                        
                    }
                }
            }

            return arquivos?.ToList();
        }
    }
}
