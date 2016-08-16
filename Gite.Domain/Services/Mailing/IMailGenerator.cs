using Gite.Model.Aggregates;
using Gite.Model.Model;

namespace Gite.Model.Services.Mailing
{
    public interface IMailGenerator
    {
        Mail GenerateAdvancePaymentReceived(ReservationAggregate reservation);
    }
}