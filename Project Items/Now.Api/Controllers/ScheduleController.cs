using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Now.Api.Business;
using Now.Api.Model;
using Now.Data.Interfaces;
using Now.Entities.Models.Schedule;
using Now.Entities.Models.ViewModels;
using OfficeOpenXml.FormulaParsing.Excel.Functions.DateTime;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Now.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ScheduleController : ControllerBase
    {
        // GET: /<controller>/
        private readonly INowRepository _nowRepository;


        public ScheduleController(INowRepository nowRepository)
        {
            _nowRepository = nowRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_nowRepository.Schedules.GetAll());
        }

        [HttpPost]
        public JsonResult AddShift([FromBody] ProposedShift proposedShift)
        {
            if (proposedShift is null)
            {
                return new JsonResult("Invalid Data");
            }

            //Need to add validation for modelstate
            var shift = _nowRepository.Shifts.GetById(proposedShift.ShiftId);

            //var dateTimeOffset = proposedShift.dateTime.ToUniversalTime();

            //Need more testing on timezone conversion
            var timeSpan = proposedShift.dateTime - shift.StartTime;

            //var sampledate = new DateTimeOffset();

            var proposedSchedule = new Schedule()
            {
                Employee = proposedShift.EmployeeId.ToString(),
                ScheduleId = Guid.NewGuid(),
                ShiftId = shift.ShiftId,
                StartTime = shift.StartTime.AddDays(timeSpan.Days + 1),
                EndTime = shift.EndTime.AddDays(timeSpan.Days + 1)
            };

            var conflict = _nowRepository.Schedules.GetAll()
                .Where(x =>
                    (x.Employee == proposedShift.EmployeeId.ToString()) &&
                    (
                        (x.StartTime <= proposedSchedule.EndTime && x.StartTime >= proposedSchedule.StartTime) ||
                        (x.EndTime <= proposedSchedule.EndTime && x.StartTime >= proposedSchedule.EndTime) ||
                        (x.EndTime >= proposedSchedule.EndTime && x.StartTime <= proposedSchedule.StartTime)
                    )
                );

            if (conflict.Any())
            {
                //provide more details on conflict

                return new JsonResult("Conflict");
            }
            else
            {
                //static operating Hours
                var operatingHours = new List<ScheduleVM>()
                {
                    new ScheduleVM()
                    {
                        StartTime = new DateTimeOffset(2019, 2, 1, 8, 0, 0, TimeSpan.Zero),
                        EndTime = new DateTimeOffset(2019, 2, 1, 17, 0, 0, TimeSpan.Zero),
                        ShiftId = new Guid("00000000-0000-0000-0000-000000000100"),
                        isOpen = true,
                        isWholeDay = true,
                        timeZone = "China Standard Time"
                    },
                    new ScheduleVM()
                    {
                        StartTime = new DateTimeOffset(2019, 2, 2, 8, 0, 0, TimeSpan.Zero),
                        EndTime = new DateTimeOffset(2019, 2, 2, 17, 0, 0, TimeSpan.Zero),
                        ShiftId = new Guid("00000000-0000-0000-0000-000000000100"),
                        isOpen = true,
                        isWholeDay = true,
                        timeZone = "China Standard Time"
                    },
                    new ScheduleVM()
                    {
                        StartTime = new DateTimeOffset(2019, 2, 3, 8, 0, 0, TimeSpan.Zero),
                        EndTime = new DateTimeOffset(2019, 2, 3, 17, 0, 0, TimeSpan.Zero),
                        ShiftId = new Guid("00000000-0000-0000-0000-000000000100"),
                        isOpen = true,
                        isWholeDay = true,
                        timeZone = "China Standard Time"
                    },
                    new ScheduleVM()
                    {
                        StartTime = new DateTimeOffset(2019, 2, 4, 8, 0, 0, TimeSpan.Zero),
                        EndTime = new DateTimeOffset(2019, 2, 4, 17, 0, 0, TimeSpan.Zero),
                        ShiftId = new Guid("00000000-0000-0000-0000-000000000100"),
                        isOpen = true,
                        isWholeDay = true,
                        timeZone = "China Standard Time"
                    },
                    new ScheduleVM()
                    {
                        StartTime = new DateTimeOffset(2019, 2, 5, 8, 0, 0, TimeSpan.Zero),
                        EndTime = new DateTimeOffset(2019, 2, 5, 17, 0, 0, TimeSpan.Zero),
                        ShiftId = new Guid("00000000-0000-0000-0000-000000000100"),
                        isOpen = true,
                        isWholeDay = true,
                        timeZone = "China Standard Time"
                    },
                    new ScheduleVM()
                    {
                        StartTime = new DateTimeOffset(2019, 2, 5, 0, 0, 0, TimeSpan.Zero),
                        EndTime = new DateTimeOffset(2019, 2, 6, 0, 0, 0, TimeSpan.Zero),
                        ShiftId = new Guid("00000000-0000-0000-0000-000000000100"),
                        isOpen = false,
                        isWholeDay = true,
                        timeZone = "China Standard Time"
                    },
                    new ScheduleVM()
                    {
                        StartTime = new DateTimeOffset(2019, 2, 6, 0, 0, 0, TimeSpan.Zero),
                        EndTime = new DateTimeOffset(2019, 2, 7, 0, 0, 0, TimeSpan.Zero),
                        ShiftId = new Guid("00000000-0000-0000-0000-000000000100"),
                        isOpen = false,
                        isWholeDay = true,
                        timeZone = "China Standard Time"
                    }
                };

                var schedules = _nowRepository.Schedules.GetAll().Where(x => x.Employee == proposedSchedule.Employee).ToList();

                if (schedules.Any())
                {

                    #region PreviousSchedule

                    //get previous and next schedule base on proposed schedule
                    var previousSchedule = schedules.Where(x => x.EndTime < proposedSchedule.StartTime).OrderByDescending(x => x.EndTime).FirstOrDefault();

                    //calculate startRange
                    if (previousSchedule != null)
                    {
                        var StartTimeSpan = proposedSchedule.StartTime - previousSchedule.EndTime;

                        if (StartTimeSpan.TotalHours >= 24)
                        {
                            var previousOperatingHours = operatingHours.Where(x => x.EndTime < proposedSchedule.StartTime).OrderByDescending(x => x.EndTime).FirstOrDefault();

                            if (previousOperatingHours.isOpen && !previousOperatingHours.isWholeDay)
                            {
                                var startTimeSpan = (proposedSchedule.StartTime - previousOperatingHours.EndTime) / 2;
                                proposedSchedule.StartRange = proposedSchedule.StartTime.AddTicks(startTimeSpan.Ticks + 1);
                            }
                            else
                            {
                                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(previousOperatingHours.timeZone);
                                previousOperatingHours.EndTime = TimeZoneInfo.ConvertTimeToUtc(previousOperatingHours.EndTime.DateTime, timeZone);

                                var startTimeSpan = (previousOperatingHours.EndTime - proposedSchedule.StartTime) / 2;
                                proposedSchedule.StartRange = proposedSchedule.StartTime.AddTicks(startTimeSpan.Ticks + 1);
                            }
                        }
                        else
                        {
                            StartTimeSpan = StartTimeSpan / 2;
                            proposedSchedule.StartRange = proposedSchedule.StartTime.AddTicks(StartTimeSpan.Ticks + 1);
                            previousSchedule.EndRange = proposedSchedule.StartTime.AddTicks(StartTimeSpan.Ticks);
                        }
                    }
                    else
                    {
                        var previousOperatingHours = operatingHours.Where(x => x.EndTime < proposedSchedule.StartTime).OrderByDescending(x => x.EndTime).FirstOrDefault();

                        if (previousOperatingHours.isOpen && !previousOperatingHours.isWholeDay)
                        {
                            var StartTimeSpan = (proposedSchedule.StartTime - previousOperatingHours.EndTime) / 2;
                            proposedSchedule.StartRange = proposedSchedule.StartTime.AddTicks(StartTimeSpan.Ticks + 1);
                        }
                        else
                        {
                            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(previousOperatingHours.timeZone);
                            previousOperatingHours.EndTime = TimeZoneInfo.ConvertTimeToUtc(previousOperatingHours.EndTime.DateTime, timeZone);

                            var startTimeSpan = (previousOperatingHours.EndTime - proposedSchedule.StartTime) / 2;
                            proposedSchedule.StartRange = proposedSchedule.StartTime.AddTicks(startTimeSpan.Ticks + 1);
                        }
                    }

                    #endregion

                    #region NextSchedule

                    var nextSchedule = schedules.Where(x => x.StartTime > proposedSchedule.EndTime).OrderByDescending(x => x.StartTime).FirstOrDefault();

                    //calculating endRange
                    if (nextSchedule != null)
                    {
                        var EndTimeSpan = nextSchedule.StartTime - proposedSchedule.EndTime;

                        if (EndTimeSpan.TotalHours >= 24)
                        {
                            var nextOperatingHours = operatingHours.Where(x => x.StartTime > proposedSchedule.EndTime).OrderByDescending(x => x.StartTime).FirstOrDefault();

                            if (nextOperatingHours.isOpen && !nextOperatingHours.isWholeDay)
                            {
                                var endTimeSpan = (nextOperatingHours.StartTime - proposedSchedule.EndTime) / 2;
                                proposedSchedule.EndRange = proposedSchedule.EndTime.AddTicks(endTimeSpan.Ticks);
                            }
                            else
                            {
                                var timeZone = TimeZoneInfo.FindSystemTimeZoneById(nextOperatingHours.timeZone);
                                nextOperatingHours.StartTime = TimeZoneInfo.ConvertTimeToUtc(nextOperatingHours.StartTime.DateTime, timeZone);

                                var endTimeSpan = (nextOperatingHours.StartTime - proposedSchedule.EndTime) / 2;
                                proposedSchedule.EndRange = proposedSchedule.EndTime.AddTicks(endTimeSpan.Ticks);
                            }

                        }
                        else
                        {
                            EndTimeSpan = EndTimeSpan / 2;
                            proposedSchedule.EndRange = proposedSchedule.EndTime.AddTicks(EndTimeSpan.Ticks);
                            nextSchedule.StartRange = proposedSchedule.EndTime.AddTicks(EndTimeSpan.Ticks + 1);
                        }
                    }
                    else
                    {
                        var nextOperatingHours = operatingHours.Where(x => x.StartTime > proposedSchedule.EndTime).OrderByDescending(x => x.StartTime).FirstOrDefault();

                        if (nextOperatingHours.isOpen && !nextOperatingHours.isWholeDay)
                        {
                            var EndTimeSpan = nextOperatingHours.StartTime - proposedSchedule.EndTime;
                            proposedSchedule.EndRange = proposedSchedule.EndTime.AddTicks(EndTimeSpan.Ticks);
                        }
                        else
                        {
                            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(nextOperatingHours.timeZone);
                            nextOperatingHours.StartTime = TimeZoneInfo.ConvertTimeToUtc(nextOperatingHours.StartTime.DateTime, timeZone);

                            var endTimeSpan = (nextOperatingHours.StartTime - proposedSchedule.EndTime) / 2;
                            proposedSchedule.EndRange = proposedSchedule.EndTime.AddTicks(endTimeSpan.Ticks);
                        }
                    }

                    #endregion

                }
                else
                {
                    if (operatingHours.Any())
                    {
                        #region PreviousOperatingHours

                        var previousOperatingHours = operatingHours.Where(x => x.EndTime < proposedSchedule.StartTime).OrderByDescending(x => x.EndTime).FirstOrDefault();

                        if (previousOperatingHours.isOpen && !previousOperatingHours.isWholeDay)
                        {
                            //calculate the startRange of proposedSchedule
                            var startTimeSpan = ((proposedSchedule.StartTime - previousOperatingHours.EndTime) / 2);
                            proposedSchedule.StartRange = proposedSchedule.StartTime.AddTicks(startTimeSpan.Ticks + 1);
                        }
                        else
                        {
                            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(previousOperatingHours.timeZone);
                            previousOperatingHours.EndTime = TimeZoneInfo.ConvertTimeToUtc(previousOperatingHours.EndTime.DateTime, timeZone);

                            var startTimeSpan = (previousOperatingHours.EndTime - proposedSchedule.StartTime) / 2;
                            proposedSchedule.StartRange = proposedSchedule.StartTime.AddTicks(startTimeSpan.Ticks + 1);
                        }

                        #endregion

                        #region nextOperatingHours

                        var nextOperatingHours = operatingHours.Where(x => x.StartTime > proposedSchedule.EndTime).OrderBy(x => x.StartTime.DateTime).FirstOrDefault();

                        if (nextOperatingHours.isOpen && !nextOperatingHours.isWholeDay)
                        {
                            //calculate the endRange of proposedSchedule
                            var endTimeSpan = ((nextOperatingHours.StartTime - proposedSchedule.EndTime) / 2);
                            proposedSchedule.EndRange = proposedSchedule.EndTime.AddTicks(endTimeSpan.Ticks);
                        }
                        else
                        {
                            var timeZone = TimeZoneInfo.FindSystemTimeZoneById(nextOperatingHours.timeZone);
                            nextOperatingHours.StartTime = TimeZoneInfo.ConvertTimeToUtc(nextOperatingHours.StartTime.DateTime, timeZone);

                            var endTimeSpan = (nextOperatingHours.StartTime - proposedSchedule.EndTime) / 2;
                            proposedSchedule.EndRange = proposedSchedule.EndTime.AddTicks(endTimeSpan.Ticks);
                        }

                        #endregion
                    }
                    else
                    {
                        return new JsonResult("No Operating Hours yet!");
                    }

                }

                _nowRepository.Schedules.Add(proposedSchedule);
                _nowRepository.SaveAsync();

                return new JsonResult(proposedSchedule);

            }


        }

        [HttpPost]
        public JsonResult ProcessRawLogs([FromBody] Param Employee)
        {
            //static rawLogs
            var rawLogs = new List<LogsVm>()
            {
                new LogsVm
                {
                    EmployeeId = "00000000-0000-0000-0000-000000000001",
                    LogTime = new DateTimeOffset(2019, 2, 2, 0, 0, 0, TimeSpan.Zero)
                },
                new LogsVm
                {
                    EmployeeId = "00000000-0000-0000-0000-000000000001",
                    LogTime = new DateTimeOffset(2019, 2, 3, 19, 30, 0, TimeSpan.Zero)
                }

            };

            var Schedules = _nowRepository.Schedules.GetAll().Where(x => x.Employee == Employee.EmployeeId
                                                        && (x.StartTime.Date <= Employee.DateTime.Date
                                                            && x.EndRange.Date >= Employee.DateTime.Date)).ToList();

            var ProcessedLogs = from a in rawLogs
                                 join b in Schedules
                                 on a.EmployeeId equals b.Employee
                                 where a.EmployeeId == Employee.EmployeeId && 
                                 (a.LogTime >= b.StartRange && a.LogTime <= b.EndRange)
                                 select a;

            var minLog = ProcessedLogs.Min(x => x.LogTime);
            var maxLog = ProcessedLogs.Max(x => x.LogTime);

            var timeLogDetails = new TimeLogDetailsVM()
            {
                EmployeeId = Employee.EmployeeId,
                StartTime = minLog,
                EndTime = maxLog
            };

            return new JsonResult(timeLogDetails);
        }

        [HttpPost]
        public JsonResult AddShifts([FromBody] List<Shift> shift)
        {
            return new JsonResult(_nowRepository.Schedules.GetAll());
        }

        [HttpPost]
        public JsonResult AddShifting([FromBody] List<Shift> shift)
        {
            return new JsonResult(_nowRepository.Schedules.GetAll());
        }

    }
}
