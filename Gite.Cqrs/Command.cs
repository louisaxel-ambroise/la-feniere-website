using System;

namespace Gite.Cqrs
{
    public class Command
    {
        public Guid AggregateId { get; set; } 
    }
}