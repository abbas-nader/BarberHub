using BarberHub.Api.Contracts.Barber;
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