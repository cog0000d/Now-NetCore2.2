using System;
using System.Threading.Tasks;

namespace Now.Data.Interfaces
{
    public interface ILogTransact : IDisposable
    {
        void Save();
        Task SaveAsync();
    }
}