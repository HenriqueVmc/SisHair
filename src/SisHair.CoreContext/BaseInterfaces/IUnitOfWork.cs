using System.Threading.Tasks;

namespace SisHair.CoreContext.BaseInterfaces
{
    public interface IUnitOfWork
    {        
        Task<bool> Commit();
    }
}
