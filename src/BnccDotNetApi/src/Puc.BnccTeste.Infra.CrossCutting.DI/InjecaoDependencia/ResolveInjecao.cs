using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Puc.BnccTeste.Domain.ObjetoValor;
using Puc.BnccTeste.Infra.Data.Interface;
using Puc.BnccTeste.Infra.Data.Repositorio;
using Puc.BnccTeste.Service.Interface;
using Puc.BnccTeste.Service.Service;

namespace Puc.BnccTeste.Infra.CrossCutting.DI.InjecaoDependencia
{
    public static class ResolveInjecao
    {
        public static IServiceCollection InjecaoDependencia(this IServiceCollection services)
        {           
            
            #region Repositorio
            services.AddScoped<IBnccMatematicaEfRepositorio, BnccMatematicaEfRepositorio>();
            services.AddScoped<IBnccLinguaPortuguesaEfRepositorio, BnccLinguaPortuguesaEfRepositorio>();
            services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
            #endregion

            #region Services
            services.AddScoped<IBnccMatematicaEfService, BnccMatematicaEfService>();
            services.AddScoped<IBnccLinguaPortuguesaEfService, BnccLinguaPortuguesaEfService>();
            services.AddScoped<IUsuarioService, UsuarioService>();
            services.AddScoped<IContractorResult, ContractorResult>();
            #endregion

            return services;
        }
    }
}
