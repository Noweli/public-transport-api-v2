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
public class ScheduleController : ControllerBase
{
    private readonly IScheduleEntryService _scheduleService;

    public ScheduleController(IScheduleEntryService scheduleService)
    {
        _scheduleService = scheduleService;
    }

    [HttpGet]
    public async Task<ActionResult<Result<List<ScheduleEntry>>>> GetAllSchedules()
    {
        var serviceResult = await _scheduleService.GetAllSchedules();

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }

    [HttpPost]
    public async Task<ActionResult<Result<ScheduleEntry>>> AddSchedule([FromBody] ScheduleEntryDTO scheduleEntryDTO)
    {
        var serviceResult = await _scheduleService.AddSchedule(scheduleEntryDTO);

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }

    [HttpDelete]
    public async Task<ActionResult<Result>> RemoveSchedule([FromQuery] int id)
    {
        var serviceResult = await _scheduleService.RemoveSchedule(id);

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }

    [HttpPatch]
    public async Task<ActionResult<Result<ScheduleEntry>>> UpdateSchedule([FromQuery] int id,
        [FromBody] ScheduleEntryDTO scheduleEntryDTO)
    {
        var serviceResult = await _scheduleService.UpdateSchedule(id, scheduleEntryDTO);

        if (!serviceResult.IsSuccess)
        {
            return new BadRequestObjectResult(serviceResult);
        }

        return new OkObjectResult(serviceResult);
    }
}