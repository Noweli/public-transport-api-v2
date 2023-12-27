using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PublicTransportApi.Data;
using PublicTransportApi.Data.Models;
using PublicTransportApi.Data.Models.DTOs;
using PublicTransportApi.Data.Models.Mappers;
using PublicTransportApi.Helpers.Extensions;
using PublicTransportApi.Resources;
using PublicTransportApi.Services.Interfaces;

namespace PublicTransportApi.Services;

public class StopPointService : IStopPointService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IValidator<StopPointDTO> _dtoValidator;

    public StopPointService(ApplicationDbContext applicationDbContext, IValidator<StopPointDTO> dtoValidator)
    {
        _applicationDbContext = applicationDbContext;
        _dtoValidator = dtoValidator;
    }

    public async Task<Result<List<StopPoint>>> GetAllStopPoints()
    {
        try
        {
            var result = await _applicationDbContext.StopPoints.ToListAsync();

            return new Result<List<StopPoint>>
            {
                IsSuccess = true,
                Data = result
            };
        }
        catch (Exception)
        {
            return new Result<List<StopPoint>>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result<StopPoint>> AddStopPoint(StopPointDTO stopPointDTO)
    {
        var validationResult = await _dtoValidator.ValidateAsync(stopPointDTO);

        if (!validationResult.IsValid)
        {
            return new Result<StopPoint>
            {
                IsSuccess = false,
                Message = validationResult.Errors.GetConcatenatedErrorMessages()
            };
        }

        var mapper = new StopPointMapper();
        var modelToAdd = mapper.DtoToStopPoint(stopPointDTO);

        try
        {
            var addedEntity = await _applicationDbContext.StopPoints.AddAsync(modelToAdd);
            _ = await _applicationDbContext.SaveChangesAsync();

            return new Result<StopPoint>
            {
                IsSuccess = true,
                Data = addedEntity.Entity
            };
        }
        catch (Exception)
        {
            return new Result<StopPoint>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result> DeleteStopPoint(int id)
    {
        try
        {
            var stopPointFromDb = await _applicationDbContext.StopPoints.FindAsync(id);

            if (stopPointFromDb is null)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = ErrorMessages.StopPoint_NotFound
                };
            }

            _ = _applicationDbContext.Remove(stopPointFromDb);
            _ = await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new Result
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }

        return new Result
        {
            IsSuccess = true
        };
    }

    public async Task<Result<StopPoint>> UpdateStopPoint(int id, StopPointDTO stopPointDTO)
    {
        try
        {
            var stopPointFromDb = await _applicationDbContext.StopPoints.FindAsync(id);

            if (stopPointFromDb is null)
            {
                return new Result<StopPoint>
                {
                    IsSuccess = false,
                    Message = ErrorMessages.StopPoint_NotFound
                };
            }

            UpdateStopPoint(stopPointFromDb, stopPointDTO);
            
            _ = await _applicationDbContext.SaveChangesAsync();
            
            return new Result<StopPoint>
            {
                IsSuccess = true,
                Data = stopPointFromDb
            };
        }
        catch (Exception)
        {
            return new Result<StopPoint>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    private static void UpdateStopPoint(StopPoint stopPoint, StopPointDTO stopPointDTO)
    {
        if (stopPointDTO.Lat is not null)
        {
            stopPoint.Lat = stopPointDTO.Lat;
        }

        if (stopPointDTO.Long is not null)
        {
            stopPoint.Long = stopPointDTO.Long;
        }

        if (stopPointDTO.Identifier != null)
        {
            stopPoint.Identifier = stopPointDTO.Identifier;
        }

        if (stopPointDTO.Name is not null)
        {
            stopPoint.Name = stopPointDTO.Name;
        }

        if (stopPointDTO.StreetName is not null)
        {
            stopPoint.StreetName = stopPointDTO.StreetName;
        }
    }
}