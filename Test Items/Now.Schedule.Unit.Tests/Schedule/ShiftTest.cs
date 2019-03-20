using System;
using System.Collections.Generic;
using Itenso.TimePeriod;
using Moq;
using Now.Api.Controllers;
using Now.Data.Interfaces;
using Now.Entities.Models.Schedule;
using Xunit;
using Xunit.Abstractions;
using ShiftDetailType = Now.Entities.Models.Schedule.ShiftDetailType;

namespace Now.Schedule.Unit.Tests.Schedule
{
    public class ShiftTest
    {

        INowRepository _nowRepository;
        ITestOutputHelper output;
        ShiftController _controller;


        public ShiftTest(ITestOutputHelper output)
        {
            this.output = output;
            _controller = new ShiftController(_nowRepository);
        }


        [Fact]
        public void Test1()
        {
            Mock<INowRepository> mockShift = new Mock<INowRepository>();


            var type = new List<ShiftDetailType>
            {
                new ShiftDetailType
                {
                    ShiftDetailTypeId = Guid.NewGuid(),
                    Name = "Regular",
                    Description = "Regular Shift",
                    Active = 1
                },
                new ShiftDetailType
                {
                    ShiftDetailTypeId = Guid.NewGuid(),
                    Name = "Lunch",
                    Description = "test1",
                    Active = 0
                },
                new ShiftDetailType
                {
                    ShiftDetailTypeId = Guid.NewGuid(),
                    Name = "Break",
                    Description = "test1",
                    Active = 0
                }
            };

            var shift = new List<Shift>
            {
                new Shift
                {
                    ShiftId = Guid.NewGuid(),
                    ShiftName = "8 - 5",
                    ShiftDescription = "test1",
                    StartTime = new DateTimeOffset(1900, 1, 1, 8, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(1900, 1, 1, 17, 0, 0, TimeSpan.Zero)
                },
                new Shift
                {
                    ShiftId = Guid.NewGuid(),
                    ShiftName = "9 - 6",
                    ShiftDescription = "test1",
                    StartTime = new DateTimeOffset(1900, 1, 1, 9, 0, 0, TimeSpan.Zero),
                    EndTime = new DateTimeOffset(1900, 1, 1, 18, 0, 0, TimeSpan.Zero)
                }
            };

            var startTime = shift.Find(x => x.ShiftName == "8 - 5").StartTime.UtcDateTime;
            var endTime = shift.Find(x => x.ShiftName == "8 - 5").EndTime.UtcDateTime;
            var sdt = new Week(DateTimeOffset.UtcNow.UtcDateTime).FirstDayOfWeek;
            sdt = sdt.Add(startTime.TimeOfDay);
            var Duration = new DateDiff(startTime, endTime);
            var edt = sdt.AddSeconds(Duration.Seconds);
            var test = Duration.ElapsedSeconds;

            var schedule = new List<Entities.Models.Schedule.Schedule>();


            for (var i = 0; i < 7; i++)
            {
                schedule.Add(
                    new Entities.Models.Schedule.Schedule
                    {
                        ScheduleId = Guid.NewGuid(),
                        ShiftId = shift.Find(x => x.ShiftName == "8 - 5").ShiftId,
                        Employee = "Marc",
                        StartRange =
                            i == 0
                                ? new DateTimeOffset()
                                : new DateTimeOffset(
                                    schedule[i - 1].EndTime.UtcDateTime.AddSeconds(
                                        new DateDiff(schedule[i - 1].EndTime.UtcDateTime, sdt.AddDays(i)).Seconds / 2),
                                    TimeSpan.Zero),
                        StartTime = new DateTimeOffset(sdt.AddDays(i), TimeSpan.Zero),
                        EndTime = new DateTimeOffset(sdt.AddDays(i).AddSeconds(Duration.Seconds), TimeSpan.Zero),
                        EndRange = new DateTimeOffset(sdt.AddDays(i).AddSeconds(Duration.Seconds), TimeSpan.Zero)
                    });
                if (i > 0)
                {
                    var a = schedule[i - 1].EndRange;
                    var diff = new DateDiff(a.UtcDateTime, schedule[i].StartTime.UtcDateTime);
                    var b = diff.Seconds / 2;
                    var c = schedule[i - 1].EndRange.AddSeconds(b);
                    schedule[i - 1].EndRange = new DateTimeOffset(schedule[i - 1].EndRange.AddSeconds(b).UtcDateTime,
                        TimeSpan.Zero);
                }
                ;
            }

            output.WriteLine("Shifts Available are:");
            foreach (var s in shift)
                output.WriteLine("{0} is between {1} and {2}.  Duration is {3}",
                    s.ShiftName,
                    s.StartTime.UtcDateTime,
                    s.EndTime.UtcDateTime, new DateDiff(s.StartTime.UtcDateTime,
                        s.EndTime.UtcDateTime));

            output.WriteLine("");
            output.WriteLine("{0} has the following schedule for the week {1} of {2}.",
                schedule[0].Employee, new Week(schedule[0].StartTime.UtcDateTime).WeekOfYear,
                schedule[0].StartTime.Year);
            foreach (var sched in schedule)
                output.WriteLine("{0} is from {1} to {2}. Range is from {3} to {4}.",
                    sched.StartTime.DayOfWeek,
                    sched.StartTime.UtcDateTime,
                    sched.EndTime.UtcDateTime,
                    sched.StartRange.UtcDateTime,
                    sched.EndRange.UtcDateTime);


            Assert.Equal(3, type.Count);
            Assert.Equal(2, shift.Count);
        }

        [Fact]
        public void GetByDateTest()
        {

        }
    }
}
