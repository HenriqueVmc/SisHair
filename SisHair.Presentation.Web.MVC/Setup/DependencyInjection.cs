using Microsoft.Extensions.DependencyInjection;
using SisHair.FuncionarioContext.Infra.CrossCutting.IoC;

namespace SisHair.Presentation.Web.MVC.Setup
{
    public static class DependencyInjection
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            FuncionarioDependencyInjection.RegisterServices(services);
        }
    }
}
