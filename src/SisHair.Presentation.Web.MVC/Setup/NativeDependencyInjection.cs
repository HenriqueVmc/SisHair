using MediatR;
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
            services.AddScoped<FuncionarioDataContext>();
        }
    }
}
