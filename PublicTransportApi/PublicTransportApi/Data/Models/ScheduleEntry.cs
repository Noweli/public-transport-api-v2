namespace PublicTransportApi.Data.Models;

public class ScheduleEntry
{
    public int Id { get; set; }
    public bool IsRecurring { get; set; }
    public string? RecurringDays { get; set; }
    public DateTime DateTime { get; set; }
    public StopPointLineCorrelation? SPLCorrelation { get; set; }
}