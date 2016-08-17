using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Events;
using Gite.Messaging.Events;
using Gite.Model.Aggregates;
using Gite.Model.Services.Mailing;

namespace Gite.Model.Handlers.Events
{
    public class MailSenderHandler : IEventHandler<ReservationCreated>, IEventHandler<AdvancePaymentReceived>
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateLoader;
        private readonly IMailGenerator _mailGenerator;
        private readonly IMailSender _mailSender;

        public MailSenderHandler(IAggregateManager<ReservationAggregate> aggregateLoader, IMailGenerator mailGenerator, IMailSender mailSender)
        {
            if (aggregateLoader == null) throw new ArgumentNullException("aggregateLoader");
            if (mailGenerator == null) throw new ArgumentNullException("mailGenerator");
            if (mailSender == null) throw new ArgumentNullException("mailSender");

            _aggregateLoader = aggregateLoader;
            _mailGenerator = mailGenerator;
            _mailSender = mailSender;
        }

        public void Handle(ReservationCreated @event)
        {
            var reservation = _aggregateLoader.Load(@event.AggregateId);
            var mail = _mailGenerator.GenerateReservationCreated(reservation);

            _mailSender.SendMail(mail, reservation.Contact.Mail);
        }

        public void Handle(AdvancePaymentReceived @event)
        {
            var reservation = _aggregateLoader.Load(@event.AggregateId);

            var mail = _mailGenerator.GenerateAdvancePaymentReceived(reservation);
            _mailSender.SendMail(mail, _mailSender.From);
        }
    }
}