using Ordering.Domain.Core.Models;
using System;

namespace Ordering.Domain.AggregatesModel.OderAggregate
{
    public class Address : ValueObject<Address>
    {
        public string Street { get; }
        public string City { get; }
        public string State { get; }
        public string Country { get; }
        public string ZipCode { get; }

        private Address() { }

        public Address(string street, string city, string state, string country, string zipcode)
        {
            Street = street;
            City = city;
            State = state;
            Country = country;
            ZipCode = zipcode;
        }

        protected override bool EqualsCore(Address other)
        {
            return other.Street == Street && other.City == City && other.State == State && other.Country == Country && other.ZipCode == ZipCode;
        }

        protected override int GetHashCodeCore()
        {
            return (GetType().GetHashCode() * 907);
        }
    }
}
