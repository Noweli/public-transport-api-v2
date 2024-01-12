using Microsoft.EntityFrameworkCore;
using PublicTransportApi.Data;
using PublicTransportApi.Data.Models;
using PublicTransportApi.Data.Models.DTOs;
using PublicTransportApi.Resources;
using PublicTransportApi.Services.Interfaces;

namespace PublicTransportApi.Services;

public class SPLService : ISPLService
{
    private readonly ApplicationDbContext _applicationDbContext;

    public SPLService(ApplicationDbContext applicationDbContext)
    {
        _applicationDbContext = applicationDbContext;
    }

    public async Task<Result<List<StopPointLineCorrelation>>> GetAllSPL()
    {
        try
        {
            var result = await _applicationDbContext.StopPointLineCorrelations
                .Include(spl => spl.Line)
                .Include(spl => spl.StopPoint)
                .Include(spl => spl.ScheduleEntries)
                .ToListAsync();

            return new Result<List<StopPointLineCorrelation>>
            {
                IsSuccess = true,
                Data = result
            };
        }
        catch (Exception)
        {
            return new Result<List<StopPointLineCorrelation>>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result<StopPointLineCorrelation>> GetSPLById(int id)
    {
        try
        {
            var result = await _applicationDbContext.StopPointLineCorrelations
                .Include(spl => spl.Line)
                .Include(spl => spl.StopPoint)
                .Include(spl => spl.ScheduleEntries)
                .FirstOrDefaultAsync(spl => spl.Id.Equals(id));

            if (result is null)
            {
                return new Result<StopPointLineCorrelation>
                {
                    IsSuccess = false,
                    Message = ErrorMessages.SPL_SPLNotFound
                };
            }

            return new Result<StopPointLineCorrelation>
            {
                IsSuccess = true,
                Data = result
            };
        }
        catch (Exception)
        {
            return new Result<StopPointLineCorrelation>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result<List<string>>> GetNearestDeparturesTime(int lineId)
    {
        try
        {
            var schedules = await _applicationDbContext.StopPointLineCorrelations
                .Include(spl => spl.ScheduleEntries)
                .Where(spl => spl.Line != null && spl.Line.Id.Equals(lineId))
                .SelectMany(spl => spl.ScheduleEntries)
                .ToListAsync();

            var scheduleTimes = schedules.OrderDescending()
                .Take(5)
                .Select(schedule => schedule.DateTime.ToString("t"))
                .ToList();

            return new Result<List<string>>
            {
                IsSuccess = true,
                Data = scheduleTimes
            };
        }
        catch (Exception)
        {
            return new Result<List<string>>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result<StopPointLineCorrelation>> AddSPL(SPLDTO splDto)
    {
        try
        {
            var line = await _applicationDbContext.Lines.FindAsync(splDto.LineId);
            var stopPoint = await _applicationDbContext.StopPoints.FindAsync(splDto.StopPointId);

            if (line is null)
            {
                return new Result<StopPointLineCorrelation>
                {
                    IsSuccess = false,
                    Message = ErrorMessages.Line_LineCouldNotBeFound
                };
            }

            if (stopPoint is null)
            {
                return new Result<StopPointLineCorrelation>
                {
                    IsSuccess = false,
                    Message = ErrorMessages.StopPoint_NotFound
                };
            }

            var newSPL = new StopPointLineCorrelation
            {
                Line = line,
                StopPoint = stopPoint
            };

            var added = await _applicationDbContext.StopPointLineCorrelations.AddAsync(newSPL);
            _ = await _applicationDbContext.SaveChangesAsync();

            return new Result<StopPointLineCorrelation>
            {
                IsSuccess = true,
                Data = added.Entity
            };
        }
        catch (Exception)
        {
            return new Result<StopPointLineCorrelation>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result> DeleteSPL(int id)
    {
        try
        {
            var splToDelete = await _applicationDbContext.StopPointLineCorrelations.FindAsync(id);

            if (splToDelete is null)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = ErrorMessages.SPL_SPLNotFound
                };
            }

            _ = _applicationDbContext.StopPointLineCorrelations.Remove(splToDelete);
            _ = await _applicationDbContext.SaveChangesAsync();

            return new Result
            {
                IsSuccess = true
            };
        }
        catch (Exception)
        {
            return new Result
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result<StopPointLineCorrelation>> UpdateSPL(int id, SPLDTO splDto)
    {
        try
        {
            var splToUpdate = await _applicationDbContext.StopPointLineCorrelations
                .Include(spl => spl.StopPoint)
                .Include(spl => spl.Line)
                .FirstOrDefaultAsync(spl => spl.Id.Equals(id));

            if (splToUpdate is null)
            {
                return new Result<StopPointLineCorrelation>
                {
                    IsSuccess = false,
                    Message = ErrorMessages.SPL_SPLNotFound
                };
            }

            if (splDto.LineId != splToUpdate.Line!.Id)
            {
                var line = await _applicationDbContext.Lines.FindAsync(splDto.LineId);
                if (line is null)
                {
                    return new Result<StopPointLineCorrelation>
                    {
                        IsSuccess = false,
                        Message = ErrorMessages.Line_LineCouldNotBeFound
                    };
                }

                splToUpdate.Line = line;
            }

            if (splDto.StopPointId != splToUpdate.StopPoint?.Id)
            {
                var stopPoint = await _applicationDbContext.StopPoints.FindAsync(splDto.StopPointId);
                if (stopPoint is null)
                {
                    return new Result<StopPointLineCorrelation>
                    {
                        IsSuccess = false,
                        Message = ErrorMessages.StopPoint_NotFound
                    };
                }

                splToUpdate.StopPoint = stopPoint;
            }

            _ = await _applicationDbContext.SaveChangesAsync();

            return new Result<StopPointLineCorrelation>
            {
                IsSuccess = true,
                Data = splToUpdate
            };
        }
        catch (Exception)
        {
            return new Result<StopPointLineCorrelation>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }
}