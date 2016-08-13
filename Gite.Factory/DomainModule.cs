using Gite.Model.Services.Calendar;
using Gite.Model.Services.Pricing;
using Gite.Model.Services.Reservations;
using Ninject.Modules;

namespace Gite.Factory
{
    public class DomainModule : NinjectModule
    {
        private readonly string _from;
        private readonly string _password;
        private readonly string _baseUrl;

        public DomainModule(string from, string password, string baseUrl)
        {
            _from = from;
            _password = password;
            _baseUrl = baseUrl;
        }

        public override void Load()
        {
            // TODO: bind model.
            Bind<IBooker>().To<Booker>();
            Bind<IWeekCalendar>().To<WeekCalendar>();
            Bind<IPriceCalculator>().To<PriceCalculator>();
            Bind<IReservationPlanner>().To<ReservationPlanner>();
        }
    }
}