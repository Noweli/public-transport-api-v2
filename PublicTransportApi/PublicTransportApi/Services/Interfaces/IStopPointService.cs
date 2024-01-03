using PublicTransportApi.Data.Models;
using PublicTransportApi.Data.Models.DTOs;

namespace PublicTransportApi.Services.Interfaces;

public interface IStopPointService
{
    Task<Result<List<StopPoint>>> GetAllStopPoints();
    Task<Result<StopPoint>> GetStopPointById(int id);
    Task<Result<StopPoint>> AddStopPoint(StopPointDTO stopPointDTO);
    Task<Result> DeleteStopPoint(int id);
    Task<Result<StopPoint>> UpdateStopPoint(int id, StopPointDTO stopPointDTO);
}