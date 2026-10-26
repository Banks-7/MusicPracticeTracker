namespace PracticeService.Models;

public class PracticeSession
{
    public int Id { get; set; }

    public int InstrumentId { get; set; }

    public DateTime Date { get; set; }

    public int DurationMinutes { get; set; }
}