﻿using System;
using System.IO;
using System.Reflection;
using ChickenAPI.DAL.Interfaces;
using ChickenAPI.Dtos;
using ChickenAPI.Enums;
using ChickenAPI.Packets;
using ChickenAPI.Plugin;
using ChickenAPI.Utils;
using log4net;
using log4net.Config;
using log4net.Repository;
using NosSharp.DatabasePlugin;
using NosSharp.RedisSessionPlugin;
using NosSharp.World.Cryptography;
using NosSharp.World.Network;
using NosSharp.World.Packets;
using NosSharp.World.Session;
using NosSharp.World.Utils;

namespace NosSharp.World
{
    internal class WorldServer
    {
        private static void InitializePlugins()
        {
            DependencyContainer.Instance.Register<IPluginManager>(new SimplePluginManager());
            var tmp = new RedisPlugin();
            tmp.OnLoad();
            tmp.OnEnable();
            var tmpAgain = new NosSharpDatabasePlugin();
            tmpAgain.OnLoad();
            tmpAgain.OnDisable();
            IPlugin[] plugins = DependencyContainer.Instance.Get<IPluginManager>().LoadPlugins(new DirectoryInfo("plugins"));
            if (plugins == null)
            {
                return;
            }

            foreach (IPlugin plugin in plugins)
            {
                plugin.OnEnable();
            }
        }

        private static void InitializeConfigs()
        {
            Environment.SetEnvironmentVariable("io.netty.allocator.type", "unpooled");
            Environment.SetEnvironmentVariable("io.netty.allocator.maxOrder", "5");
            string port = Environment.GetEnvironmentVariable("SERVER_PORT");
            if (!int.TryParse(port, out int intPort))
            {
                Server.Port = 1337;
            }

            Server.Port = intPort;
            Server.Ip = Environment.GetEnvironmentVariable("SERVER_IP") ?? "127.0.0.1";
            Server.WorldGroup = Environment.GetEnvironmentVariable("SERVER_WORLDGROUP") ?? "NosWings";
        }

        private static void PrintHeader()
        {
            Console.Title = "Nos# - WORLD";
            const string text = "WORLD SERVER - Nos#";
            int offset = Console.WindowWidth / 2 + text.Length / 2;
            string separator = new string('=', Console.WindowWidth);
            Console.WriteLine(separator + string.Format("{0," + offset + "}\n", text) + separator);
        }

        private static void InitializeLogger()
        {
            ILoggerRepository logRepository = LogManager.GetRepository(Assembly.GetEntryAssembly());
            XmlConfigurator.Configure(logRepository, new FileInfo("plugins/config/log4net.config"));
            Logger.Log = LogManager.GetLogger(typeof(WorldServer));
        }


        private static void Main()
        {
            PrintHeader();
            InitializeLogger();
            InitializeConfigs();
            DependencyContainer.Instance.Register<IPacketFactory>(new PluggablePacketFactory());
            DependencyContainer.Instance.Register<IPacketHandler>(new PacketHandler());
            InitializePlugins();
            ClientSession.SetPacketFactory(DependencyContainer.Instance.Get<IPacketFactory>());
            ClientSession.SetPacketHandler(DependencyContainer.Instance.Get<IPacketHandler>());
            if (Server.RegisterServer())
            {
                Logger.Log.Info($"Failed to register to ServerAPI");
                return;
            }
            Server.RunServerAsync(1337, new WorldCryptoFactory()).Wait();
            Server.UnregisterServer();
        }
    }
}