using System.ComponentModel.DataAnnotations;

namespace PublicTransportApi.Data.Models;

public class ScheduleEntry
{
    public int Id { get; set; }
    public bool IsRecurring { get; set; }
    [MaxLength(15)] public string? RecurringDays { get; set; }
    public DateTime DateTime { get; set; }
    public StopPointLineCorrelation? SPLCorrelation { get; set; }
}