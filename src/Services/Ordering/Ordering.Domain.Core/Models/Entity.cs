using MediatR;
using System;
using System.Collections.Generic;

namespace Ordering.Domain.Core.Models
{
    public abstract class Entity
    {
        public Guid Id { get; protected set; }

        private List<INotification> _domainEvents;
        public IReadOnlyCollection<INotification> DomainEvents => _domainEvents?.AsReadOnly();

        public void AddDomainEvent(INotification eventItem)
        {
            _domainEvents = _domainEvents ?? new List<INotification>();
            _domainEvents.Add(eventItem);
        }

        public void RemoveDomainEvent(INotification eventItem) => _domainEvents?.Remove(eventItem);

        public void ClearDomainEvents() => _domainEvents?.Clear();

        public override bool Equals(object obj)
        {
            var compareTo = obj as Entity;

            if (ReferenceEquals(this, compareTo)) return true;
            if (compareTo is null) return false;

            return Id.Equals(compareTo.Id);
        }

        public static bool operator ==(Entity entityA, Entity entityB)
        {
            if (entityA is null && entityB is null) return true;

            if (entityA is null || entityB is null) return false;

            return entityA.Equals(entityB);
        }

        public static bool operator !=(Entity a, Entity b) => !(a == b);

        public override int GetHashCode() => (GetType().GetHashCode() * 907) + Id.GetHashCode();

        public override string ToString() => GetType().Name + " [Id=" + Id + "]";
    }
}