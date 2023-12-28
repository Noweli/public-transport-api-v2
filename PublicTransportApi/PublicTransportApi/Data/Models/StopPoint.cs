using System.ComponentModel.DataAnnotations;

namespace PublicTransportApi.Data.Models;

public class StopPoint
{
    public int Id { get; init; }
    [MaxLength(30)] public string? Lat { get; set; }
    [MaxLength(30)] public string? Long { get; set; }
    [MaxLength(30)] public string? Identifier { get; set; }
    [MaxLength(120)] public string? Name { get; set; }
    [MaxLength(120)] public string? StreetName { get; set; }
    public virtual ICollection<StopPointLineCorrelation> SPLCorrelation { get; } = new List<StopPointLineCorrelation>();
}