using Sander.TestTask.Services;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddConfigurationSources();
builder.Services.AddControllers()
    .AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddSwaggerGen();
builder.Services.AddConfiguration(builder.Configuration);
builder.Services.AddSqlServer();
builder.Services.AddRepositories();
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Sander.TestTask.Application.QueryHandlers.IQueryHandler<,>).Assembly));
builder.Services.AddMemoryCache();
builder.Logging.AddConsole();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("error");
}

app.UseAuthorization();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.Run();