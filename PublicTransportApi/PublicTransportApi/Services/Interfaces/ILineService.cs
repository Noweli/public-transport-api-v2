using PublicTransportApi.Data.Models;

namespace PublicTransportApi.Services.Interfaces;

public interface ILineService
{
    Task<Result> AddLine(string identifier, string name);
    Task<Result<Line>> GetLine(int id);
    Task<Result<Line>> GetLineByIdentifier(string identifier);
    Task<Result<Line>> GetLineByName(string name);
    Task<Result<List<Line>>> GetAllLines();
    Task<Result> DeleteLine(string id);
    Task<Result> DeleteLineByIdentifier(string identifier);
    Task<Result> DeleteLineByName(string name);
    Task<Result<Line>> UpdateLine(int id, string identifier, string name);
}