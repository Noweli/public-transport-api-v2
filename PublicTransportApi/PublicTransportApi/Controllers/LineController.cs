using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;
using PublicTransportApi.Data.Models;
using PublicTransportApi.Data.Models.DTOs;
using PublicTransportApi.Services;
using PublicTransportApi.Services.Interfaces;

namespace PublicTransportApi.Controllers;

[ApiVersion(1.0)]
[ApiController]
[Route("api/v{version:apiVersion}/[controller]")]
public class LineController : ControllerBase
{
    private ILineService _lineService;

    public LineController(ILineService lineService)
    {
        _lineService = lineService;
    }

    [HttpGet("getAll")]
    public async Task<ActionResult<Result<List<Line>>>> GetAllLines()
    {
        var result = await _lineService.GetAllLines();

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }
    
    [HttpGet("{id:int}")]
    public async Task<ActionResult<Result<Line>>> GetById(int id)
    {
        var result = await _lineService.GetLine(id);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }
    
    [HttpGet("getByIdentifier/{identifier}")]
    public async Task<ActionResult<Result<Line>>> GetByIdentifier(string identifier)
    {
        var result = await _lineService.GetLineByIdentifier(identifier);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }
    
    [HttpGet("getByName/{name}")]
    public async Task<ActionResult<Result<Line>>> GetByName(string name)
    {
        var result = await _lineService.GetLineByName(name);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }

    [HttpPost]
    public async Task<ActionResult<Result>> AddLine([FromBody] LineDTO lineDto)
    {
        var result = await _lineService.AddLine(lineDto.Identifier!, lineDto.Name!);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }

    [HttpDelete("{id:int}")]
    public async Task<ActionResult<Result>> DeleteLine(int id)
    {
        var result = await _lineService.DeleteLine(id);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }
    
    [HttpDelete("deleteByIdentifier/{identifier}")]
    public async Task<ActionResult<Result>> DeleteLineByIdentifier(string identifier)
    {
        var result = await _lineService.DeleteLineByIdentifier(identifier);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }
    
    [HttpDelete("deleteByName/{name}")]
    public async Task<ActionResult<Result>> DeleteLineByName(string name)
    {
        var result = await _lineService.DeleteLineByName(name);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }
    
    [HttpPatch("{id:int}")]
    public async Task<ActionResult<Result<Line>>> DeleteLineByName(int id, [FromBody] LineDTO lineDto)
    {
        var result = await _lineService.UpdateLine(id, lineDto.Identifier!, lineDto.Name!);

        if (result.IsSuccess)
        {
            return new OkObjectResult(result);
        }

        return new BadRequestObjectResult(result);
    }
}