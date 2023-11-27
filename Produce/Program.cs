using System;
using System.Text.RegularExpressions;
using Confluent.Kafka;
using Consume.Models;
using Newtonsoft.Json;

class Program
{
    static void Main()
    {
        // Consumer
        var configC = new ConsumerConfig
        {
            GroupId = "test_groupId",
            BootstrapServers = "localhost:9092",
            //AutoOffsetReset = AutoOffsetReset.Earliest
        };
        using (var consumer = new ConsumerBuilder<Null, string>(configC).Build())
        {
            consumer.Subscribe("test-topic");

            Console.WriteLine("Consumer App: ");

            while (true)
            {
                try
                {
                    var consumeResult = consumer.Consume();
                    var receivedMessage = consumeResult.Message.Value;

                    var productModel = JsonConvert.DeserializeObject<Product>(receivedMessage);
                    Console.WriteLine($"[Consumer] Received: Id={productModel.Id}, Name={productModel.Name},Status={productModel.status}");
                }
                catch (ConsumeException e)
                {
                    Console.WriteLine($"Error while consuming: {e.Error.Reason}");
                }
            }

        }
    }
}

