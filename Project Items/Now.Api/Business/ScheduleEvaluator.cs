using System;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Now.Api.Interfaces;
using Now.Entities.Models.Schedule;

namespace Now.Api.Business
{
    public class ScheduleEvaluator
    {
        IScheduleValidator<Shift> _scheduleValidator;

        public ScheduleEvaluator(IScheduleValidator<Shift> scheduleValidator)
        {
            _scheduleValidator = scheduleValidator ??
                throw new ArgumentException(nameof(scheduleValidator));
        }

        public ScheduleDecision Evaluate(Shift proposedShift)
        {
            if (proposedShift is null)
            {
                throw new NullReferenceException(nameof(proposedShift));
            }


            return ScheduleDecision.Available;
        }

   }
}
