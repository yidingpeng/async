﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using Mapster;
using MapsterMapper;
using LsWebsocketClient;
using RW.Position.Common;
using RW.Position.websocketServers;
using RW.Position.events;
using System.Linq;
using System.Net.Sockets;
using MediatR;

namespace RW.Position.Extensions
{
    public static class ConfigurationService
    {
        public static IServiceCollection Services;
        public static IConfiguration config;
        public static IServiceProvider Injection()
        {
              config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .Build();
            Services = new ServiceCollection();
            var serviceProvider = Services
                .AddLogging(logBuilder =>
                {
                    logBuilder.AddConfiguration(config.GetSection("Logging"));
                    //logBuilder.AddConsole();
                })
                .AddCustomServices(config)
                .BuildServiceProvider();

            return serviceProvider;
        }

        public static IServiceCollection AddCustomServices(this IServiceCollection service, IConfiguration config)
        {
            // 注入FreeSql
           
            var dbStr = config["MySqlConnectionStrings:DbConnectionStr"];
           
            Func<IServiceProvider, IFreeSql> fsqlFactory = r =>
            {
                IFreeSql fsql = new FreeSql.FreeSqlBuilder()
                    .UseConnectionString(FreeSql.DataType.MySql, dbStr)
                    //.UseMonitorCommand(cmd => Console.WriteLine($"Sql：{cmd.CommandText}"))//监听SQL语句
                    .UseAutoSyncStructure(true) //自动同步实体结构到数据库，FreeSql不会扫描程序集，只有CRUD时才会生成表。
                    .Build();
                return fsql;
            };
            // 创建 Socket 对象
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            // 连接服务器
            socket.Connect(config["TcpSocketConnectionStrings:ip"], int.Parse(config["TcpSocketConnectionStrings:Port"]));
            service.AddSingleton(fsqlFactory);
            // Mapster注入
            var mapsterConfig = new TypeAdapterConfig();
            // Or
            // var config = TypeAdapterConfig.GlobalSettings;
            service.AddSingleton(mapsterConfig);
            service.AddSingleton<OnDemandSubscription>();
            service.AddSingleton<Publisher>();
            service.AddMediatR(typeof(Program));
            service.AddSingleton<WebSocketServerConfig>();
            service.AddSingleton(socket);
            service.AddScoped<IMapper, ServiceMapper>();
            var assembly = typeof(OnMessagePOSServers).Assembly;
            var types = assembly.GetTypes().Where(t => t.Namespace == "RW.Position.websocketServers");

            // 注册所有类
            foreach (var type in types)
            {
                service.AddSingleton(type);
            }
            return service;
        }
    }
}
