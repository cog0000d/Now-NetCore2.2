using Microsoft.EntityFrameworkCore;
using Now.Data.Interfaces;
using Now.Entities.Models.Time;

namespace Now.Data.Repositories
{
    public class LogRepository<TContext> : LogTransact<TContext>, ILogRepository where TContext : DbContext
    {
        private readonly TContext _context;

        public LogRepository(TContext context) : base(context)
        {
            _context = context;
            Logs = new EntityRepository<TContext, Log>(_context);
        }

        public IEntityRepository<Log> Logs { get; set; }
    }
}