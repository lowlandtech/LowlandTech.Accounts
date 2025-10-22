namespace LowlandTech.Accounts.Backend.Devices;

[ApiController]
[Produces("application/json")]
[EnableRateLimiting("strict")]
[Route("api/devices")]
public sealed class DeviceController : ControllerBase
{
    private readonly IMediator _mediator;
    public DeviceController(IMediator mediator) => _mediator = mediator;

    [HttpGet]
    [Description("Retrieve a paged list (default 50 per page)")]
    [Authorize(Policy = "device::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveAllDeviceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveAllDeviceResponse>> GetAll([FromQuery] RetrieveAllDeviceRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return Ok(resp);
    }

    [HttpGet("{id:guid}")]
    [Description("Retrieve a single item by Id")]
    [Authorize(Policy = "device::read")]
    [ResponseCache(Duration = 30, Location = ResponseCacheLocation.Any)]
    [ProducesResponseType(typeof(RetrieveDeviceByIdResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<RetrieveDeviceByIdResponse>> GetById(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new RetrieveDeviceByIdRequest { Id = id }, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpPost]
    [Description("Create a new item")]
    [Authorize(Policy = "device::create")]
    [ProducesResponseType(typeof(CreateDeviceResponse), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<CreateDeviceResponse>> Create([FromBody] CreateDeviceRequest req, CancellationToken ct)
    {
        var result = await _mediator.Send(req, ct);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }

    [HttpPut]
    [Description("Update an existing item")]
    [Authorize(Policy = "device::update")]
    [ProducesResponseType(typeof(UpdateDeviceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<UpdateDeviceResponse>> Update([FromBody] UpdateDeviceRequest req, CancellationToken ct)
    {
        var resp = await _mediator.Send(req, ct);
        return resp.Item is null ? NotFound(resp) : Ok(resp);
    }

    [HttpDelete("{id:guid}")]
    [Description("Delete an item by Id")]
    [Authorize(Policy = "device::delete")]
    [ProducesResponseType(typeof(DeleteDeviceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<DeleteDeviceResponse>> Delete(Guid id, CancellationToken ct)
    {
        var resp = await _mediator.Send(new DeleteDeviceRequest { Id = id }, ct);
        return Ok(resp);
    }
}

