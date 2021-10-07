using Consul;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace Ray.JdTool
{
    public static class ConsulRegistryHelper
    {
        public static void ConsulRegistry(this IConfiguration configuration)
        {
            try
            {
                var section = configuration.GetSection("Consul");

                string ip = section["Ip"];
                string port = section["Port"];
                string weight = section["Weight"];
                string consulAddress = section["ConsulAddress"];
                string consulDatacenter = section["ConsulDatacenter"];

                if (consulAddress.IsNullOrWhiteSpace())
                {
                    return;
                }

                ConsulClient consulClient = new ConsulClient(c =>
                {
                    c.Address = new Uri(consulAddress);
                    c.Datacenter = consulDatacenter;
                });

                var re = consulClient.Agent.ServiceRegister(new AgentServiceRegistration
                {
                    ID = $"JdApiService{Guid.NewGuid()}",
                    Name = "JdApiService",
                    Address = ip,
                    Port = int.Parse(port),
                    Tags = new string[] { weight },
                    Check = new AgentServiceCheck
                    {
                        Interval = TimeSpan.FromSeconds(12),
                        HTTP = $"http://{ip}:{port}/api/app/health-check",
                        Timeout = TimeSpan.FromSeconds(5),
                        DeregisterCriticalServiceAfter = TimeSpan.FromSeconds(20)
                    }
                }).Result;

                Log.Logger.Information($"{JsonSerializer.Serialize(re)}");
                Log.Logger.Information($"{ip}:{port}--weight:{weight}");
            }
            catch (Exception ex)
            {
                Log.Logger.Information($"Consul注册：{ex.Message}");
            }
        }
    }
}
