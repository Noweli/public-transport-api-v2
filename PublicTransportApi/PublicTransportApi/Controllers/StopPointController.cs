using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PublicTransportApi.Data.Models;
using PublicTransportApi.Data.Models.DTOs;
using PublicTransportApi.Services;
using PublicTransportApi.Services.Interfaces;

namespace PublicTransportApi.Controllers;

[ApiController]
[ApiVersion(1.0)]
[ApiExplorerSettings(GroupName = "V1")]
[Route("api/v{version:apiVersion}/[controller]")]
public class StopPointController : ControllerBase
{
    private readonly IStopPointService _stopPointService;

    public StopPointController(IStopPointService stopPointService)
    {
        _stopPointService = stopPointService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<StopPoint>>>> GetAllStopPoints()
    {
        var serviceResult = await _stopPointService.GetAllStopPoints();

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<StopPoint>>> GetStopPointById(int id)
    {
        var serviceResult = await _stopPointService.GetStopPointById(id);

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }

    [HttpPost]
    public async Task<ActionResult<Result<StopPoint>>> AddStopPoint([FromBody] StopPointDTO stopPointDTO)
    {
        var serviceResult = await _stopPointService.AddStopPoint(stopPointDTO);

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }

    [HttpDelete]
    public async Task<ActionResult<Result>> DeleteStopPoint([FromQuery] int id)
    {
        var serviceResult = await _stopPointService.DeleteStopPoint(id);

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }

    [HttpPatch]
    public async Task<ActionResult<Result<List<StopPoint>>>> UpdateStopPoint([FromQuery] int id,
        [FromBody] StopPointDTO stopPointDTO)
    {
        var serviceResult = await _stopPointService.UpdateStopPoint(id, stopPointDTO);

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }
}