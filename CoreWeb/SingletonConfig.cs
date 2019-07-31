using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreWeb
{
    public sealed class SingletonConfig
    {
        private static SingletonConfig instance = null;
        private static readonly object padlock = new object();
       
        SingletonConfig() { }

       
        public static SingletonConfig Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (padlock)
                    {
                        if (instance == null)
                        {
                            instance = new SingletonConfig();
                        }
                    }
                }
                return instance;
            }
        }
    }
}
