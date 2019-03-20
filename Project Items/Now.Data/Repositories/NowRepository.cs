using Common.Data.Interfaces;
using Common.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Now.Data.Interfaces;
using Now.Entities.Models.Schedule;

namespace Now.Data.Repositories
{
    public class NowRepository<TContext> : Transact<TContext>, INowRepository where TContext : DbContext
    {
        private readonly TContext _context;

        public NowRepository(TContext context) : base(context)
        {
            _context = context;
            Shifts = new EntityBaseRepository<TContext, Shift>(_context);
            ShiftDetails = new EntityBaseRepository<TContext, ShiftDetail>(_context);
            Schedules = new EntityBaseRepository<TContext, Schedule>(_context);
        }

        public IEntityBaseRepository<Schedule> Schedules { get; set; }
        public IEntityBaseRepository<Shift> Shifts { get; set; }
        public IEntityBaseRepository<ShiftDetail> ShiftDetails { get; set; }
    }
}