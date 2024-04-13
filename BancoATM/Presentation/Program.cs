using Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddNewtonsoftJson();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<ITarjetaRepository, TarjetaRepository>();
builder.Services.AddScoped<ITarjetaService, TarjetaService>();
builder.Services.AddScoped<IATMRepository, ATMRepository>();
builder.Services.AddScoped<IATMService, ATMService>();



builder.Services.AddScoped<ICreditoTipoMapper, CreditoTipoMapper>();
builder.Services.AddScoped<ICreditoTipoRepository, CreditoTipoRepository>();
builder.Services.AddScoped<ICreditoTipoService, CreditoTipoService>();

builder.Services.AddScoped<ICreditoPlazoMapper, CreditoPlazoMapper>();
builder.Services.AddScoped<ICreditoPlazoRepository, CreditoPlazoRepository>();
builder.Services.AddScoped<ICreditoPlazoService, CreditoPlazoService>();




builder.Services.AddScoped<ICreditoPagosMapper, CreditoPagosMapper>();
builder.Services.AddScoped<ICreditoPagosRepository, CreditoPagosRepository>();
builder.Services.AddScoped<ICreditoPagosService, CreditoPagosService>();



builder.Services.AddScoped<ICreditoMapper, CreditoMapper>();
builder.Services.AddScoped<ICreditoRepository, CreditoRepository>();
builder.Services.AddScoped<ICreditoService, CreditoService>();


builder.Services.AddScoped<ITransaccionService, TransaccionService>();
builder.Services.AddScoped<ITransaccionRepository, TransaccionRepository>();

builder.Services.AddCors(options => options.AddPolicy("AllowWebapp",
                                    builder => builder.AllowAnyOrigin()
                                                    .AllowAnyHeader()
                                                    .AllowAnyMethod()));
builder.Services.AddDbContext<ApplicationDbContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("cadenaSQL"));
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowWebapp");
app.UseAuthorization();
app.MapControllers();
app.Run();
