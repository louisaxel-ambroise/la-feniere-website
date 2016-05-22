using Gite.Model.Services.Calendar;
using Gite.Model.Services.DepositRefundProcessor;
using Gite.Model.Services.MailSender;
using Gite.Model.Services.PaymentProcessor;
using Gite.Model.Services.PriceCalculation;
using Gite.Model.Services.ReservationPersister;
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
            Bind<IPriceCalculator>().To<PriceCalculator>();
            Bind<IReservationPersister>().To<ReservationPersister>();
            Bind<IReservationCalendar>().To<ReservationCalendar>();
            Bind<IPaymentProcessor>().To<PaymentProcessor>();
            Bind<IRefundProcessor>().To<RefundProcessor>();
            Bind<IReservationConfirmationMailSender>().To<ReservationConfirmationMailSender>()
                .WithConstructorArgument("from", _from)
                .WithConstructorArgument("password", _password)
                .WithConstructorArgument("baseUrl", _baseUrl);
        }
    }
}