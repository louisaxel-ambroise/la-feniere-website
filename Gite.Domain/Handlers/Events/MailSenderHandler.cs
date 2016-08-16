﻿using System;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Events;
using Gite.Messaging.Events;
using Gite.Model.Aggregates;
using Gite.Model.Services.Mailing;

namespace Gite.Model.Handlers.Events
{
    public class MailSenderHandler : IEventHandler<ReservationCreated>, IEventHandler<AdvancePaymentReceived>
    {
        private readonly IAggregateLoader _aggregateLoader;
        private readonly IMailGenerator _mailGenerator;
        private readonly IMailSender _mailSender;

        public MailSenderHandler(IAggregateLoader aggregateLoader, IMailGenerator mailGenerator, IMailSender mailSender)
        {
            if (aggregateLoader == null) throw new ArgumentNullException("aggregateLoader");
            if (mailGenerator == null) throw new ArgumentNullException("mailGenerator");

            _aggregateLoader = aggregateLoader;
            _mailGenerator = mailGenerator;
            _mailSender = mailSender;
        }

        public void Handle(ReservationCreated @event)
        {
            var reservation = _aggregateLoader.Load<ReservationAggregate>(@event.AggregateId);
            var mail = _mailGenerator.GenerateReservationCreated(reservation);

            _mailSender.SendMail(mail, reservation.Contact.Mail);
        }

        public void Handle(AdvancePaymentReceived @event)
        {
            var reservation = _aggregateLoader.Load<ReservationAggregate>(@event.AggregateId);

            var mail = _mailGenerator.GenerateAdvancePaymentReceived(reservation);
            _mailSender.SendMail(mail, reservation.Contact.Mail);
        }
    }
}