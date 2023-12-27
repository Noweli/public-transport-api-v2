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

public class ScheduleService : IScheduleEntryService
{
    private readonly ApplicationDbContext _applicationDbContext;
    private readonly IValidator<ScheduleEntryDTO> _dtoValidator;

    public ScheduleService(ApplicationDbContext applicationDbContext, IValidator<ScheduleEntryDTO> dtoValidator)
    {
        _applicationDbContext = applicationDbContext;
        _dtoValidator = dtoValidator;
    }

    public async Task<Result<ScheduleEntry>> AddSchedule(ScheduleEntryDTO scheduleEntryDTO)
    {
        var validationResult = await _dtoValidator.ValidateAsync(scheduleEntryDTO);

        if (!validationResult.IsValid)
        {
            return new Result<ScheduleEntry>
            {
                IsSuccess = false,
                Message = validationResult.Errors.GetConcatenatedErrorMessages()
            };
        }

        var mapper = new ScheduleEntryMapper();
        var scheduleToAdd = mapper.DtoToModel(scheduleEntryDTO);

        try
        {
            var attachSpl = await AttachSPL(scheduleEntryDTO.SPLCorrelationId, scheduleToAdd);

            if (!attachSpl.IsSuccess)
            {
                return new Result<ScheduleEntry>
                {
                    IsSuccess = false,
                    Message = attachSpl.Message
                };
            }
            
            var addedEntity = await _applicationDbContext.AddAsync(scheduleToAdd);
            _ = await _applicationDbContext.SaveChangesAsync();
            
            return new Result<ScheduleEntry>
            {
                IsSuccess = true,
                Data = addedEntity.Entity
            };
        }
        catch (Exception)
        {
            return new Result<ScheduleEntry>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result<List<ScheduleEntry>>> GetAllSchedules()
    {
        try
        {
            var result = await _applicationDbContext.ScheduleEntries.ToListAsync();

            return new Result<List<ScheduleEntry>>
            {
                IsSuccess = true,
                Data = result
            };
        }
        catch (Exception)
        {
            return new Result<List<ScheduleEntry>>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    public async Task<Result> RemoveSchedule(int id)
    {
        try
        {
            var entityToRemove = await _applicationDbContext.ScheduleEntries.FindAsync(id);

            if (entityToRemove is null)
            {
                return new Result<List<ScheduleEntry>>
                {
                    IsSuccess = false,
                    Message = ErrorMessages.Schedule_ScheduleNotFound
                };
            }

            _ = _applicationDbContext.ScheduleEntries.Remove(entityToRemove);
            _ = await _applicationDbContext.SaveChangesAsync();
        }
        catch (Exception)
        {
            return new Result<List<ScheduleEntry>>
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

    public async Task<Result<ScheduleEntry>> UpdateSchedule(int id, ScheduleEntryDTO scheduleEntryDTO)
    {
        try
        {
            var entityToUpdate = await _applicationDbContext.ScheduleEntries.FindAsync(id);

            if (entityToUpdate is null)
            {
                return new Result<ScheduleEntry>
                {
                    IsSuccess = false,
                    Message = ErrorMessages.Schedule_ScheduleNotFound
                };
            }

            var updateEntityResult = await UpdateEntity(entityToUpdate, scheduleEntryDTO);
            if (!updateEntityResult.IsSuccess)
            {
                return new Result<ScheduleEntry>
                {
                    IsSuccess = false,
                    Message = updateEntityResult.Message
                };
            }
            
            await _applicationDbContext.SaveChangesAsync();
            
            return new Result<ScheduleEntry>
            {
                IsSuccess = true,
                Data = entityToUpdate
            };
        }
        catch (Exception)
        {
            return new Result<ScheduleEntry>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    private async Task<Result> UpdateEntity(ScheduleEntry entityToUpdate, ScheduleEntryDTO scheduleEntryDTO)
    {
        entityToUpdate.IsRecurring = scheduleEntryDTO.IsRecurring;
        entityToUpdate.RecurringDays = string.IsNullOrEmpty(scheduleEntryDTO.RecurringDays)
            ? entityToUpdate.RecurringDays
            : scheduleEntryDTO.RecurringDays;

        entityToUpdate.DateTime = scheduleEntryDTO.DateTime == default
            ? entityToUpdate.DateTime
            : scheduleEntryDTO.DateTime;


        var attachSpl = await AttachSPL(scheduleEntryDTO.SPLCorrelationId, entityToUpdate);

        return attachSpl;
    }

    private async Task<Result> AttachSPL(int? splId, ScheduleEntry entityToUpdate)
    {
        if (splId is null)
        {
            return new Result
            {
                IsSuccess = true
            };
        }

        var spl = await _applicationDbContext.StopPointLineCorrelations.FindAsync(splId);

        if (spl is null)
        {
            return new Result
            {
                IsSuccess = false,
                Message = ErrorMessages.SPL_SPLNotFound
            };
        }

        entityToUpdate.SPLCorrelation = spl;

        return new Result
        {
            IsSuccess = true
        };
    }
}