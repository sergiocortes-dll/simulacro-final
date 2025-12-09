using EduTrack.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Edutrack.API.Controllers.Public;

public class AcademicProgramsController : ControllerBase
{
    private readonly IMediator _mediator;

    public AcademicProgramsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    /// <summary>
    /// Obtiene todos los programas académicos activos
    /// </summary>
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<AcademicProgramDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetActivePrograms()
    {
        var query = new GetActive
    }
}