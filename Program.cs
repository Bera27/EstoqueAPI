using EstoqueAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<EstoqueDataContext>();

var app = builder.Build();
app.MapControllers();

app.Run();
