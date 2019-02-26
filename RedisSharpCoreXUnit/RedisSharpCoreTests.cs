using RedisSharp;
using System;
using Xunit;

namespace RedisSharpCoreXUnit
{
    public class RedisSharpCoreTests
    {
        [Fact]
        public void TestRedisLibrary()
        {
            IRedisService redisService = RedisService.Instance;


            //redisService.SetValue("key", "value");

            //redisService.SetValue("tempkey", "value", TimeSpan.FromMinutes(1));
            //var keys = redisService.GetAllKeys();
            //var keys2 = redisService.GetAllDict();
            //redisService.RemoveItem("key");
            //redisService.RemoveItem("tempkey");
            //var value = redisService.FindKeyByValue("value");
            //redisService.RemoveItemByValue("value");
            //var obj = redisService.GetItem<string>("key"); 
            //var obj2 = redisService.GetItemString("key"); 
            //redisService.SetValueIfNotExists("key", "s");
            //redisService.SetValueIfNotExists("key","s", TimeSpan.FromMinutes(1));
            //redisService.SetValueIfExists(string key, string value)
            //redisService.SetValueIfExists(string key, string value, TimeSpan expireIn)
            //var keys = redisService.GetAllKeys();
            //var pairs =  redisService.GetAll();
            //var key =  redisService.GetItem("key");
            redisService.RemoveAllItemsInDB(); 
        }
    }
}
