using System;
using System.Collections.Generic;
using System.Text;
using Now.Entities.Models.Schedule;

namespace Now.Data.DataContexts.Schedule
{
public class ScheduleDataConfiguration
    {
        private readonly ScheduleDbContext _context;

        public ScheduleDataConfiguration(ScheduleDbContext context)
        {
            _context = context;
        }

        #region _Shifts

        public IEnumerable<Shift> GetShifts()
        {
            var shifts = new List<Shift>();
            try
            {
                //shifts.Add(new Shift
                //{
                //    ShiftId = new Guid("00000000-0000-0000-0000-000000000001"),
                //    ShiftName = "8:00 AM to 5:00 PM (UTC)",
                //    ShiftDescription = "Normal Office Hours starting at 8AM",
                //    SiteId = new Guid("00000000-0000-0000-0000-000000000001"),
                //    StartTime = new DateTimeOffset(1, 1, 2, 8, 0, 0, TimeSpan.Zero),
                //    EndTime = new DateTimeOffset(1, 1, 2, 17, 0, 0, TimeSpan.Zero),
                //    Duration = new DateTimeOffset(1, 1, 2, 17, 0, 0, TimeSpan.Zero) -
                //            new DateTimeOffset(1, 1, 2, 8, 0, 0, TimeSpan.Zero),
                //    AddedBy = new Guid("00000000-0000-0000-0000-000000000001"),
                //    AddedDate = DateTimeOffset.UtcNow
                //});
                //shifts.Add(new Shift
                //{
                //    ShiftId = new Guid("00000000-0000-0000-0000-000000000002"),
                //    ShiftName = "9:00 AM to 6:00 PM (UTC)",
                //    ShiftDescription = "Normal Office Hours starting at 9AM",
                //    SiteId = new Guid("00000000-0000-0000-0000-000000000001"),
                //    StartTime = new DateTimeOffset(1, 1, 2, 9, 0, 0, TimeSpan.Zero),
                //    EndTime = new DateTimeOffset(1, 1, 2, 18, 0, 0, TimeSpan.Zero),
                //    Duration = new DateTimeOffset(1, 1, 2, 18, 0, 0, TimeSpan.Zero) -
                //               new DateTimeOffset(1, 1, 2, 9, 0, 0, TimeSpan.Zero),
                //    AddedBy = new Guid("00000000-0000-0000-0000-000000000001"),
                //    AddedDate = DateTimeOffset.UtcNow
                //});
                //shifts.Add(new Shift
                //{
                //    ShiftId = new Guid("00000000-0000-0000-0000-000000000003"),
                //    ShiftName = "5 Days Straight",
                //    ShiftDescription = "5 Straight Working Days",
                //    SiteId = new Guid("00000000-0000-0000-0000-000000000001"),
                //    StartTime = new DateTimeOffset(1, 1, 2, 8, 0, 0, TimeSpan.Zero),
                //    EndTime = new DateTimeOffset(1, 1, 6, 17, 0, 0, TimeSpan.Zero),
                //    Duration = new DateTimeOffset(1, 1, 6, 17, 0, 0, TimeSpan.Zero) -
                //               new DateTimeOffset(1, 1, 2, 8, 0, 0, TimeSpan.Zero),
                //    AddedBy = new Guid("00000000-0000-0000-0000-000000000001"),
                //    AddedDate = DateTimeOffset.UtcNow
                //});
                //shifts.Add(new Shift
                //{
                //    ShiftId = new Guid("00000000-0000-0000-0000-000000000004"),
                //    ShiftName = "12:00am - 5:00am",
                //    ShiftDescription = "First Shift",
                //    SiteId = new Guid("00000000-0000-0000-0000-000000000001"),
                //    StartTime = new DateTimeOffset(1, 1, 2, 0, 0, 0, TimeSpan.Zero),
                //    EndTime = new DateTimeOffset(1, 1, 2, 5, 0, 0, TimeSpan.Zero),
                //    Duration = new DateTimeOffset(1, 1, 2, 5, 0, 0, TimeSpan.Zero) -
                //                new DateTimeOffset(1, 1, 2, 0, 0, 0, TimeSpan.Zero),
                //    AddedBy = new Guid("00000000-0000-0000-0000-000000000001"),
                //    AddedDate = DateTimeOffset.UtcNow
                //});
                //shifts.Add(new Shift
                //{
                //    ShiftId = new Guid("00000000-0000-0000-0000-000000000005"),
                //    ShiftName = "8:00am - 2:00pm",
                //    ShiftDescription = "Second Shift",
                //    SiteId = new Guid("00000000-0000-0000-0000-000000000001"),
                //    StartTime = new DateTimeOffset(1, 1, 2, 8, 0, 0, TimeSpan.Zero),
                //    EndTime = new DateTimeOffset(1, 1, 2, 14, 0, 0, TimeSpan.Zero),
                //    Duration = new DateTimeOffset(1, 1, 2, 14, 0, 0, TimeSpan.Zero) -
                //                new DateTimeOffset(1, 1, 2, 8, 0, 0, TimeSpan.Zero),
                //    AddedBy = new Guid("00000000-0000-0000-0000-000000000001"),
                //    AddedDate = DateTimeOffset.UtcNow
                //});
                shifts.Add(new Shift
                {
                    ShiftId = new Guid("00000000-0000-0000-0000-000000000006"),
                    ShiftName = "5:00pm - 11:00pm",
                    ShiftDescription = "sample Shift",
                    SiteId = new Guid("00000000-0000-0000-0000-000000000001"),
                    StartTime = new DateTimeOffset(1, 1, 2, 9, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(1, 1, 2, 15, 0, 0, TimeSpan.Zero),
                    Duration = new DateTimeOffset(1, 1, 2, 15, 0, 0, TimeSpan.Zero) -
                                new DateTimeOffset(1, 1, 2, 9, 0, 0, TimeSpan.Zero),
                    AddedBy = new Guid("00000000-0000-0000-0000-000000000001"),
                    AddedDate = DateTimeOffset.UtcNow
                });
                return shifts;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        #region _Schedules

        public IEnumerable<Entities.Models.Schedule.Schedule> GetSchedules()
        {
            var schedules = new List<Entities.Models.Schedule.Schedule>();
            try
            {
                schedules.Add(new Entities.Models.Schedule.Schedule
                {

                });
 
                return schedules;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion
    }
}
