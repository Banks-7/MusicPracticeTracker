using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PracticeService.Data;
using PracticeService.Models;

namespace PracticeService.Controllers;

[ApiController]
[Route("api/practice")]
public class PracticeController : ControllerBase
{
    private readonly PracticeDbContext _context;

    public PracticeController(PracticeDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<PracticeSession>>> GetPracticeSessions()
    {
        return await _context.PracticeSessions.ToListAsync();
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<PracticeSession>> GetPracticeSession(int id)
    {
        var practiceSession = await _context.PracticeSessions.FindAsync(id);

        if (practiceSession == null)
        {
            return NotFound();
        }

        return practiceSession;
    }

    [HttpGet("instrument/{instrumentId:int}")]
    public async Task<ActionResult<IEnumerable<PracticeSession>>> GetSessionsForInstrument(int instrumentId)
    {
        return await _context.PracticeSessions
            .Where(session => session.InstrumentId == instrumentId)
            .ToListAsync();
    }

    [HttpPost]
    public async Task<ActionResult<PracticeSession>> AddPracticeSession(PracticeSession practiceSession)
    {
        _context.PracticeSessions.Add(practiceSession);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetPracticeSession), new { id = practiceSession.Id }, practiceSession);
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeletePracticeSession(int id)
    {
        var practiceSession = await _context.PracticeSessions.FindAsync(id);

        if (practiceSession == null)
        {
            return NotFound();
        }

        _context.PracticeSessions.Remove(practiceSession);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}