using Gite.Model.Model;

namespace Gite.Model.Readers
{
    public interface IBookedWeekRepository
    {
        void Add(BookedWeek week);
    }
}