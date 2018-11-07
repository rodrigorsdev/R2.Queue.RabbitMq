﻿using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Text;

namespace R2.Queue.RabbitMq.Consumer1
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var factory = new ConnectionFactory { HostName = "localhost" };

                using (var connection = factory.CreateConnection())
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare(
                        "r2queue",
                        false,
                        false,
                        false,
                        null);

                    var consumer = new EventingBasicConsumer(channel);

                    consumer.Received += (model, ea) =>
                    {
                        var body = ea.Body;
                        var message = Encoding.UTF8.GetString(body);
                        Console.WriteLine($"Received: {message}");
                    };
                    
                    channel.BasicConsume(
                        "r2queue",
                        true,
                        consumer);
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
