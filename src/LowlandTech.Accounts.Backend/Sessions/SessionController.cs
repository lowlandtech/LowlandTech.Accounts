namespace LowlandTech.Accounts.Backend.Sessions;

[ApiController]
[Produces("application/json")]
[EnableRateLimiting("strict")]
[Route("api/[controller]")]
public sealed class SessionController : ControllerBase
{
    private readonly IMediator _mediator;
    public SessionController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Description("Retrieve a paged list (default 50 per page)")]
    [Authorize(Policy = "session::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveAllSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveAllSessionResponse>> GetAll([FromQuery] RetrieveAllSessionRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return Ok(resp);
    }

    [HttpGet("{id:guid}")]
    [Description("Retrieve a single item by Id")]
    [Authorize(Policy = "session::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveSessionByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveSessionByIdResponse>> GetById(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new RetrieveSessionByIdRequest { Id = id }, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpPost]
    [Description("Create a new item")]
    [Authorize(Policy = "session::create")]
    [ProducesResponseType(typeof(CreateSessionResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateSessionResponse>> Create([FromBody] CreateSessionRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [Description("Update an existing item")]
    [Authorize(Policy = "session::update")]
    [ProducesResponseType(typeof(UpdateSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UpdateSessionResponse>> Update([FromBody] UpdateSessionRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpDelete("{id:guid}")]
    [Description("Delete an item by Id")]
    [Authorize(Policy = "session::delete")]
    [ProducesResponseType(typeof(DeleteSessionResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<DeleteSessionResponse>> Delete(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new DeleteSessionRequest { Id = id }, ct);
        return Ok(resp);
    }
}

