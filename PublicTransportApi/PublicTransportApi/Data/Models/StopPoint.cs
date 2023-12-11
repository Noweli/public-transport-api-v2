namespace PublicTransportApi.Data.Models;

public class StopPoint
{
    public int Id { get; init; }
    public int Lat { get; init; }
    public int Long { get; init; }
    public string? Identifier { get; init; }
    public string? Name { get; init; }
}