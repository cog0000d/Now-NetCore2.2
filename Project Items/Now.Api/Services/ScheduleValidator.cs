using System;
using Now.Api.Interfaces;
using Now.Entities.Models.Schedule;

namespace Now.Api.Services
{
    public class ScheduleValidator : IScheduleValidator<Shift>
    {

        public bool IsValid(Shift entity)
        {
            throw new NotImplementedException();
        }

        public void IsValid(Shift entity, out bool isValid)
        {
            throw new NotImplementedException();
        }
    }
}
