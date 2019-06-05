using RabbitMQ.Client;
using System;
using System.Text;

namespace RabbitMQSend_DirectEx
{
    class Program
    {
        static void Main(string[] args)
        {
            string exchangeName = "directExChange";
            string queueName = "hello";
            string routeKey = "helloRouteKey";
            //创建连接工厂
            ConnectionFactory factory = new ConnectionFactory
            {
                UserName = "xuejian",
                Password = "xuejian123",
                HostName = "10.2.230.124"
            };
               using (var connection = factory.CreateConnection()) {
                using (var channel = connection.CreateModel()) {
                    //定义一个Direct类型交换机
                    channel.ExchangeDeclare(exchangeName, ExchangeType.Direct, false, false, null);

                    //定义一个队列
                    channel.QueueDeclare(queueName, false, false, false, null);
                    channel.QueueDeclare("hello123", false, false, false, null);

                    //将队列绑定到交换机
                    channel.QueueBind(queueName, exchangeName, routeKey, null);
                    channel.QueueBind("hello123", exchangeName, routeKey, null);

                    Console.WriteLine($"\nRabbitMQ连接成功,Exchange：{exchangeName}，Queue：{queueName}，Route：{routeKey}，\n\n请输入消息，输入exit退出！");

                    string input;
                    do
                    {
                        input = Console.ReadLine();

                        var sendBytes = Encoding.UTF8.GetBytes(input);
                        //发布消息
                        channel.BasicPublish(exchangeName, routeKey, null, sendBytes);
                        //channel.BasicPublish(exchangeName, "123", null, sendBytes);

                    } while (input.Trim().ToLower() != "exit");

                }
            }

        }
    }
}
