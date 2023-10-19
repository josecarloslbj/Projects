using DotNet.DynamicInjector;
using JC.Application.Services;
using JC.Core.Entities.Localizacao;
using JC.Core.Repositories;
using JC.Infrastructure.Shared.JwtUtils;
using JC.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JC.Application;

public static class ApplicationModule
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services
            .AddApplicationServices();

        return services;
    }

    private static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {
        services.AddScoped<IJwtUtils, JwtUtils>();
        services.AddScoped<IUsuarioService, UsuarioService>();
        services.AddScoped<IPerfilService, PerfilService>();
        services.AddScoped<ICategoriaPermissaoService, CategoriaPermissaoService>();
        services.AddScoped<ICategoriaService, CategoriaService>();
        services.AddScoped<IPermissaoService, PermissaoService>();

        services.AddScoped<ILocalizacaoService, LocalizacaoService>();
        services.AddScoped<IFornecedorService, FornecedorService>();
        services.AddScoped<IFabricanteService, FabricanteService>();
        services.AddScoped<IProdutoService, ProdutoService>();
        services.AddScoped<IGrupoProdutoService, GrupoProdutoService>();
        services.AddScoped<IUploadService, UploadService>();
        services.AddScoped<IPedidoService, PedidoService>();
        services.AddScoped<IItemPedidoService, ItemPedidoService>();
        services.AddScoped<IClienteService, ClienteService>();
        services.AddScoped<IUnidadeService, UnidadeService>();

        //Usando Tributo [Service]
        //services.AddScoped<IEstoqueProdutoService, EstoqueProdutoService>();
        //services.AddScoped<ICaixaService, CaixaService>();



        var assembly = AssemblyConfig.LoadAssembly("JC.Application.dll");
        Type type = typeof(JC.Infrastructure.Shared.Attributes.Service);
        var exportedServicesTypes = assembly.ExportedTypes.Where(x => x.IsInterface == true)
            .Where(x => x.CustomAttributes.Where(q => q.AttributeType == type).Any())
            .ToList();


        var interfacesServicos = exportedServicesTypes;


        foreach (var item in interfacesServicos)
        {
            foreach (var s in assembly.ExportedTypes)
            {
                if (item.IsAssignableFrom(s))
                {
                    var interfaceName = item.Name;
                    var classeName = s.Name;

                    services.AddScoped(item, s);
                    break;
                }
            }
        }


        return services;
    }
}
