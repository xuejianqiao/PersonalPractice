using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQSend
{
    class Program
    {
        static void Main(string[] args)
        {
            //创建连接工厂
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "xuejian",
                Password = "xuejian123",
                HostName = "10.2.230.217"
            };
            //创建连接
            using (var connection = factory.CreateConnection())
            {
                //创建通道
                using (var channel = connection.CreateModel())
                {
                    //申明一个队列
                    channel.QueueDeclare("hello", false, false, false, null);
                    Console.WriteLine("\nRabbitMQ连接成功，请输入消息，输入exit退出！");
                    string input;
                    do
                    {
                        input = Console.ReadLine();
                        var sendBytes = Encoding.UTF8.GetBytes(input);
                        //发布消息
                        channel.BasicPublish("", "hello", null, sendBytes);
                    } while (input.Trim().ToLower() != "exit");
                }
            }
        }
    }
}
