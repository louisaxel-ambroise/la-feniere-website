using Gite.Model.Aggregates;
using System.IO;

namespace Gite.Model.Services.Contract
{
    public interface IFicheDescriptiveGenerator
    {
        Stream GenerateForReservation(ReservationAggregate reservation);
    }
}