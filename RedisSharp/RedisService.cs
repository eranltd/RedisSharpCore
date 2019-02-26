using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RedisSharp
{
    public sealed class RedisService : IRedisService
    {
        #region Properties \ Vars

        private static volatile RedisService instance;
        private static object syncRoot = new Object();

        private readonly IDatabase dbClient;

        private readonly RedisRepository _repository = null; //init in c'tor (readonly keyword)
        private readonly string NamespacePrefix;


        public string RDSSchemaKey(string key) => NamespacePrefix + key;

        #endregion

        #region C'tor 

        public static RedisService Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new RedisService();
                    }
                }

                return instance;
            }
        }

        private RedisService()
        {
            _repository = RedisRepository.Instance;
            dbClient = _repository.GetDatabase();

            NamespacePrefix = RedisRepository.ReadSetting("RedisKeyPrefix") + ':';

        }

        ~RedisService()
        {
            //if (_repository != null)
            //    _repository.Redis.Dispose();
        }
        public void Dispose()
        {
            //if (_repository != null)
            //    _repository.Redis.Dispose();
        }
        #endregion

        #region Service Methods

        #region String Setters
        public bool SetValue(string key, string value)

            //inserting data
            => dbClient.StringSet(RDSSchemaKey(key), value);


        public bool SetValue(string key, string value, TimeSpan expire)

            //inserting data with expire
            => dbClient.StringSet(RDSSchemaKey(key), value, expire);


        public bool SetValueIfNotExists(string key, string value)
        {
            //inserting data
            if (!dbClient.KeyExists(key))
            {
                SetValue(key, value);
            }
            return dbClient.KeyExists(key);

        }

        public bool SetValueIfNotExists(string key, string value, TimeSpan expire)
        {
            //inserting data with expire
            //inserting data
            if (!dbClient.KeyExists(key))
            {
                SetValue(key, value, expire);
            }
            return dbClient.KeyExists(key);
        }

        public bool SetValueIfExists(string key, string value)
        {
            //inserting data
            if (dbClient.KeyExists(key))
            {
                SetValue(key, value);
            }
            return dbClient.KeyExists(key);
        }

        public bool SetValueIfExists(string key, string value, TimeSpan expireIn)
        {
            //inserting data with expire
            //inserting data
            if (dbClient.KeyExists(key))
            {
                SetValue(key, value, expireIn);
            }
            return dbClient.KeyExists(key);
        }


        #endregion

        #region Object Setters

        public void Store<T>(string id, T entity)
        {
            var valueString = JsonConvert.SerializeObject(entity);
            SetValue(id, valueString);
        }

        public void Store<T>(string id, T entity, TimeSpan expireIn)
        {
            var valueString = JsonConvert.SerializeObject(entity, Formatting.Indented);
            SetValue(id, valueString, expireIn);
        }

        public T Get<T>(string key)
        {
            var valueString = dbClient.StringGet(RDSSchemaKey(key));
            var value = JsonConvert.DeserializeObject<T>(valueString);
            return value;
        }

        #endregion

        #region Object Getters




        #endregion

        //Getters
        public IEnumerable<RedisKey> GetAllKeys()
        {
            IEnumerable<RedisKey> enumerable = _repository.GetAllKeys(NamespacePrefix);

            return new List<RedisKey>(enumerable);
        }



        public IDictionary<string, string> GetAllDict()
        {
            var RedisCollection = new Dictionary<string, string>();
            var keysInDB = GetAllKeys();
            foreach (var key in keysInDB)
            {
                string value = GetItem(key, true);
                if (value != null)
                {
                    string newKey = key;
                    newKey = newKey.Remove(newKey.IndexOf(NamespacePrefix), NamespacePrefix.Length);
                    RedisCollection.Add(newKey, value);
                }
            }

            return RedisCollection;
        }

        //return specific item
        public string GetItem(string key, bool withPrefix = false)
        {
            string readData = default(string);
            if (withPrefix)
            {
                readData = dbClient.StringGet(key);
            }
            else
            {
                readData = dbClient.StringGet(RDSSchemaKey(key));
            }
            return readData;
        }



        public string FindKeyByValue(string value)
        {
            var allKeyValuePair = GetAllDict();
            var match = allKeyValuePair.FirstOrDefault(pair => pair.Value == value);
            return match.Key;
        }

        //Removers

        //remove all items
        public void RemoveAllItemsInDB()
        {
            _repository.FlushDatabase();
        }

        //remove specific item
        public bool RemoveItem(string key) => dbClient.KeyDelete(key == null ? "" : RDSSchemaKey(key));

        public bool RemoveItemByValue(string value)
        {
            var keyToRemove = FindKeyByValue(value);
            return RemoveItem(keyToRemove);
        }

        #endregion


    }
}
