using Marc.Mono.Service.Controllers;
using Marc.Mono.Service.Data;
using Marc.Mono.Service.Entities;
using Marc.Mono.Service.Settings;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using MongoDB.Driver;
using MongoDB.Driver.Core.Configuration;
using Serilog;

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

// builder.Services.AddLogging();                
// builder.Services.AddCsvLogger();

//create collection for loggin
// var serviceProvider = builder.Services.BuildServiceProvider();
// var mongoDatabase = serviceProvider.GetRequiredService<IMongoDatabase>();
// var mongoCollection = mongoDatabase.GetCollection<BsonDocument>("logs");

//attempt to build my database url here

//first I will get the configuration object here to use to build my database url
/*var databaseUrl = builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetService<IConfiguration>();
    return configuration;
});
*/




builder.Host.UseSerilog((context, configuration) =>
{
    //I want to build the mongodb object to use to write logs to mongodb here


    configuration.ReadFrom.Configuration(context.Configuration).WriteTo.MongoDB("mongodb://localhost:27017/Sports","logs");

                                                   
});

// builder.Host.AddMyLog();





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

app.UseSerilogRequestLogging();//added by me

app.UseHttpsRedirection();

app.UseCors("myAppCors");

app.MapControllers();

app.Run();


