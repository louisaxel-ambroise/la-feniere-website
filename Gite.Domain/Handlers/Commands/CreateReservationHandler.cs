using System;
using System.Linq;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Messaging.Events;
using Gite.Model.Repositories;

namespace Gite.Model.Handlers.Commands
{
    public class CreateReservationHandler : CommandHandler<CreateReservation>
    {
        private readonly IBookedWeekReader _bookedWeekReader;

        public CreateReservationHandler(IBookedWeekReader bookedWeekReader)
        {
            _bookedWeekReader = bookedWeekReader;
        }

        public override void Handle(CreateReservation command)
        {
            var bookedWeeks = _bookedWeekReader.Query().Any(x => x.IsValid() && x.Week >= command.FirstWeek && x.Week <= command.LastWeek);
            if (bookedWeeks) throw new Exception("Week is already booked.");

            RaiseEvent(new ReservationCreated
            {
                FirstWeek = command.FirstWeek,
                LastWeek = command.LastWeek
            });
        }
    }
}