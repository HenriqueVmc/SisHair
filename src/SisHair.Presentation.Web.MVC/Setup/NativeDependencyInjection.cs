using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SisHair.CoreContext.BaseInterfaces;
using SisHair.CoreContext.Mediator;
using SisHair.FuncionarioContext.Application.Commands;
using SisHair.FuncionarioContext.Application.Events;
using SisHair.FuncionarioContext.Application.Handlers;
using SisHair.FuncionarioContext.Application.Queries;
using SisHair.FuncionarioContext.Domain.Interfaces;
using SisHair.FuncionarioContext.Infra.Data;

namespace SisHair.Presentation.Web.MVC.Setup
{
    public static class NativeDependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {            
            // Application
            services.AddScoped<IFuncionarioQueries, FuncionarioQueries>();
            
            services.AddScoped<IMediatorHandler, MediatorHandler>();
            
            services.AddScoped<IRequestHandler<CadastrarFuncionarioCommand, ICommandResult>, IFuncionarioCommandHandler>();
            services.AddScoped<IRequestHandler<AtualizarFuncionarioCommand, ICommandResult>, IFuncionarioCommandHandler>();
            services.AddScoped<IRequestHandler<RemoverFuncionarioCommand, ICommandResult>, IFuncionarioCommandHandler>();

            services.AddScoped<INotificationHandler<CadastrarFuncionarioEvent>, FuncionarioEventHandler>();

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
