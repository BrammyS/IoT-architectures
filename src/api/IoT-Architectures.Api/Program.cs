using IoT_Architectures.Api.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterCore();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

const string corsPolicyName = "CustomCors";
builder.Services.AddCors(
    options =>
    {
        options.AddPolicy(
            name: corsPolicyName,
            policy =>
            {
                policy.WithOrigins(builder.Configuration["AllowedHosts"]?.Split(',') ?? new[] { "*" })
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
        );
    }
);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Enables the ability to read the raw body data in a api controller.
app.Use(
    (context, next) =>
    {
        context.Request.EnableBuffering();
        return next();
    }
);

app.UseHttpsRedirection();
app.UseCors(corsPolicyName);

app.UseAuthorization();
app.MapControllers();

app.Run();