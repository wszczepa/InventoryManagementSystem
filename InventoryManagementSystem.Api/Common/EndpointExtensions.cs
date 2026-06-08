using FluentValidation;
using Microsoft.AspNetCore.Http.HttpResults;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace InventoryManagementSystem.Api.Common
{
    public static class EndpointExtensions
    {
        public static IResult ToErrorResult(this Exception ex)
        {
            switch (ex)
            {
                case ValidationException vex:
                    var messages = vex.Errors?.Select(e => e.ErrorMessage).ToArray() ?? new[] { vex.Message };
                    return Results.BadRequest(new ErrorResponse("Validation failed", string.Join("; ", messages)));

                case KeyNotFoundException knf:
                    return Results.NotFound(new ErrorResponse(knf.Message));

                case ArgumentException arg:
                    return Results.BadRequest(new ErrorResponse(arg.Message));

                case DbUpdateException dbUp:
                    return Results.Problem(detail: dbUp.InnerException?.Message ?? dbUp.Message, title: "A database error occurred.", statusCode: 500);

                case DbException db:
                    return Results.Problem(detail: db.Message, title: "A database error occurred.", statusCode: 500);

                default:
                    return Results.Problem(detail: ex.Message, title: "An unexpected error occurred.", statusCode: 500);
            }
        }
    }
}
