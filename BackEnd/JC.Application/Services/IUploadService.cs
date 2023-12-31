﻿using JC.Core.Dtos;
using Microsoft.AspNetCore.Http;

namespace JC.Application.Services
{
    public interface IUploadService
    {
        Task<ArquivoDTO> UploadAsync(IFormFile arquivo);

        Task<ArquivoDTO> GetFile(int idArquivo);

        Task<ArquivoDTO> DeleteFilesUnused();
    }
}
