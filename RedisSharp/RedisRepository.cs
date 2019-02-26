using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace RedisSharp
{
    public sealed class RedisRepository
    {
        private readonly string _redisIP1;
        private readonly string _redisIP2;
        private readonly string _redisIP3;
        private readonly int _serverPort;
        private readonly int _databaseNumber;
        private readonly string _password;
        private static volatile RedisRepository instance; //volatile for multi-threading..
        private static object syncRoot = new Object();

        public ConnectionMultiplexer Redis { get; }

        public RedisRepository()
        {
            try
            {
                _redisIP1 = ReadSetting("RedisServerIP1");
                _redisIP2 = ReadSetting("RedisServerIP2");
                _redisIP3 = ReadSetting("RedisServerIP3");
                _password = ReadSetting("RedisServerPassword");

                Int32.TryParse(ReadSetting("RedisServerPort"), out int serverPort);
                _serverPort = serverPort;

                Int32.TryParse(ReadSetting("RedisDatabase"), out int redisDBNumber);
                _databaseNumber = redisDBNumber;


                var configurationOptions = new ConfigurationOptions
                {
                    EndPoints =
                    {
                        {_redisIP1, _serverPort},
                        {_redisIP2, _serverPort},
                        {_redisIP3, _serverPort}
                    },
                    KeepAlive = 90,
                    Password = _password,
                    AllowAdmin = true //** Needed for cache clear
                };
                Redis = ConnectionMultiplexer.Connect(configurationOptions);
            }
            catch (Exception ex)
            {
                throw new Exception("Can not access to RedisDB server.", ex);
            }
        }

        public IDatabase GetDatabase() => Redis.GetDatabase(_databaseNumber);

        public void FlushDatabase()
        {

            var endpoints = Redis.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = Redis.GetServer(endpoint);
                if (!server.IsSlave)
                    server.FlushDatabase(_databaseNumber);
            }
        }

        public IEnumerable<RedisKey> GetAllKeys(string NamespacePrefix)
        {
            var endpoints = Redis.GetEndPoints(true);
            foreach (var endpoint in endpoints)
            {
                var server = Redis.GetServer(endpoint);
                if (server.IsConnected)
                    return server.Keys(_databaseNumber, pattern: $"{NamespacePrefix}*");
            }
            return default(IEnumerable<RedisKey>);
        }


        public static RedisRepository Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RedisRepository();
                    }
                }

                return instance;
            }
        }

        //read from App.Config.
        public static string ReadSetting(string key)
        {
            try
            {
                string RedisServiceSettingsSectionName = "RedisSettings";
                System.Collections.Specialized.NameValueCollection R2RedisServiceSettings =
                    ConfigurationManager.GetSection(RedisServiceSettingsSectionName) as System.Collections.Specialized.NameValueCollection;

                var appSettings = R2RedisServiceSettings;
                string result = appSettings != null ? appSettings[key] : "";
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                throw ex;
            }
        }
    }
}
