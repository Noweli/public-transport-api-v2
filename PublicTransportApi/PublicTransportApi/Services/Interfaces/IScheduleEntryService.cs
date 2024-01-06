using PublicTransportApi.Data.Models;
using PublicTransportApi.Data.Models.DTOs;

namespace PublicTransportApi.Services.Interfaces;

public interface IScheduleEntryService
{
    Task<Result<ScheduleEntry>> AddSchedule(ScheduleEntryDTO scheduleEntryDTO);
    Task<Result<List<ScheduleEntry>>> GetAllSchedules();
    Task<Result<ScheduleEntry>> GetScheduleById(int id);
    Task<Result> RemoveSchedule(int id);
    Task<Result<ScheduleEntry>> UpdateSchedule(int id, ScheduleEntryDTO scheduleEntryDTO);
}