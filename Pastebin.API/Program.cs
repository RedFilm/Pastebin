using Pastebin.API.Filters;
using Pastebin.API.Handlers;
using Pastebin.Application.CQRS.Commands;
using Pastebin.Persistence.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services
	.AddPersistence(builder.Configuration);

builder.Services.AddMediatR(cfg =>
	cfg.RegisterServicesFromAssembly(typeof(CreatePasteCommand).Assembly));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

builder.Services.AddHttpContextAccessor();

builder.Services.AddSwaggerGen(c =>
{
	c.OperationFilter<BaseProducesResponseTypeFilter>();
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();



app.UseExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
