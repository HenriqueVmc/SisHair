using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SisHair.FuncionarioContext.Application.Events;
using SisHair.FuncionarioContext.Application.Events.Interfaces;
using SisHair.FuncionarioContext.Application.Handlers;
using SisHair.FuncionarioContext.Application.Queries;
using SisHair.FuncionarioContext.Domain.Interfaces;
using SisHair.FuncionarioContext.Infra.Data;

namespace SisHair.FuncionarioContext.Infra.CrossCutting.IoC
{
    public static class FuncionarioDependencyInjection
    {
        public static void RegisterServices(IServiceCollection services)
        {            
            // Application
            services.AddScoped<IFuncionarioQueries, FuncionarioQueries>();
            services.AddScoped<IFuncionarioCommandHandler, FuncionarioCommandHandler>();
            services.AddScoped<ICadastrarFuncionarioEventHandler, CadastrarFuncionarioEventHandler>();

            // Infra - Data
            services.AddScoped<IFuncionarioRepository, FuncionarioRepository>();
            services.AddScoped<IFuncionarioService, FuncionarioService>();
         
            services.AddDbContext<FuncionarioDataContext, FuncionarioDataContext>
            (
                options => options.UseSqlServer // TODO: Pegar Connection string de application.json
                (
                    @"Data Source=(localdb)\MSSQLLocalDB;" +
                    @"Initial Catalog=DbSisHair;" +
                    @"Integrated Security=True;" +
                    @"Connect Timeout=30;" +
                    @"Encrypt=False;" +
                    @"TrustServerCertificate=False;" +
                    @"ApplicationIntent=ReadWrite;" +
                    @"MultiSubnetFailover=False"
                )
            );
        }
    }
}
