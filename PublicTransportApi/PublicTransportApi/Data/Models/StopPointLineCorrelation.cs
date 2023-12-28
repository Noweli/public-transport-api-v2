namespace PublicTransportApi.Data.Models;

public class StopPointLineCorrelation
{
    public int Id { get; set; }
    public Line? Line { get; set; }
    public StopPoint? StopPoint { get; set; }
    public virtual ICollection<ScheduleEntry> ScheduleEntries { get; } = new List<ScheduleEntry>();
}