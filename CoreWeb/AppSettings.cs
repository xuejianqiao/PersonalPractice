using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CoreWeb
{
    public class AppSettings : Singleton<AppSettings>
    {
        public IConfigurationRoot Config;
       
        public AppSettings() 
        {

        }
        public IConfigurationRoot GetConfigurationRoot(string jsonDir, string environmentName = null)
        {
           
            // 添加默认的 JSON 配置
            var builder = new ConfigurationBuilder().SetBasePath(jsonDir).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            // 根据环境变量添加相应的 JSON 配置文件
            if (!string.IsNullOrEmpty(environmentName))
            {
                builder = builder.AddJsonFile($"appsettings.{environmentName}.json", optional: true, reloadOnChange: true);
            }
            builder.AddEnvironmentVariables();
            // 返回构建成功的 IConfigurationRoot 对象
            Config = builder.Build();
            return Config;
        }

        public static string GetConfig(string name)
        {
            return Instance.Config.GetSection(name).Value;
        }

      
    }

    public class Singleton<T> where T : class, new()
    {
        private static T _instance;
        private static object _syncRoot = new Object();
        /// <summary>
                /// 单例实例
                /// </summary>
        public static T Instance
        {
            get
            {
                var instance = _instance;
                if (instance == null)
                {
                    lock (_syncRoot)
                    {
                        instance = Volatile.Read(ref _instance);
                        if (instance == null)
                        {
                            instance = new T();
                        }
                        Volatile.Write(ref _instance, instance);
                    }
                }
                return instance;
            }
        }
    }
}
