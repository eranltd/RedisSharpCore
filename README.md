# RedisSharpCore  
Based on StackExchange.Redis - **Production Ready** - **Redis Client .Net Core**  

**Usage:**    
- Simple add this Class Library to your .NET Core Project
- Call Service Singleton - **IRedisService redisService = RedisService.Instance;**


**Basic API:**     
- redisService.SetValue("key", "value");  
- redisService.SetValue("tempkey", "value", TimeSpan.FromMinutes(1));    

- var keys = redisService.GetAllKeys();  
- var keys2 = redisService.GetAllDict();    

- Check out Interface at : https://github.com/eranltd/RedisSharpCore/blob/master/RedisSharp/IRedisService.cs  

**Store Objects as strings using JSON**  
public bool AddUpdate<T>(string key, T entity)  
        {  
            redisService.Store<T>(key, entity);  
            //var obj = Get<T>(key);  //check if insertion is successful  
            //if (obj == null)  
            //    return false;  
            return true;  
        }  

        public bool AddUpdate<T>(string key, T entity, TimeSpan expireIn)  
        {  
            redisService.Store<T>(key, entity, expireIn);  
            //var obj = Get<T>(key);  //check if insertion is successful  
            //if (obj == null)  
            //    return false;  
            return true;  
        }  

        public T Get<T>(string key) => redisService.Get<T>(key);   


Prerequisites:  

- Visual Studio 2017  
- .NET Core 2.1 and up  
- Redis Server(s)  
- StackExchange.Redis (via Nuget)  

How To Install Redis Server via Linux:  

> sudo apt-get update  
> sudo apt-get upgrade  
> sudo apt-get install redis-server  
> redis-cli -v  

Restart the Redis server to make sure it is running:  
> sudo service redis-server restart  

Execute a simple Redis command to verify your Redis server is running and available:  

$ redis-cli   
127.0.0.1:6379> set user:1 "Jane"  
127.0.0.1:6379> get user:1  
"Jane"  

To stop your Redis server:  
> sudo service redis-server stop  





**StackExchange.Redis Release Notes**

Overview (StackExchange.Redis)
StackExchange.Redis is a high performance general purpose redis client for .NET languages (C# etc). It is the logical successor to BookSleeve, and is the client developed-by (and used-by) Stack Exchange for busy sites like Stack Overflow.

Features
High performance multiplexed design, allowing for efficient use of shared connections from multiple calling threads
Abstraction over redis node configuration: the client can silently negotiate multiple redis servers for robustness and availability
Convenient access to the full redis feature-set
Full dual programming model both synchronous and asynchronous usage, without requiring “sync over async” usage of the TPL
Support for redis “cluster”
Installation
StackExchange.Redis can be installed via the nuget UI (as StackExchange.Redis), or via the nuget package manager console:

PM> Install-Package StackExchange.Redis


**Documentation

https://github.com/StackExchange/StackExchange.Redis
https://redislabs.com/blog/redis-on-windows-10/
