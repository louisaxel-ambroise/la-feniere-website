using Gite.Domain.Model;
using System.IO;

namespace Gite.Domain.Services.Contract
{
    public interface IContractGenerator
    {
        Stream GenerateForReservation(Reservation reservation);
    }
}