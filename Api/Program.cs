using Api.Configuration;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables();

builder.Services.AddControllers();

builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

builder.Services.AdicionarSwagger();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

builder.Services.ConfigurarServicos();
builder.RegistrarAutenticaco();

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
