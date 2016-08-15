using Gite.Model.Model;
using System.IO;

namespace Gite.Model.Services.Contract
{
    public interface IContractGenerator
    {
        Stream GenerateForReservation(Reservation reservation);
    }
}