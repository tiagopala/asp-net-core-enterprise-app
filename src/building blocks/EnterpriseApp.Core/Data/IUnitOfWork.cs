using System.Threading.Tasks;

namespace EnterpriseApp.Core.Data
{
    public interface IUnitOfWork
    {
        Task<bool> Commit();
    }
}
