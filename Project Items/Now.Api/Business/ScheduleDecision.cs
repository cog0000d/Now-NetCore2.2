namespace Now.Api.Business
{
    public enum ScheduleDecision
    {
        Available, //ample shift schedule
        Unavailable, //shift does not fit
        OverlapWithPrevious, //conflict with previous
        SucceedWitNext, //conflict with next
        Unbalanced, //more than the required hours for the week
    }
}
