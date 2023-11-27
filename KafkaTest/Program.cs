using System;
using System.Text.RegularExpressions;
using Confluent.Kafka;
using Newtonsoft.Json;
using Produce.Models;

class Program
{
    static void Main()
    {
        // Kafka Configuration
        var config = new ProducerConfig { BootstrapServers = "localhost:9092" };

        // Producer
        using (var producer = new ProducerBuilder<Null, string>(config).Build())
        {
            Console.WriteLine("Producer : ");

            for (int i = 0; i < 10; i++)
            {
                var id = i+1;
                var productModel = new Product { Id = id, Name = "Product " + id, status = true };
                var sendMessage = JsonConvert.SerializeObject(productModel);

                producer.Produce("test-topic", new Message<Null, string> { Value = sendMessage });
                producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine($"[Producer] Sent: {sendMessage}");
            }
        }
    }
}
