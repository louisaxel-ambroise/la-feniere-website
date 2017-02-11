using System;
using Gite.Domain.Interceptors;
using Gite.Domain.Model;
using Gite.Domain.Readers;
using Gite.Domain.Services.Mailing;
using Gite.Domain.Readers;
using System.Linq;

namespace Gite.Domain.Services.Reservations
{
    public class ReservationManager : IReservationManager
    {
        private readonly IReservationRepository _reservationRepository;
        private readonly ICalendarRepository _calendarRepository;
        private readonly IMailSender _mailSender;

        public ReservationManager(IReservationRepository reservationRepository, ICalendarRepository calendarRepository, IMailSender mailSender)
        {
            if (reservationRepository == null) throw new ArgumentNullException("reservationRepository");
            if (calendarRepository == null) throw new ArgumentNullException("calendarRepository");
            if (mailSender == null) throw new ArgumentNullException("mailSender");

            _reservationRepository = reservationRepository;
            _calendarRepository = calendarRepository;
            _mailSender = mailSender;
        }

        public Guid Book(DateTime firstWeek, DateTime lastWeek, double expectedPrice, Contact contact, People people)
        {
            var reservations = _calendarRepository.QueryValids();
            if (reservations.Any(x => (x.FirstWeek <= firstWeek && x.LastWeek >= firstWeek) || (x.FirstWeek >= firstWeek && x.FirstWeek < lastWeek))) throw new Exception("A reservation for these dates already exists");

            var id = Guid.NewGuid();
            var reservation = new Reservation
            {
                Id = id,
                BookedOn = DateTime.UtcNow,
                FirstWeek = firstWeek,
                LastWeek = lastWeek,
                IsLastMinute = (firstWeek - DateTime.UtcNow.Date).Days <= 7,
                DisablesOn = DateTime.UtcNow.AddDays(7),
                DefaultPrice = expectedPrice,
                FinalPrice = expectedPrice,
                People = people,
                Contact = contact,
            };

            _reservationRepository.Insert(reservation);
            _reservationRepository.Flush();
            _mailSender.SendReservationCreated(reservation);

            return id;
        }

        [CommitTransaction]
        public void CancelReservation(Guid id, string reason)
        {
            var reservation = _reservationRepository.Load(id);
            if (reservation.IsCancelled) throw new Exception("Reservation already cancelled.");

            reservation.IsCancelled = true;
            reservation.CancellationReason = reason;
            reservation.CancellationDate = DateTime.UtcNow;

            _reservationRepository.Flush();
            _mailSender.SendReservationCancelled(reservation);
        }

        [CommitTransaction]
        public void DeclareAdvancePaymentDone(Guid id)
        {
            var reservation = _reservationRepository.Load(id);
            if (reservation.PaymentDeclared) throw new Exception("Advance payment already declared.");

            reservation.AdvancePaymentDeclared = true;
            reservation.AdvancePaymentDeclarationDate = DateTime.UtcNow;

            _reservationRepository.Flush();
            _mailSender.SendAdvancePaymentDeclared(reservation);
        }

        [CommitTransaction]
        public void DeclareAdvanceReceived(Guid id, double amount)
        {
            var reservation = _reservationRepository.Load(id);
            if (reservation.AdvancePaymentReceived) throw new Exception("Advance payment already declared.");

            reservation.AdvancePaymentReceived = true;
            reservation.AdvancePaymentReceptionDate = DateTime.UtcNow;
            reservation.AdvancePaymentValue = amount;

            _reservationRepository.Flush();
            _mailSender.SendAdvancePaymentReceived(reservation);
        }

        [CommitTransaction]
        public void DeclarePaymentDone(Guid id)
        {
            var reservation = _reservationRepository.Load(id);
            if (reservation.PaymentDeclared) throw new Exception("Payment already declared.");

            reservation.PaymentDeclared = true;
            reservation.PaymentDeclarationDate = DateTime.UtcNow;

            _reservationRepository.Flush();
            _mailSender.SendPaymentDeclared(reservation);
        }

        [CommitTransaction]
        public void DeclarePaymentReceived(Guid id, double amount)
        {
            var reservation = _reservationRepository.Load(id);
            if (reservation.PaymentReceived) throw new Exception("Payment already received.");

            reservation.PaymentReceived = true;
            reservation.PaymentReceptionDate = DateTime.UtcNow;
            reservation.PaymentValue = amount;

            _reservationRepository.Flush();
            _mailSender.SendFinalPaymentReceived(reservation);
        }

        [CommitTransaction]
        public void ExtendExpiration(Guid id, int days)
        {
            var reservation = _reservationRepository.Load(id);
            if (reservation.AdvancePaymentReceived || reservation.PaymentReceived) throw new Exception("Reservation already validated.");

            reservation.DisablesOn = reservation.DisablesOn.AddDays(days);
        }
    }
}