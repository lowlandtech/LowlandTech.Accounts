namespace LowlandTech.Accounts.Backend.EmailVerificationTokens;

[ApiController]
[Produces("application/json")]
[EnableRateLimiting("strict")]
[Route("api/[controller]")]
public sealed class EmailVerificationTokenController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmailVerificationTokenController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Description("Retrieve a paged list (default 50 per page)")]
    [Authorize(Policy = "emailverificationtoken::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveAllEmailVerificationTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveAllEmailVerificationTokenResponse>> GetAll([FromQuery] RetrieveAllEmailVerificationTokenRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return Ok(resp);
    }

    [HttpGet("{id:guid}")]
    [Description("Retrieve a single item by Id")]
    [Authorize(Policy = "emailverificationtoken::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveEmailVerificationTokenByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveEmailVerificationTokenByIdResponse>> GetById(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new RetrieveEmailVerificationTokenByIdRequest { Id = id }, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpPost]
    [Description("Create a new item")]
    [Authorize(Policy = "emailverificationtoken::create")]
    [ProducesResponseType(typeof(CreateEmailVerificationTokenResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateEmailVerificationTokenResponse>> Create([FromBody] CreateEmailVerificationTokenRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [Description("Update an existing item")]
    [Authorize(Policy = "emailverificationtoken::update")]
    [ProducesResponseType(typeof(UpdateEmailVerificationTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UpdateEmailVerificationTokenResponse>> Update([FromBody] UpdateEmailVerificationTokenRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpDelete("{id:guid}")]
    [Description("Delete an item by Id")]
    [Authorize(Policy = "emailverificationtoken::delete")]
    [ProducesResponseType(typeof(DeleteEmailVerificationTokenResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<DeleteEmailVerificationTokenResponse>> Delete(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new DeleteEmailVerificationTokenRequest { Id = id }, ct);
        return Ok(resp);
    }
}

