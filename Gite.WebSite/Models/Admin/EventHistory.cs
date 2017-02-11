using Gite.Domain.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gite.WebSite.Models.Admin
{
    public class EventHistory
    {
        public DateTime OccuredOn { get; set; }
        public string Description { get; set; }
    }

    public static class EventHistoryMapping
    {
        public static IEnumerable<EventHistory> MapToEventHistory(this Reservation reservation)
        {
            return new EventHistory[0]; // TODO: rebuild event history from scratch.
        }

        private static EventHistory Map(object @event)
        {
            return new EventHistory
            {
                //OccuredOn = @event.OccuredOn.AddHours(2),
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
                case "PaymentDeclared":
                    return "paiement déclaré payé";
                case "PaymentReceived":
                    return "paiement reçu";
                case "ReservationCancelled":
                    return "réservation annulée";
                case "AdvancePaymentDelayExtended":
                    return "délai acompte étendu";
                default:
                    return type.Name;
            }
        }
    }
}