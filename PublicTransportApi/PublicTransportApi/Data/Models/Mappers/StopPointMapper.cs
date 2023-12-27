using PublicTransportApi.Data.Models.DTOs;
using Riok.Mapperly.Abstractions;

namespace PublicTransportApi.Data.Models.Mappers;

[Mapper]
public partial class StopPointMapper
{
    public partial StopPoint DtoToStopPoint(StopPointDTO stopPointDTO);
}