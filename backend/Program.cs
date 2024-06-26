using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddControllers();
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<backend.services.interfaces.ICompraService, backend.services.implementations.CompraService>();
builder.Services.AddScoped<backend.services.interfaces.IPagoService, backend.services.implementations.PagoService>();
builder.Services.AddScoped<backend.services.interfaces.ITarjetaDeCreditoService, backend.services.implementations.TarjetaDeCreditoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();