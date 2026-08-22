using Microsoft.AspNetCore.Mvc;

namespace BarberHub.Api.Controllers;

[ApiController]
[Route("api/v{version:apiVersion}/")]
public abstract class BaseController : ControllerBase
{
}