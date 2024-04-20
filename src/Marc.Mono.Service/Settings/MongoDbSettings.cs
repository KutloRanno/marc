namespace Marc.Mono.Service.Settings;

 public class MongoDbSettings
    {
        public string Host { get; init; }//init makes them to be only set once and not modified by any code in my codebase
        public int Port { get; init; }
        public string ConnectionString =>$"mongodb://{Host}:{Port}";
        
    }