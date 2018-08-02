namespace Ordering.Domain.Core.Models
{
    public abstract class ValueObject<T> where T : ValueObject<T>
    {
        public override bool Equals(object obj)
        {
            var valueObject = obj as T;
            return !(valueObject is null) && EqualsCore(valueObject);
        }

        protected abstract bool EqualsCore(T other);

        public override int GetHashCode() => GetHashCodeCore();

        protected abstract int GetHashCodeCore();

        public static bool operator ==(ValueObject<T> valueA, ValueObject<T> valueB)
        {
            if (valueA is null && valueB is null) return true;

            if (valueA is null || valueB is null) return false;

            return valueA.Equals(valueB);
        }

        public static bool operator !=(ValueObject<T> a, ValueObject<T> b) => !(a == b);
    }
}
