using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc.Routing;
using Moq;
using Now.Api.Controllers;
using Now.Api.Model;
using Now.Data.Interfaces;
using Xunit;

namespace Now.Schedule.Unit.Tests.Schedule
{
    public class AddShiftTest
    {
        INowRepository _nowRepository;
        ScheduleController _controller;

        public AddShiftTest()
        {
            _controller = new ScheduleController(_nowRepository);
        }

        [Fact]
        public void AddShiftMissingEmployeeId()
        {
            ProposedShift proposedShift = new ProposedShift()
            {
                dateTime = new DateTimeOffset(),
                ShiftId = new Guid()
            };

            var mockRepository = new Mock<INowRepository>();
            var controller = new ScheduleController(mockRepository.Object);
 

            var response = controller.AddShift(proposedShift);

            Assert.Equal("Invalid Data", response.Value);
        }
    }
    }
