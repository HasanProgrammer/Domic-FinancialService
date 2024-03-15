using Domic.Core.Domain.Contracts.Abstracts;

namespace Domic.Domain.Account.ValueObjects;

public class Balance : ValueObject
{
    public readonly long Value;

    /// <summary>
    /// 
    /// </summary>
    public Balance() {}
    
    /// <summary>
    /// 
    /// </summary>
    /// <param name="value"></param>
    /// <exception cref="InValidValueObjectException"></exception>
    public Balance(long value)
    {
        //validations

        Value = value;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}