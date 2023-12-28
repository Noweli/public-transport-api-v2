using System.ComponentModel.DataAnnotations;

namespace PublicTransportApi.Data.Models;

public class Line
{
    public int Id { get; init; }
    [MaxLength(30)] public string? Identifier { get; set; }
    [MaxLength(120)] public string? Name { get; set; }
    public virtual ICollection<StopPointLineCorrelation> SPLCorrelations { get; } = new List<StopPointLineCorrelation>();
}