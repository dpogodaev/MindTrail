using Microsoft.AspNetCore.Mvc;
using MindTrail.DomainServices.Exceptions.Base;

namespace MindTrail.WebApi.Interfaces.Providers;

public interface IHttpErrorResultProvider
{
    ConflictObjectResult ToConflict(DomainException e);

    BadRequestObjectResult ToBadRequest(DomainException e, string invalidPropName);

    NotFoundObjectResult ToNotFound(DomainException? e = null);
}