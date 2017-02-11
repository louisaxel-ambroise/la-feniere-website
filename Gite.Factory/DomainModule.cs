using Gite.Domain.Services.Calendar;
using Gite.Domain.Services.Contract;
using Gite.Domain.Services.Mailing;
using Gite.Domain.Services.Pricing;
using Gite.Domain.Services.Reservations;
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
            Bind<IContractGenerator>().To<ContractGenerator>().WithConstructorArgument("baseUrl", _baseUrl);
            Bind<IFicheDescriptiveGenerator>().To<FicheDescriptiveGenerator>().WithConstructorArgument("baseUrl", _baseUrl);
            Bind<IMailGenerator>().To<MailGenerator>().WithConstructorArgument("baseUrl", _baseUrl);
            Bind<IMailSender>().To<MailSender>().WithConstructorArgument("from", _from).WithConstructorArgument("password", _password);
            Bind<IPriceCalculator>().To<PriceCalculator>();
            Bind<IReservationManager>().To<ReservationManager>();
            Bind<IWeekCalendar>().To<WeekCalendar>();
        }
    }
}