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
            return BadRequest(result);
        }

        return Ok(result);
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