using Common.Data.Interfaces;
using Now.Entities.Models.Schedule;

namespace Now.Data.Interfaces
{
    public interface INowRepository : ITransact
    {
        IEntityBaseRepository<Schedule> Schedules { get; }
        IEntityBaseRepository<Shift> Shifts { get; }
        IEntityBaseRepository<ShiftDetail> ShiftDetails { get; }
    }
}