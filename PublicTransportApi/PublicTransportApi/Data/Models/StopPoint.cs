using System.ComponentModel.DataAnnotations;

namespace PublicTransportApi.Data.Models;

public class StopPoint
{
    public int Id { get; init; }
    [MaxLength(30)] public string? Lat { get; init; }
    [MaxLength(30)] public string? Long { get; init; }
    [MaxLength(30)] public string? Identifier { get; init; }
    [MaxLength(120)] public string? Name { get; init; }
    [MaxLength(120)] public string? StreetName { get; set; }
    public ICollection<StopPointLineCorrelation> SPLCorrelation { get; } = new List<StopPointLineCorrelation>();
}