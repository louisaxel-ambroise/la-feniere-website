using System;
using System.Linq;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Messaging.Events;
using Gite.Model.Readers;

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
            if (command.AdultsCount + command.ChildrenCount > 6) throw new Exception("Maximum 6 people over 2 years are allowed.");

            var bookedWeeks = _bookedWeekReader.QueryValids().Any(x => x.Week >= command.FirstWeek && x.Week <= command.LastWeek);
            if (command.FirstWeek <= DateTime.Now || bookedWeeks)
            {
                throw new Exception("Week is past or already booked.");
            }

            RaiseEvent(new ReservationCreated
            {
                AggregateId = command.AggregateId,
                FirstWeek = command.FirstWeek,
                LastWeek = command.LastWeek,
                FinalPrice = command.FinalPrice,
                OriginalPrice = command.OriginalPrice,
                Reduction = command.Reduction,
                Address = command.Address,
                Mail = command.Mail,
                Phone = command.Phone,
                Name = command.Name,
                AdultsCount = command.AdultsCount,
                ChildrenCount = command.ChildrenCount,
                BabiesCount = command.BabiesCount,
                AnimalsCount = command.AnimalsCount,
                AnimalsType = command.AnimalsType
            });
        }
    }
}