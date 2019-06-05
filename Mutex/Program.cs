using System;
using System.Threading;
using static System.Console;
namespace MutexClass
{ 
    class Program
    {
        private static Mutex mutex = null;  //设为Static成员，是为了在整个程序生命周期内持有Mutex

        static void Main()
        {
            bool firstInstance;
            mutex = new Mutex(true, @"Global\MutexSampleApp", out firstInstance);
            try
            {
                if (!firstInstance)
                {

                    Console.WriteLine("已有实例运行，输入回车退出……");
                    Console.ReadLine();
                    return;

                }
                else
                {
                    Console.WriteLine("我们是第一个实例！");
                    Thread.Sleep(10000);
                }
            }
            finally
            {
                if (firstInstance)
                {

                    mutex.ReleaseMutex();

                }
                mutex.Close();
                mutex = null;
            }
        }
    }
}
