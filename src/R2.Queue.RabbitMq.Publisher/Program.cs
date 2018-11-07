using RabbitMQ.Client;
using System;
using System.Text;

namespace R2.Queue.RabbitMq.Publisher
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.Write($"Enter a message:");
                var message = Console.ReadLine();

                var factory = new ConnectionFactory { HostName = "localhost" };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    //Create queue
                    channel.QueueDeclare(
                        "r2queue",
                        false,
                        false,
                        false,
                        null);
                    
                    var body = Encoding.UTF8.GetBytes(message);

                    //Send message
                    channel.BasicPublish(
                        "r2Exchange",
                        "r2routingkey1",
                        null,
                        body);

                    Console.Write(Environment.NewLine);
                    Console.WriteLine($"Message {message} sent!");
                }
            }
            catch (Exception e)
            {
                Console.Write(Environment.NewLine);
                Console.WriteLine($"Error {e}");
            }

            Console.Write(Environment.NewLine);
            Console.WriteLine($"Press to finish!");
            Console.ReadKey();
        }
    }
}