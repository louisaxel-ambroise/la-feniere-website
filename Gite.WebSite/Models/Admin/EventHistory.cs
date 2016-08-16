using System;
using System.Collections.Generic;
using System.Linq;
using Gite.Cqrs.Events;
using Gite.Model.Aggregates;

namespace Gite.WebSite.Models.Admin
{
    public class EventHistory
    {
        public DateTime OccuredOn { get; set; }
        public string Description { get; set; }
    }

    public static class EventHistoryMapping
    {
        public static IEnumerable<EventHistory> MapToEventHistory(this ReservationAggregate aggregate)
        {
            return aggregate.Events.Select(Map);
        }

        private static EventHistory Map(Event @event)
        {
            return new EventHistory
            {
                OccuredOn = @event.OccuredOn,
                Description = GetDescription(@event.GetType())
            };
        }

        private static string GetDescription(Type type)
        {
            switch(type.Name)
            {
                case "ReservationCreated":
                    return "réservation créée";
                case "AdvancePaymentDeclared":
                    return "acompte déclaré payé";
                case "AdvancePaymentReceived":
                    return "acompte reçu";
                case "ReservationCancelled":
                    return "réservation annulée";
                default:
                    return type.Name;
            }
        }
    }
}