using System;

namespace Gite.Cqrs.Commands
{
    public class Command
    {
        public Guid AggregateId { get; set; } 
    }
}