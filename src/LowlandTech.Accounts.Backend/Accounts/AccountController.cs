namespace LowlandTech.Accounts.Backend.Accounts;

[ApiController]
[Produces("application/json")]
[EnableRateLimiting("strict")]
[Route("api/[controller]")]
public sealed class AccountController : ControllerBase
{
    private readonly IMediator _mediator;
    public AccountController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Description("Retrieve a paged list (default 50 per page)")]
    [Authorize(Policy = "account::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveAllAccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveAllAccountResponse>> GetAll([FromQuery] RetrieveAllAccountRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return Ok(resp);
    }

    [HttpGet("{id:guid}")]
    [Description("Retrieve a single item by Id")]
    [Authorize(Policy = "account::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveAccountByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveAccountByIdResponse>> GetById(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new RetrieveAccountByIdRequest { Id = id }, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpPost]
    [Description("Create a new item")]
    [Authorize(Policy = "account::create")]
    [ProducesResponseType(typeof(CreateAccountResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateAccountResponse>> Create([FromBody] CreateAccountRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [Description("Update an existing item")]
    [Authorize(Policy = "account::update")]
    [ProducesResponseType(typeof(UpdateAccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UpdateAccountResponse>> Update([FromBody] UpdateAccountRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpDelete("{id:guid}")]
    [Description("Delete an item by Id")]
    [Authorize(Policy = "account::delete")]
    [ProducesResponseType(typeof(DeleteAccountResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<DeleteAccountResponse>> Delete(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new DeleteAccountRequest { Id = id }, ct);
        return Ok(resp);
    }
}

