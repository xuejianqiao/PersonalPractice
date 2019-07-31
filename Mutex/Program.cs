using System;
using System.Threading;
using static System.Console;
namespace MutexClass
{ 
    class Program
    {
       /* private static Mutex mutex = null; */ //设为Static成员，是为了在整个程序生命周期内持有Mutex

        static void Main()
        {
            for (int i= 0;i<2;i++) {
                var a = i.ToString();
                Thread th = new Thread(()=>TestMutex.Get(a));
                th.Start();
            }
            Console.ReadLine();
        }
    }


    
    public  class TestMutex
    {

       private  const string MutexName = @"MutexSampleApp";


        public static void  Get(string name)
        {
            using (Mutex mutex = new Mutex(false, MutexName))
            {
                bool conrol = mutex.WaitOne(TimeSpan.FromSeconds(10), false);
                try
                {
                    if (conrol)
                    {
                        Thread.Sleep(20000);
                        Console.WriteLine(name + "获取到信号量");
                    }
                    else
                    {
                        Console.WriteLine(name + "没有获取到信号量");
                    }
                }
                catch (Exception ex)
                {


                }
                finally
                {
                    if (conrol)
                    {
                        mutex.ReleaseMutex();
                    }
                    //mutex.Close();
                }
            }
           
        }


    }
}
