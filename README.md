# RedisSharpCore
Based on StackExchange.Redis - Production Ready - Redis Client .Net Core

Usage:

Prerequisites:









https://github.com/StackExchange/StackExchange.Redis

https://redislabs.com/blog/redis-on-windows-10/


StackExchange.Redis
Release Notes

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


#Documentation
Basic Usage - getting started and basic usage
Configuration - options available when connecting to redis
Pipelines and Multiplexers - what is a multiplexer?
Keys, Values and Channels - discusses the data-types used on the API
Transactions - how atomic transactions work in redis
Events - the events available for logging / information purposes
Pub/Sub Message Order - advice on sequential and concurrent processing
Where are KEYS / SCAN / FLUSH*? - how to use server-based commands
Profiling - profiling interfaces, as well as how to profile in an async world
Scripting - running Lua scripts with convenient named parameter replacement
Testing - running the StackExchange.Redis.Tests suite to validate changes
