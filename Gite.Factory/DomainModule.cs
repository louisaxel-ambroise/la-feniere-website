using Gite.Model.Services.Calendar;
using Gite.Model.Services.Contract;
using Gite.Model.Services.Mailing;
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
            Bind<IWeekCalendar>().To<WeekCalendar>();
            Bind<IContractGenerator>().To<ContractGenerator>().WithConstructorArgument("baseUrl", _baseUrl);
            Bind<IFicheDescriptiveGenerator>().To<FicheDescriptiveGenerator>().WithConstructorArgument("baseUrl", _baseUrl);
            Bind<IMailGenerator>().To<MailGenerator>().WithConstructorArgument("baseUrl", _baseUrl);
            Bind<IMailSender>().To<MailSender>().WithConstructorArgument("from", _from).WithConstructorArgument("password", _password);
            Bind<IPriceCalculator>().To<PriceCalculator>();
            Bind<IBooker>().To<Booker>();
            Bind<IReservationCanceller>().To<ReservationCanceller>();
            Bind<IPaymentManager>().To<PaymentManager>();
        }
    }
}