
using JC.WebApi.Filters;
using JC.Application.Comuns;
using JC.Core.Base;
using JC.Core.Dtos;
using JC.Core.Dtos.Filters;

using Microsoft.AspNetCore.Mvc;
using System.Net;

using JC.Infrastructure.Shared.Attributes;
using JC.Application.Services;

namespace JC.Api.Controllers
{
    //[Authorize]
    [ConectarBanco]
    [ApiController]
    [RespostaBase]
    [Route("[controller]")]
    public class FabricanteController : Controller
    {
        private readonly IFabricanteService _fabricanteService;

        public FabricanteController(IFabricanteService fabricanteService)
        {
            _fabricanteService = fabricanteService;
        }

        [Route("salvar")]
        [HttpPost]
        [ProducesResponseType(typeof(RespostaBaseViewModel<FabricanteDTO>), (int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Salvar([FromBody] FabricanteDTO fornecedor)
        {
            return Json(await _fabricanteService.Salvar(fornecedor));
        }

        [Route("{id:}")]
        [HttpGet]
        [ProducesResponseType(typeof(RespostaBaseViewModel<FabricanteDTO>), (int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> ObterFornecedor(int id)
        {
            return Json(await _fabricanteService.Obter(id));
        }

        [Route("{id:}")]
        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> Deletar(int id)
        {
            await _fabricanteService.Deletar(id);
            return Json(null);
        }

        [Route("buscar-fabricantes")]
        [HttpPost]
        [ProducesResponseType(typeof(RespostaBaseViewModel<PagedResultDTO<FabricanteDTO>>), (int)HttpStatusCode.OK)]
        public virtual async Task<IActionResult> BuscarFabricantes([FromBody] FabricanteFilterDTO filters)
        {
            return Json(await _fabricanteService.ObterPaginado(filters));
        }
    }
}
