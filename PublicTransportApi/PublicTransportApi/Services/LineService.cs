using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PublicTransportApi.Data;
using PublicTransportApi.Data.Models;
using PublicTransportApi.Migrations;
using PublicTransportApi.Resources;
using PublicTransportApi.Services.Interfaces;

namespace PublicTransportApi.Services;

public class LineService : ILineService
{
    private readonly ApplicationDbContext _dbContext;
    private readonly IValidator<Line> _validator;

    public LineService(ApplicationDbContext dbContext, IValidator<Line> validator)
    {
        _dbContext = dbContext;
        _validator = validator;
    }

    public async Task<Result> AddLine(string identifier, string name)
    {
        var line = new Line
        {
            Identifier = identifier,
            Name = name
        };

        var validationResult = await _validator.ValidateAsync(line);

        if (!validationResult.IsValid)
        {
            return new Result
            {
                IsSuccess = false,
                Message = validationResult.Errors.GetConcatenatedErrorMessages()
            };
        }

        try
        {
            _ = await _dbContext.Lines.AddAsync(line);
            _ = await _dbContext.SaveChangesAsync();
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
            IsSuccess = true,
        };
    }

    public async Task<Result<Line>> GetLine(int id)
    {
        Line? lineFromDb;
        
        try
        {
            lineFromDb = await _dbContext.Lines.FirstOrDefaultAsync(item => item.Id.Equals(id));
        }
        catch (Exception)
        {
            return new Result<Line>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }

        if (lineFromDb is null)
        {
            return new Result<Line>
            {
                IsSuccess = false,
                Message = ErrorMessages.Line_LineCouldNotBeFound
            };
        }

        return new Result<Line>
        {
            IsSuccess = true,
            Data = lineFromDb
        };
    }

    public Task<Result<Line>> GetLineByIdentifier(string identifier)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Line>> GetLineByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<Result<List<Line>>> GetAllLines()
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteLine(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteLineByIdentifier(string identifier)
    {
        throw new NotImplementedException();
    }

    public Task<Result> DeleteLineByName(string name)
    {
        throw new NotImplementedException();
    }

    public Task<Result<Line>> UpdateLine(int id, string identifier, string name)
    {
        throw new NotImplementedException();
    }
}