using Ordering.Domain.Core.Models;
using System;

namespace Ordering.Domain.AggregatesModel.OderAggregate
{
    public class Order : Entity, IAggregateRoot
    {
        public DateTime CreatedAt { get; private set; }
        public Address Address { get; private set; }
    }
}
