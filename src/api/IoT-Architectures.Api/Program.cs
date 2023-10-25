using IoT_Architectures.Api.Core;

var builder = WebApplication.CreateBuilder(args);

builder.Services.RegisterCore();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var origins = builder.Configuration["AllowedHosts"]?.Split(',') ?? new[] { "*" };
builder.Services.AddCors(
    options => { options.AddDefaultPolicy(policy => { policy.WithOrigins(origins).AllowAnyHeader().AllowAnyMethod(); }); }
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
app.UseStaticFiles();
app.UseRouting();

app.UseCors();

app.UseAuthorization();
app.MapControllers();

app.Run();