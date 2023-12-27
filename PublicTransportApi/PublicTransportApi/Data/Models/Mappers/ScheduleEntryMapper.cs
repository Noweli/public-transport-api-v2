using PublicTransportApi.Data.Models.DTOs;
using Riok.Mapperly.Abstractions;

namespace PublicTransportApi.Data.Models.Mappers;

[Mapper]
public partial class ScheduleEntryMapper
{
    [MapperIgnoreSource(nameof(ScheduleEntryDTO.SPLCorrelationId))]
    [MapperIgnoreTarget(nameof(ScheduleEntry.SPLCorrelation))]
    public partial ScheduleEntry DtoToModel(ScheduleEntryDTO scheduleEntryDto);
    
    [MapperIgnoreSource(nameof(ScheduleEntry.SPLCorrelation))]
    [MapperIgnoreTarget(nameof(ScheduleEntryDTO.SPLCorrelationId))]
    public partial ScheduleEntryDTO ModelToDto(ScheduleEntry scheduleEntry);
}