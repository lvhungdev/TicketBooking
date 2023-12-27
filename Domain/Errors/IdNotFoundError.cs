using FluentResults;

namespace Domain.Errors;

public class IdNotFoundError : Error
{
    public IdNotFoundError(string id) : base("Id Not Found")
    {
        Metadata.Add("Id", id);
    }
}
