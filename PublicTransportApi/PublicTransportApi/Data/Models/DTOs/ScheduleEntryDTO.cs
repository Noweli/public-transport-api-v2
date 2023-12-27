namespace PublicTransportApi.Data.Models.DTOs;

public class ScheduleEntryDTO
{
    public bool IsRecurring { get; set; }
    public string? RecurringDays { get; set; }
    public DateTime DateTime { get; set; }
    public int? SPLCorrelationId { get; set; }
}