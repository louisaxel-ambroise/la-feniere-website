using Gite.Model.Business;
using Gite.Model.Services;
using Ninject.Modules;

namespace Gite.Factory
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPriceCalculator>().To<PriceCalculator>();
            Bind<IReservationPersister>().To<ReservationPersister>();
        }
    }
}