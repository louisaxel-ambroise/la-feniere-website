using System;
using Gite.Cqrs.Commands;
using Gite.Messaging.Commands;
using Gite.Model.Interceptors;
using Gite.Model.Model;
using Gite.Model.Services.Pricing;

namespace Gite.Model.Services.Reservations
{
    public class Booker : IBooker
    {
        private readonly ICommandDispatcher _commandDispatcher;
        private readonly IPriceCalculator _priceCalculator;

        public Booker(ICommandDispatcher commandDispatcher, IPriceCalculator priceCalculator)
        {
            if (commandDispatcher == null) throw new ArgumentNullException("commandDispatcher");
            if (priceCalculator == null) throw new ArgumentNullException("priceCalculator");

            _commandDispatcher = commandDispatcher;
            _priceCalculator = priceCalculator;
        }

        [CommitTransaction]
        public Guid Book(DateTime firstWeek, DateTime lastWeek, double expectedPrice, Contact contact, People people)
        {
            var price = _priceCalculator.ComputeForInterval(firstWeek, lastWeek);
            if (Math.Abs(price.Final - expectedPrice) > 0.01)
            {
                throw new Exception(string.Format("Price has changed. Expected {0} but was {1}", expectedPrice, price.Final));
            }

            var command = new CreateReservation
            {
                AggregateId = Guid.NewGuid(),
                IsLastMinute = (firstWeek.Date - DateTime.Now).Days <= 7,
                FirstWeek = firstWeek,
                LastWeek = lastWeek,
                Address = contact.Address,
                Mail = contact.Mail,
                Phone = contact.Phone,
                Name = contact.Name,
                AdultsCount = people.Adults,
                ChildrenCount = people.Children,
                BabiesCount = people.Babies,
                AnimalsCount = people.Animals,
                AnimalsType = people.AnimalsDescription,
                FinalPrice = price.Final,
                OriginalPrice = price.Original,
                Reduction = price.Reduction
            };

            _commandDispatcher.Dispatch(command);

            return command.AggregateId;
        }
    }
}