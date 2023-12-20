using System.Linq.Expressions;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using PublicTransportApi.Data;
using PublicTransportApi.Data.Models;
using PublicTransportApi.Helpers.Extensions;
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

    public Task<Result<Line>> GetLine(int id)
    {
        return GenericGetLine(item => item.Id.Equals(id));
    }

    public Task<Result<Line>> GetLineByIdentifier(string identifier)
    {
        return GenericGetLine(item =>
            item.Identifier != null && item.Identifier.ToLower().Equals(identifier.ToLower()));
    }

    public Task<Result<Line>> GetLineByName(string name)
    {
        return GenericGetLine(item => item.Name != null && item.Name.ToLower().Equals(name.ToLower()));
    }

    public async Task<Result<List<Line>>> GetAllLines()
    {
        List<Line> linesFromDb;

        try
        {
            linesFromDb = await _dbContext.Lines.ToListAsync();
        }
        catch (Exception)
        {
            return new Result<List<Line>>
            {
                IsSuccess = true,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }

        return new Result<List<Line>>
        {
            IsSuccess = true,
            Data = linesFromDb
        };
    }

    public async Task<Result> DeleteLine(int id)
    {
        try
        {
            var lineFromDb = await _dbContext.Lines.FirstOrDefaultAsync(l => l.Id == id);

            if (lineFromDb is null)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = ErrorMessages.Line_LineCouldNotBeFound
                };
            }

            _ = _dbContext.Lines.Remove(lineFromDb);
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
            IsSuccess = true
        };
    }

    public async Task<Result> DeleteLineByIdentifier(string identifier)
    {
        if (string.IsNullOrEmpty(identifier))
        {
            return new Result
            {
                IsSuccess = false,
                Message = ErrorMessages.Line_IdentifierCannotBeNull
            };
        }

        try
        {
            var lineFromDb = await _dbContext.Lines.FirstOrDefaultAsync(l =>
                l.Identifier != null && l.Identifier.ToLower().Equals(identifier.ToLower()));

            if (lineFromDb is null)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = ErrorMessages.Line_LineCouldNotBeFound
                };
            }

            _ = _dbContext.Lines.Remove(lineFromDb);
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
            IsSuccess = true
        };
    }

    public async Task<Result> DeleteLineByName(string name)
    {
        if (string.IsNullOrEmpty(name))
        {
            return new Result
            {
                IsSuccess = false,
                Message = ErrorMessages.Line_NameCannotBeNull
            };
        }

        try
        {
            var lineFromDb =
                await _dbContext.Lines.FirstOrDefaultAsync(l =>
                    l.Name != null && l.Name.ToLower().Equals(name.ToLower()));

            if (lineFromDb is null)
            {
                return new Result
                {
                    IsSuccess = false,
                    Message = ErrorMessages.Line_LineCouldNotBeFound
                };
            }

            _dbContext.Lines.Remove(lineFromDb);
            await _dbContext.SaveChangesAsync();
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

    public async Task<Result<Line>> UpdateLine(int id, string? identifier, string? name)
    {
        try
        {
            var lineFromDb = await _dbContext.Lines.FirstOrDefaultAsync(l => l.Id.Equals(id));

            if (lineFromDb is null)
            {
                return new Result<Line>
                {
                    IsSuccess = false,
                    Message = ErrorMessages.Line_LineCouldNotBeFound
                };
            }

            if(!string.IsNullOrEmpty(identifier))
            {
                lineFromDb.Identifier = identifier;
            }

            if(!string.IsNullOrEmpty(name))
            {
                lineFromDb.Name = name;
            }

            var validationResult = await _validator.ValidateAsync(lineFromDb);

            if (!validationResult.IsValid)
            {
                return new Result<Line>
                {
                    IsSuccess = false,
                    Message = validationResult.Errors.GetConcatenatedErrorMessages()
                };
            }

            _ = _dbContext.Lines.Update(lineFromDb);
            _ = await _dbContext.SaveChangesAsync();

            return new Result<Line>
            {
                IsSuccess = true,
                Data = lineFromDb
            };
        }
        catch (Exception)
        {
            return new Result<Line>
            {
                IsSuccess = false,
                Message = ErrorMessages.Generic_ExceptionOccured
            };
        }
    }

    private async Task<Result<Line>> GenericGetLine(Expression<Func<Line, bool>> predicate)
    {
        Line? lineFromDb;

        try
        {
            lineFromDb = await _dbContext.Lines.FirstOrDefaultAsync(predicate);
        }
        catch (Exception)
        {
            return new Result<Line>
            {
                IsSuccess = true,
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
}