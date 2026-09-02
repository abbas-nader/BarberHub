using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/")]
[Authorize]
public abstract class BaseController : ControllerBase
{
}