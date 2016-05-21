using Gite.Model.Services.Calendar;
using Gite.Model.Services.DepositRefundProcessor;
using Gite.Model.Services.PaymentProcessor;
using Gite.Model.Services.PriceCalculation;
using Gite.Model.Services.ReservationPersister;
using Ninject.Modules;

namespace Gite.Factory
{
    public class DomainModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IPriceCalculator>().To<PriceCalculator>();
            Bind<IReservationPersister>().To<ReservationPersister>();
            Bind<IReservationCalendar>().To<ReservationCalendar>();
            Bind<IPaymentProcessor>().To<PaymentProcessor>();
            Bind<IRefundProcessor>().To<RefundProcessor>();
        }
    }
}