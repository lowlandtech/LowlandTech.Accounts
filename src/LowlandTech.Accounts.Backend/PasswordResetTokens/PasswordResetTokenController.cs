namespace LowlandTech.Accounts.Backend.PasswordResetTokens;

[ApiController]
[Produces("application/json")]
[EnableRateLimiting("strict")]
[Route("api/passwordresettokens")]
public sealed class PasswordResetTokenController : ControllerBase
{
    private readonly IMediator _mediator;
    public PasswordResetTokenController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Description("Retrieve a paged list (default 50 per page)")]
    [Authorize(Policy = "passwordresettoken::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveAllPasswordResetTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveAllPasswordResetTokenResponse>> GetAll([FromQuery] RetrieveAllPasswordResetTokenRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return Ok(resp);
    }

    [HttpGet("{id:guid}")]
    [Description("Retrieve a single item by Id")]
    [Authorize(Policy = "passwordresettoken::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrievePasswordResetTokenByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrievePasswordResetTokenByIdResponse>> GetById(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new RetrievePasswordResetTokenByIdRequest { Id = id }, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpPost]
    [Description("Create a new item")]
    [Authorize(Policy = "passwordresettoken::create")]
    [ProducesResponseType(typeof(CreatePasswordResetTokenResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreatePasswordResetTokenResponse>> Create([FromBody] CreatePasswordResetTokenRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [Description("Update an existing item")]
    [Authorize(Policy = "passwordresettoken::update")]
    [ProducesResponseType(typeof(UpdatePasswordResetTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UpdatePasswordResetTokenResponse>> Update([FromBody] UpdatePasswordResetTokenRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpDelete("{id:guid}")]
    [Description("Delete an item by Id")]
    [Authorize(Policy = "passwordresettoken::delete")]
    [ProducesResponseType(typeof(DeletePasswordResetTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<DeletePasswordResetTokenResponse>> Delete(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new DeletePasswordResetTokenRequest { Id = id }, ct);
        return Ok(resp);
    }
}

