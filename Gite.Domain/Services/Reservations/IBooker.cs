using System;
using Gite.Model.Views;

namespace Gite.Model.Services.Reservations
{
    public interface IBooker
    {
        Guid Book(DateTime firstWeek, DateTime lastWeek, double expectedPrice, Contact contact, People people);
    }
}