using Gite.Model.Views;

namespace Gite.Model.Readers
{
    public interface IBookedWeekRepository
    {
        void Add(BookedWeek week);
    }
}