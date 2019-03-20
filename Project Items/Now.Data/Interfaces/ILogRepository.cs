using Now.Entities.Models.Time;

namespace Now.Data.Interfaces
{
    public interface ILogRepository : ILogTransact
    {
        IEntityRepository<Log> Logs { get; }
    }
}