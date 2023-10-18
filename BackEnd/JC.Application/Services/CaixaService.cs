using AutoMapper;
using JC.Core.Comuns;
using JC.Core.Dtos;
using JC.Core.Entities;
using JC.Core.Repositories;

namespace JC.Application.Services
{
    public class CaixaService : ICaixaService
    {
        private readonly IMapper _mapper;
        private readonly ICaixaRepository _caixaRepository;
        private readonly IUsuarioRepository _usuarioRepository;
        public CaixaService(IMapper mapper,
            ICaixaRepository caixaRepository,
            IUsuarioRepository usuarioRepository)
        {
            _mapper = mapper;
            _caixaRepository = caixaRepository;
            _usuarioRepository = usuarioRepository;
        }

        public async Task<CaixaDTO> AbrirCaixa(CaixaDTO record)
        {
            record.DataAbertura = DateTime.Now;
            record.IdUsuarioAbertura = Contexto.Atual.IdUsuario;
            record.Situacao = SituacaoCaixa.ABERTO;

            return await Salvar(record);
        }

        public async Task<CaixaDTO> FecharCaixa(CaixaDTO record)
        {
            var caixa = await _caixaRepository.Obter(record.Id);

            record.DataFechamento = DateTime.Now;
            record.IdUsuarioFechamento = Contexto.Atual.IdUsuario;
            record.Situacao = SituacaoCaixa.FECHADO;          
            return await Salvar(record);
        }

        public async Task<CaixaDTO> ObterCaixaPorDia(DateTime? data)
        {
            if (!data.HasValue)
                data = DateTime.Now;

            var retorno = await _caixaRepository.ObterCaixaDia(data.Value);
            if (retorno == null)
                return new CaixaDTO { Situacao = SituacaoCaixa.AGUARDANDO_ABERTURA };

            var c = _mapper.Map<CaixaDTO>(retorno);

            List<Int32> idUsuarios = new List<int> {
                retorno.IdUsuarioAbertura,
                retorno.IdUsuarioConferenciaAbertura.GetValueOrDefault(0),
                retorno.IdUsuarioConferenciaAbertura.GetValueOrDefault(0),
                retorno.IdUsuarioConferenciaFechamento.GetValueOrDefault(0)
            };

            if (idUsuarios.Any())
            {
                idUsuarios = idUsuarios.Distinct().ToList();
                var usuarios = await _usuarioRepository.SelectAsync(q => idUsuarios.Contains(q.Id));

                c.NomeUsuarioAbertura = (from u in usuarios
                                         where u.Id == retorno.IdUsuarioAbertura
                                         select u).FirstOrDefault()?.Nome;

                if (retorno.IdUsuarioFechamento.HasValue)
                    c.NomeUsuarioFechamento = (from u in usuarios
                                               where u.Id == retorno.IdUsuarioFechamento
                                               select u).FirstOrDefault()?.Nome;

                if (retorno.IdUsuarioConferenciaAbertura.HasValue)
                    c.NomeUsuarioConferenciaAbertura = (from u in usuarios
                                                        where u.Id == retorno.IdUsuarioConferenciaAbertura
                                                        select u).FirstOrDefault()?.Nome;

                if (retorno.IdUsuarioConferenciaFechamento.HasValue)
                    c.NomeUsuarioConferenciaFechamento = (from u in usuarios
                                                          where u.Id == retorno.IdUsuarioConferenciaFechamento
                                                          select u).FirstOrDefault()?.Nome;
            }
            return c;
        }

        public async Task<CaixaDTO> Salvar(CaixaDTO record)
        {
            var retorno = await _caixaRepository.Salvar(_mapper.Map<Caixa>(record));
            return _mapper.Map<CaixaDTO>(retorno);
        }
    }
}
