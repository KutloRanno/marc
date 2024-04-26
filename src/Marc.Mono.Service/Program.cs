using Marc.Mono.Service.Controllers;
using Marc.Mono.Service.Data;
using Marc.Mono.Service.Entities;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

var allowedOrigins = builder.Configuration.GetSection("AllowedOrigins").Get<string[]>();


// Add services to the container.
builder.Services.AddControllers(options =>
            {
                options.SuppressAsyncSuffixInActionNames=false;
            });

// builder.Services.AddScoped<SportsController>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMongo()
                .AddRepository<Sport>("Sports")
                .AddRepository<User>("Users")
                .AddRepository<Admin>("Admins");


//now to enable CORS in the app
builder.Services.AddCors(options=>
{
    options.AddPolicy("myAppCors",policy=>
    {
        policy.WithOrigins(allowedOrigins)
        .AllowAnyHeader()
        .AllowAnyMethod();
});
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("myAppCors");

app.MapControllers();

app.Run();


