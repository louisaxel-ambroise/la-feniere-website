using System;

namespace Gite.Cqrs
{
    public class Command : ICommand
    {
        public Guid AggregateId { get; set; } 
    }
}