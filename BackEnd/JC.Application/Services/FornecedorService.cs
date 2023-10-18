using AutoMapper;
using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;
using JC.Core.Entities;
using JC.Core.Repositories;
using JC.Domain.Interfaces.Services;
using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services 
{
    [ExcludeFromCodeCoverageAttribute]
    public class FornecedorService : IFornecedorService
    {
        private readonly IMapper _mapper;
        private readonly IFornecedorRepository _fornecedorRepository;
        public FornecedorService(IMapper mapper, IFornecedorRepository fornecedorRepository)
        {
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
        }

        public async Task<FornecedorDTO> Salvar(FornecedorDTO fornecedor)
        {
            var fornecedorModel = await _fornecedorRepository.Salvar(_mapper.Map<Fornecedor>(fornecedor));

            return _mapper.Map<FornecedorDTO>(fornecedorModel);
        }

        public async Task<PagedResultDTO<FornecedorDTO>> ObterPaginado(FornecedorFilterDTO filters)
        {
            var retorno = await _fornecedorRepository.ObterPaginado(filters);
            return _mapper.Map<PagedResultDTO<FornecedorDTO>>(retorno);
        }

        public async Task<FornecedorDTO> Obter(int id)
        {
            var campos = new List<dynamic>()
                {
                    new {title = "Contexto", value = "TESTE"}                };


            //var msg = new MensagemSlackDTO() { DestinatarioSlack = DestinatarioSlack.TESTE, Titulo = $" Fornecedor criado", Fields = campos, CorMensagem = CorMensagemSlack.DANGER };
            //AgendarNotificacaoSlackST(msg);

            var retorno = await _fornecedorRepository.Obter(id);
            return _mapper.Map<FornecedorDTO>(retorno);
        }

        public async Task Deletar(int id)
        {
            await _fornecedorRepository.Deletar(id);
        }

        //[Hangfire.Queue("cadastro")]
        //public static void NotificarSlackST(MensagemSlackDTO msg, string corAlternativa = null) => SlackUtils.Publicar(msg, corAlternativa);

        //public static void AgendarNotificacaoSlackST(MensagemSlackDTO msg, string corAlternativa = null)
        //{
        //    Hangfire.BackgroundJob.Schedule(() => NotificarSlackST(msg, corAlternativa), TimeSpan.FromSeconds(30));
        //}

    }
}
