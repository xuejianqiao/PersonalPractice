using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NetCoreConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            var a = "D://project//PersonalPractice//WebApplication1";

            var b = a.Split("//");

            List<string> listS = new List<string>(b);

            listS.RemoveAt(listS.Count - 1);
            listS.RemoveAt(listS.Count - 1);

            var c = string.Join("//", listS);
            //SendRequest();
        }

        public static async Task SendRequest()
        {
            Console.WriteLine("Starting reqeust");
            for (int i = 0; i < 100; i++)
            {
                using (var client = new HttpClient())
                {
                    var result = await client.GetAsync("http://www.baidu.com");
                    Console.WriteLine(result.StatusCode);
                }
            }
            Console.WriteLine("Reqeust done");
        }
    }



}
