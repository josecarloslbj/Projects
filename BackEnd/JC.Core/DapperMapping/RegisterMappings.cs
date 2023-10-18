using Dapper.FluentMap;
using Dapper.FluentMap.Configuration;
using Dapper.FluentMap.Dommel;
using System.Reflection;

namespace JC.Core.DapperMapping
{
    public static class RegisterMappings
    {
        private static IEnumerable<Type> GetMappingTypes() =>
         Assembly.GetExecutingAssembly()
             .GetTypes()
             .Where(t => t.BaseType != null &&
                         t.BaseType.GetInterfaces().Contains(typeof(IRegisterFluentMapper)));

        public static void Register()
        {


            var registerFluentMapper = Assembly.GetExecutingAssembly()
          .GetTypes()
          .Where(t => t.BaseType != null &&
                      t.BaseType.GetInterfaces().Contains(typeof(IRegisterFluentMapper)));


            FluentMapper.Initialize(config =>
            {
                foreach (var type in GetMappingTypes())
                {
                    var addMapMethod = typeof(FluentMapConfiguration).GetMethod("AddMap");
                    var target = addMapMethod.MakeGenericMethod(type.BaseType.GenericTypeArguments[0]);
                    var mapping = Activator.CreateInstance(type);
                    target.Invoke(config, new[] { mapping });

                    config.ForDommel();
                }
            });


            //FluentMapper.Initialize(config =>
            //{
            //    config.AddMap(new CategoriaMap());
            //    config.AddMap(new PerfilMap());
            //    config.AddMap(new PermissaoMap());
            //    config.AddMap(new PermissaoPerfilMap());
            //    config.AddMap(new UsuarioMap());
            //    config.AddMap(new PerfilUsuarioMap());
            //    config.AddMap(new EstadoMap());
            //    config.AddMap(new CidadeMap());
            //    config.AddMap(new FornecedorMap());
            //    config.AddMap(new FabricanteMap());
            //    config.AddMap(new ProdutoMap());
            //    config.AddMap(new GrupoProdutoMap());
            //    config.AddMap(new ArquivoMap());
            //    config.AddMap(new ClienteMap());
            //    config.AddMap(new PedidoMap());
            //    config.AddMap(new ItemPedidoMap());
            //    config.AddMap(new HistoricoPedidoMap());
            //    config.ForDommel();
            //});
        }
    }
}