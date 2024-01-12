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
public class SPLController : ControllerBase
{
    private readonly ISPLService _splService;

    public SPLController(ISPLService splService)
    {
        _splService = splService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<StopPointLineCorrelation>>>> GetAllSPL()
    {
        var result = await _splService.GetAllSPL();

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<StopPointLineCorrelation>>> GetSplById(int id)
    {
        var result = await _splService.GetSPLById(id);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("nearestDeparture/{lineId:int}")]
    public async Task<ActionResult<Result<List<string>>>> GetNearestDeparture(int lineId)
    {
        var result = await _splService.GetNearestDeparturesTime(lineId);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }


    [HttpPost]
    public async Task<ActionResult<Result<StopPointLineCorrelation>>> AddSPL([FromBody] SPLDTO splDto)
    {
        var result = await _splService.AddSPL(splDto);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpDelete]
    public async Task<ActionResult<Result>> DeleteSPL([FromQuery] int id)
    {
        var result = await _splService.DeleteSPL(id);

        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPatch]
    public async Task<ActionResult<Result<StopPointLineCorrelation>>> UpdateSPL([FromQuery] int id,
        [FromBody] SPLDTO splDto)
    {
        var result = await _splService.UpdateSPL(id, splDto);
        
        if (result.IsSuccess)
        {
            return Ok(result);
        }

        return BadRequest(result);
    }
}