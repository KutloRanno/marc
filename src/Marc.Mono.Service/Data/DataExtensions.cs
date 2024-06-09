using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using Marc.Mono.Service.Entities;
using Marc.Mono.Service.Repositories;
using Marc.Mono.Service.Settings;
using Marc.Mono.Service.Logging;

namespace Marc.Mono.Service.Data;

public static class DataExtensions
{

        public static IServiceCollection AddMongo(this IServiceCollection services)
        {
            BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));
            BsonSerializer.RegisterSerializer(new DateTimeOffsetSerializer(BsonType.String));

            //AddSingleton makes sure there is only one instance of the mongodb database throughout the code and any class
            //that needs the database will access it through this instance we are defining below

            services.AddSingleton(serviceProvider=>
            {
                var configuration = serviceProvider.GetService<IConfiguration>();
                var serviceSettings = configuration.GetSection(nameof(ServiceSettings)).Get<ServiceSettings>();
                var mongoDbSettings = configuration.GetSection(nameof(MongoDbSettings)).Get<MongoDbSettings>();
                var mongoClient = new MongoClient(mongoDbSettings.ConnectionString);

                return  mongoClient.GetDatabase(serviceSettings.ServiceName);
            });

            return services;
        }

        public static IServiceCollection AddRepository<T>(this IServiceCollection services,string collectionName) where T:IEntity
        {
            //registering the dependency.
            services.AddSingleton<IRepository<T>>(serviceProvider =>
            {
                var database =serviceProvider.GetService<IMongoDatabase>();
                return new MongoRepository<T>(database,collectionName);
            });

            return services;
        }

        public static IServiceCollection AddCsvLogger(this IServiceCollection services){
            services.AddSingleton<ILogger,CsvLogger>(serviceProvider=>{
                var configuration = serviceProvider.GetService<IConfiguration>();
                return new CsvLogger(configuration);
            });
            return services;
        }


}