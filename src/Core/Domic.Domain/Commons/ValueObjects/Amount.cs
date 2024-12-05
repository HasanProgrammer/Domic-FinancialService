using Domic.Core.Domain.Contracts.Abstracts;
using Domic.Core.Domain.Exceptions;

namespace Domic.Domain.Commons.ValueObjects;

public class Amount : ValueObject
{
    public readonly long? Value;

    public Amount(){}

    public Amount(long? value)
    {
        if(value < 0)
            throw new DomainException("فیلد مبلغ نباید عدد منفی باشد !");
        
        if (value is null)
            throw new DomainException("فیلد مبلغ نباید خالی باشد !");
        
        Value = value;
    }
    
    protected override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}