using System.Net;
using Domain.Errors;
using FluentResults;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers;

[ApiController]
public class ApiController : ControllerBase
{
    protected IActionResult Problem(IEnumerable<IError> errors)
    {
        IError error = errors.First();
        string title = error.Message;
        string detail = string.Join("; ", error.Reasons.Select(m => m.Message));

        return error switch
        {
            IdNotFoundError => Problem(statusCode: (int)HttpStatusCode.NotFound, title: title, detail: detail),
            ValidationError => Problem(statusCode: (int)HttpStatusCode.BadRequest, title: title, detail: detail),
            _ => Problem(statusCode: (int)HttpStatusCode.InternalServerError)
        };
    }
}
