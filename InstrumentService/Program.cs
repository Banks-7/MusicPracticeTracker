using InstrumentService.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<InstrumentDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("InstrumentsDatabase")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<InstrumentDbContext>();
	dbContext.Database.Migrate();
}

app.MapControllers();

app.Run();
