using FluentResults;

namespace Domain.Common.Errors;

public class IdNotFoundError : Error
{
    public IdNotFoundError(string id) : base("Id does not exist.")
    {
        Reasons.Add(new Error($"Id {id} does not exist."));
        Metadata.Add("Id", id);
    }
}
