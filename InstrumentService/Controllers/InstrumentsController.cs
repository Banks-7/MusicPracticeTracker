using InstrumentService.Data;
using InstrumentService.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace InstrumentService.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InstrumentsController : ControllerBase
{
    private readonly InstrumentDbContext _context;

    public InstrumentsController(InstrumentDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Instrument>>> GetInstruments()
    {
        return await _context.Instruments.ToListAsync();
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Instrument>> GetInstrument(int id)
    {
        var instrument = await _context.Instruments.FindAsync(id);

        if (instrument == null)
        {
            return NotFound();
        }

        return instrument;
    }

    [HttpPost]
    public async Task<ActionResult<Instrument>> AddInstrument(Instrument instrument)
    {
        _context.Instruments.Add(instrument);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetInstrument), new { id = instrument.Id }, instrument);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteInstrument(int id)
    {
        var instrument = await _context.Instruments.FindAsync(id);

        if (instrument == null)
        {
            return NotFound();
        }

        _context.Instruments.Remove(instrument);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}