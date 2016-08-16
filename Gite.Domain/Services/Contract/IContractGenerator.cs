using System.IO;
using Gite.Model.Aggregates;

namespace Gite.Model.Services.Contract
{
    public interface IContractGenerator
    {
        Stream GenerateForReservation(ReservationAggregate reservation);
    }
}