using System;
using Gite.Model.Model;

namespace Gite.Model.Services.Reservations
{
    public interface IBooker
    {
        Guid Book(DateTime from, DateTime to, Contact contact);
    }
}