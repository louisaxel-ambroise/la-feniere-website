using System;
using System.Linq;
using Gite.Cqrs.Aggregates;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Aggregates;
using Gite.Model.Model;
using Gite.Model.Readers;

namespace Gite.Model.Handlers.Commands
{
    public class CreateReservationHandler : ICommandHandler<CreateReservation>
    {
        private readonly IAggregateManager<ReservationAggregate> _aggregateManager;
        private readonly IBookedWeekReader _bookedWeekReader;

        public CreateReservationHandler(IAggregateManager<ReservationAggregate> aggregateManager, IBookedWeekReader bookedWeekReader)
        {
            if (aggregateManager == null) throw new ArgumentNullException("aggregateManager");
            if (bookedWeekReader == null) throw new ArgumentNullException("bookedWeekReader");

            _aggregateManager = aggregateManager;
            _bookedWeekReader = bookedWeekReader;
        }

        public void Handle(CreateReservation command)
        {
            var bookedWeeks = _bookedWeekReader.QueryValids().Any(x => x.Week >= command.FirstWeek && x.Week <= command.LastWeek);
            if (command.AdultsCount + command.ChildrenCount > 6) throw new Exception("Maximum 6 people over 2 years are allowed.");
            if (command.FirstWeek <= DateTime.UtcNow || bookedWeeks) throw new Exception("Week is past or already booked.");

            var price = new Price{ Final = command.FinalPrice, Original = command.OriginalPrice, Reduction = command.Reduction };
            var contact = new Contact { Address = command.Address, Mail = command.Mail, Name = command.Name, Phone = command.Phone };
            var people = new People{ Adults = command.AdultsCount, Children = command.ChildrenCount, Babies = command.BabiesCount, Animals = command.AnimalsCount, AnimalsDescription = command.AnimalsType };

            var reservation = new ReservationAggregate(command.AggregateId, command.FirstWeek, command.LastWeek, price, contact, people);

            _aggregateManager.Save(reservation);
        }
    }
}