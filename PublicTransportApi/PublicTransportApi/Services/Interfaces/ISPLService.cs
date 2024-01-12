using PublicTransportApi.Data.Models;
using PublicTransportApi.Data.Models.DTOs;

namespace PublicTransportApi.Services.Interfaces;

public interface ISPLService
{
    Task<Result<List<StopPointLineCorrelation>>> GetAllSPL();
    Task<Result<StopPointLineCorrelation>> GetSPLById(int id);
    Task<Result<List<string>>> GetNearestDeparturesTime(int lineId);
    Task<Result<StopPointLineCorrelation>> AddSPL(SPLDTO splDto);
    Task<Result> DeleteSPL(int id);
    Task<Result<StopPointLineCorrelation>> UpdateSPL(int id, SPLDTO splDto);
}