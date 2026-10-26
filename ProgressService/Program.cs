var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddHttpClient("PracticeService", client =>
{
	var baseUrl = builder.Configuration["PracticeService:BaseUrl"] ?? "http://127.0.0.1:5101/";
	client.BaseAddress = new Uri(baseUrl);
});

var app = builder.Build();

app.MapControllers();

app.Run();
