using System;
using System.Threading.Tasks;

namespace TestProjectAzeriMed.Application.Contracts.Persistence
{
    public interface IUnitOfWork : IDisposable
    {
        Task Save();
    }
}
