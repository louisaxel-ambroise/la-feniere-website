using System;
using Gite.Model.Interceptors;
using Gite.Model.Model;
using Gite.Model.Repositories;
using Gite.Model.Services.Mails;
using Gite.Model.Services.Contract;

namespace Gite.Model.Services.Reservations
{
    public class Booker : IBooker
    {
        private readonly IReservationRepository _repository;
        private readonly IMailGenerator _mailGenerator;
        private readonly IMailSender _mailSender;

        public Booker(IReservationRepository repository, IMailGenerator mailGenerator, IMailSender mailSender)
        {
            if (repository == null) throw new ArgumentNullException("repository");
            if (mailGenerator == null) throw new ArgumentNullException("mailGenerator");
            if (mailSender == null) throw new ArgumentNullException("mailSender");

            _repository = repository;
            _mailGenerator = mailGenerator;
            _mailSender = mailSender;
        }

        [CommitTransaction]
        public Guid Book(Reservation reservation, ReservationDetails reservationDetails)
        {
            reservation.Contact = reservationDetails.Contact;
            reservation.People = reservationDetails.People;

            _repository.Insert(reservation);

            var mail = _mailGenerator.GenerateMail(reservation);
            _mailSender.SendMail(mail, reservation.Contact.Mail);

            return reservation.Id;
        }
    }
}