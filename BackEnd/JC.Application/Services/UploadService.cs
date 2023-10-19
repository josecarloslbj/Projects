using AutoMapper;
using JC.Core.Comuns;
using JC.Core.Dtos;
using JC.Core.Entities;
using JC.Core.Repositories;

using Microsoft.AspNetCore.Http;
using System.Diagnostics.CodeAnalysis;

namespace JC.Application.Services
{
    [ExcludeFromCodeCoverageAttribute]
    public class UploadService : IUploadService
    {
        private readonly IMapper _mapper;
        private readonly IArquivoRepository _arquivoRepository;
        public UploadService(IMapper mapper, IArquivoRepository arquivoRepository)
        {
            _mapper = mapper;
            _arquivoRepository = arquivoRepository;
        }

        public async Task<ArquivoDTO> DeleteFilesUnused()
        {
            throw new NotImplementedException();
        }

        public async Task<ArquivoDTO> GetFile(int idArquivo)
        {
            var arquivo = await _arquivoRepository.GetAsync(idArquivo);
            return _mapper.Map<ArquivoDTO>(arquivo);
        }

        public async Task<ArquivoDTO> UploadAsync(IFormFile file)
        {
            try
            {
                var caminhoArquivo = await UploadUtils.UploadAsync(file);

                Arquivo arquivo = new Arquivo();
                arquivo.Nome = file.FileName;
                arquivo.Diretorio = caminhoArquivo;
                arquivo.SubDiretorio = caminhoArquivo
                    .Replace(file.FileName, string.Empty)
                    .Replace(Constantes.DIRETORIO_IMAGENS, string.Empty)
                    .Replace("\\", string.Empty);

                arquivo.Extensao = file.FileName.Split('.').Last();

                var _arquivo = await _arquivoRepository.Salvar(arquivo);

                return _mapper.Map<ArquivoDTO>(_arquivo);
            }
            catch (Exception ex)
            {
                throw new Exception($"Não foi possível salvar o arquivo do diretório padrão. Ex.{ex.Message}");
            }
        }
    }
}
