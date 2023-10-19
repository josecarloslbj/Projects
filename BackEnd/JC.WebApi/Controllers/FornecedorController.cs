using JC.Application.Comuns;
using JC.Application.Services;
using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;
using JC.Infrastructure.Shared.Attributes;
using JC.WebApi.Filters;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace JC.Api.Controllers
{
    [Authorize]
    [ConectarBanco]
    [ApiController]
    [RespostaBase]
    [Route("[controller]")]
    public class FornecedorController : Controller
    {
        private readonly IFornecedorService _fornecedorService;

        public FornecedorController(IFornecedorService fornecedorService)
        {
            _fornecedorService = fornecedorService;
        }

        [Route("salvar")]
        [HttpPost]
        [ProducesResponseType(typeof(RespostaBaseViewModel<FornecedorDTO>), (int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Salvar([FromBody] FornecedorDTO fornecedor)
        {
            return Json(await _fornecedorService.Salvar(fornecedor));
        }

        [Route("{id:}")]
        [HttpGet]
        [ProducesResponseType(typeof(RespostaBaseViewModel<FornecedorDTO>), (int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> ObterFornecedor(int id)
        {
            return Json(await _fornecedorService.Obter(id));
        }

        [Route("{id:}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Deletar(int id)
        {
            await _fornecedorService.Deletar(id);
            return Json(null);
        }

        [Route("buscar-fornecedores")]
        [HttpPost]
        [ProducesResponseType(typeof(RespostaBaseViewModel<PagedResultDTO<FornecedorDTO>>), (int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> BuscarFornecedores([FromBody] FornecedorFilterDTO filters)
        {
            return Json(await _fornecedorService.ObterPaginado(filters));
        }
    }
}
