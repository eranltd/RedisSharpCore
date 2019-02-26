using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace RedisSharp
{
    public interface IRedisService
    {
        //** String Setters

        bool SetValue(string key, string value);

        bool SetValue(string key, string value, TimeSpan expire);

        bool SetValueIfNotExists(string key, string value);

        bool SetValueIfNotExists(string key, string value, TimeSpan expire);

        bool SetValueIfExists(string key, string value);

        bool SetValueIfExists(string key, string value, TimeSpan expireIn);

        //** Object Setters
        /// <summary>
        /// serialize object and store it in given id (we work with redis with key,value pairs)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        void Store<T>(string id, T entity);
        /// <summary>
        /// serialize object and store it in given id (we work with redis with key,value pairs) with TIMEOUT
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <param name="expireIn"></param>
        void Store<T>(string id, T entity, TimeSpan expireIn);



        //** Object Getters
        /// <summary>
        /// deserialize object by given key
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="id"></param>
        /// <returns></returns>
        T Get<T>(string key);


        //** String Getters

        /// <summary>
        /// return all keys in database
        /// </summary>
        /// <returns></returns>
        IEnumerable<RedisKey> GetAllKeys();


        /// <summary>
        /// return all keyValue Pairs in database
        /// </summary>
        /// <returns></returns>
        IDictionary<string, string> GetAllDict();

        /// <summary>
        /// return specific  item by string key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetItem(string key, bool withPrefix = false);
        /// <summary>
        /// finds key in redis within its value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        /// 
        string FindKeyByValue(string value);


        //** Removal Methods 
        /// <summary>
        /// Clear All Items in DB
        /// </summary>
        void RemoveAllItemsInDB();

        /// <summary>
        /// remove item by key
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool RemoveItem(string key);


        /// <summary>
        /// remove item by value
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        bool RemoveItemByValue(string value);

    
}
}
