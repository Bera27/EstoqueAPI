using EstoqueAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressModelStateInvalidFilter = true;
                });
                
builder.Services.AddDbContext<EstoqueDataContext>();

var app = builder.Build();
app.MapControllers();

app.Run();
