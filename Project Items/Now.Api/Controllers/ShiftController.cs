using System;
using System.Collections.Generic;
using System.Linq;
using Common.Data.Utilities;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Now.Data.Interfaces;
using Now.Entities.Models.Schedule;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Now.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : ControllerBase
    {
        private readonly INowRepository _nowRepository;

        public ShiftController(INowRepository nowRepository)
        {
            _nowRepository = nowRepository;
        }

        [HttpGet]
        public JsonResult Get()
        {
            return new JsonResult(_nowRepository.Shifts.GetAll());
        }

        [HttpGet("{dateTimeOffset}")]
        public JsonResult Get(DateTimeOffset dateTimeOffset)
        {
            return new JsonResult(JsonConvert.SerializeObject(dateTimeOffset));
        }

        [HttpGet("{startDateTimeOffset}/{endDateTimeOffset}")]
        public JsonResult Get(DateTimeOffset startDateTimeOffset, DateTimeOffset endDateTimeOffset)
        {
            var dateTime = new List<Shift>()
            {
                new Shift(){
                    SiteId = Guid.NewGuid(),
                    ShiftId = Guid.NewGuid(),
                    ShiftName = "Test",
                    ShiftDescription = "Test Description",
                    ShiftDetails = new List<ShiftDetail>(),
                    StartTime = DateTimeOffset.UtcNow,
                    EndTime = DateTimeOffset.UtcNow,
                    AddedBy = Guid.NewGuid(),
                    AddedDate = DateTimeOffset.UtcNow,
                    ModifiedBy = Guid.NewGuid(),
                    ModifiedDate = DateTimeOffset.UtcNow,

                    IsDeleted = false}
            };

            dateTime[0].MemoryConsumption = MeasureSize.GetMemoryConsumption(dateTime[0]);

            return new JsonResult(dateTime.ToList());
            //return Json(JsonConvert.SerializeObject(dateTime));
        }

        [HttpPost]
        public JsonResult Post([FromBody] Shift proposedShift)
        {
            try
            {
                //if (_nowRepository.Save())
                _nowRepository.Shifts.Add(new Shift()
                {
                    ShiftId = Guid.NewGuid(),
                    ShiftName = proposedShift.ShiftName,
                    ShiftDescription = proposedShift.ShiftDescription,
                    StartTime = new DateTimeOffset(1, 1, 2,
                        proposedShift.StartTime.Hour,
                        proposedShift.StartTime.Minute,
                        proposedShift.StartTime.Second,
                        proposedShift.StartTime.Offset).UtcDateTime,
                    EndTime = new DateTimeOffset(1, 1, 2,
                        proposedShift.EndTime.Hour,
                        proposedShift.EndTime.Minute,
                        proposedShift.EndTime.Second,
                        proposedShift.EndTime.Offset).UtcDateTime,
                    Duration = proposedShift.EndTime - proposedShift.StartTime
                });
                _nowRepository.Save();

                return new JsonResult("Saved");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}