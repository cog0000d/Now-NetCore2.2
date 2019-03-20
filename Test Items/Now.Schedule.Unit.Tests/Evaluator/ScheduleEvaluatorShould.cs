using Moq;
using Now.Api.Business;
using Now.Api.Interfaces;
using Now.Api.Services;
using Now.Entities.Models.Schedule;
using Xunit;

namespace Now.Schedule.Unit.Tests.Evaluator
{
    public class ScheduleEvaluatorShould
    {
        [Fact]
        public void AcceptHighIncomeApplications()
        {
            Mock<IScheduleValidator<Shift>> mockValidator =
                new Mock<IScheduleValidator<Shift>>();

            var sut = new ScheduleEvaluator(mockValidator.Object);

        }
    }
}
