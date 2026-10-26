using Microsoft.EntityFrameworkCore;
using PracticeService.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<PracticeDbContext>(options =>
	options.UseSqlite(builder.Configuration.GetConnectionString("PracticeDatabase")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	var dbContext = scope.ServiceProvider.GetRequiredService<PracticeDbContext>();
	dbContext.Database.Migrate();
}

app.MapControllers();

app.Run();
