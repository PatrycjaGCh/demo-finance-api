namespace DemoFinance.Domain.BuildingBlocks;

public abstract class ValueObject
{
    public static bool operator ==(ValueObject left, ValueObject right)
    {
        if (ReferenceEquals(left, null) ^ ReferenceEquals(right, null))
        {
            return false;
        }

        return ReferenceEquals(left, right) || left.Equals(right);
    }
    
    public static bool operator !=(ValueObject obj1, ValueObject obj2)
    {
        return !(obj1 == obj2);
    }

    protected abstract IEnumerable<object> GetEqualityComponents(); // it could be done in more automatic way, eg. comparing all props and fields

    public override bool Equals(object obj)
    {
        if (obj == null || obj.GetType() != GetType())
        {
            return false;
        }

        var other = (ValueObject)obj;

        return this.GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        return GetEqualityComponents()
            .Select(x => x != null ? x.GetHashCode() : 0)
            .Aggregate((x, y) => x ^ y);
    }
}