using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainShared.Exceptions.Base;

namespace MindTrail.WebApi.Abstractions.Providers;

public interface IHttpErrorResultProvider
{
    ConflictObjectResult ToConflict(DomainException e);

    BadRequestObjectResult ToBadRequest(DomainException e, string invalidPropName);

    NotFoundObjectResult ToNotFound(DomainException? e = null);
}