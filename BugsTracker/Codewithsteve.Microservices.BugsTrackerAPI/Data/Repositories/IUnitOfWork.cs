using System;
using System.Threading.Tasks;

namespace Myairops.Tech.Test.Microservices.ClientDatabase.API.Data.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBugRepository Bugs { get; }
        IClientRepository Clients { get; }

        int Complete();
        Task<int> CompleteAsync();
    }
}
