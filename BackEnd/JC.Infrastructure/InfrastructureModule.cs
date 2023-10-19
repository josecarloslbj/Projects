using DotNet.DynamicInjector;
using JC.Core.Entities.Localizacao;
using JC.Core.Repositories;
using JC.Infrastructure.Persistence.Repositories;
using JC.Infrastructure.Shared.Uow;
using JC.Repository.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JC.Infrastructure
{
    public static class InfrastructureModule
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services, string conexao)
        {
            services
                .AddDbSession(conexao)
                .AddRepositories();
            //.AddMessageBus();

            return services;
        }

        public static IServiceCollection AddDbSession(this IServiceCollection services, string _con)
        {
            services.AddScoped<DbSession>(x =>
            {
                return new DbSession(_con);
            });

            services.AddTransient<IUnitOfWork, UnitOfWork>();

            return services;
        }

        private static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            //services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            //services.AddScoped<IPermissaoRepository, PermissaoRepository>();
            //services.AddScoped<IUsuarioRepository, UsuarioRepository>();


            services.AddScoped<ILocalizacaoRepository<Pais>, LocalizacaoRepository<Pais>>();
            services.AddScoped<ILocalizacaoRepository<Estado>, LocalizacaoRepository<Estado>>();
            services.AddScoped<ILocalizacaoRepository<Cidade>, LocalizacaoRepository<Cidade>>();
            services.AddScoped<ILocalizacaoRepository<Bairro>, LocalizacaoRepository<Bairro>>();

            var assembly = AssemblyConfig.LoadAssembly("JC.Infrastructure.dll");
            var exportedTypes = assembly.ExportedTypes.Where(x => x.FullName!
                                                          .StartsWith("JC.", StringComparison.CurrentCulture)).ToList();

            foreach (var exportedType in exportedTypes)
            {
                var classesTypes = exportedType.GetInterfaces().Where(q => q.Name == $"I{exportedType.Name}");

                foreach (var @interface in classesTypes)
                {
                    var nomeInterface = @interface.Name;

                    if (!nomeInterface.Contains("ILocalizacaoRepository") &&  (!nomeInterface.Contains("IBaseRepository")))
                        services.AddScoped(@interface, exportedType);

                }
            }


            return services;
        }

        //private static IServiceCollection AddMessageBus(this IServiceCollection services)
        //{
        //    services.AddScoped<IMessageBusService, RabbitMqService>();

        //    return services;
        //}
    }
}
