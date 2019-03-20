using Now.Entities.Models.Schedule;


namespace Now.Api.Interfaces
{
    public interface IScheduleValidator<T>
    {
        bool IsValid(T entity);
        void IsValid(T entity, out bool isValid);
    }
}