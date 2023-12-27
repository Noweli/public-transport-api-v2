using PublicTransportApi.Data.Models.DTOs;
using Riok.Mapperly.Abstractions;

namespace PublicTransportApi.Data.Models.Mappers;

[Mapper]
public partial class ScheduleEntryMapper
{
    public partial ScheduleEntry DtoToModel(ScheduleEntryDTO scheduleEntryDto);
    public partial ScheduleEntryDTO ModelToDto(ScheduleEntry scheduleEntry);
}