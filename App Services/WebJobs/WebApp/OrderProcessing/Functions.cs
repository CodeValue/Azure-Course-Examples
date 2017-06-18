using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace OrderProcessing
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("orders")] string message,
            [Table("orders")] ICollector<Order> tableBinding,
            TextWriter log)
        {
            log.WriteLine(message);
            dynamic order=JObject.Parse(message);
            tableBinding.Add(new Order(order.Product.ToString()){Customer = order.Customer.ToString()});
        }
    }

    public class Order:TableEntity
    {
        public Order(string product)
        {
            PartitionKey = product;
            RowKey = Guid.NewGuid().ToString();
        }

        public string Product => PartitionKey;
        public string Customer { get; set; }
    }
}
