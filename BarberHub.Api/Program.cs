using Asp.Versioning;
using BarberHub.Api.Contracts.Barber;
using BarberHub.Api.Middleware;
using BarberHub.Application;
using BarberHub.Infrastructure;
using FluentValidation;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplications();

builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddApiVersioning(options =>
    {
        options.DefaultApiVersion = new ApiVersion(1, 0);
        options.AssumeDefaultVersionWhenUnspecified = true;
        options.ReportApiVersions = true;
        options.ApiVersionReader = new UrlSegmentApiVersionReader();
    }
).AddApiExplorer(options =>
    {
        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    }
);

var app = builder.Build();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapScalarApiReference(options =>
    {
        options.WithTitle("BarberHub API");

        // var descriptions = app.DescribeApiVersions();
        //
        // for (var index = 0; index < descriptions.Count; index++)
        // {
        //     var description = descriptions[index];
        //     var isDefault = index == 0;
        //
        //     options.AddDocument(
        //         description.GroupName,
        //         description.GroupName.ToUpperInvariant(),
        //         isDefault: isDefault);
        // }
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();