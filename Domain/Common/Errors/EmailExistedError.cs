using FluentResults;

namespace Domain.Common.Errors;

public class EmailExistedError : Error
{
    public EmailExistedError(string email)
        : base("Email is already existed.")
    {
        Reasons.Add(new Error($"Email {email} is already existed."));
        Metadata.Add("Email", email);
    }
}
