using Gite.Domain.Model;
using System.IO;

namespace Gite.Domain.Services.Contract
{
    public interface IFicheDescriptiveGenerator
    {
        Stream GenerateForReservation(Reservation reservation);
    }
}