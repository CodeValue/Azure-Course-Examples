using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.WindowsAzure.Storage.Queue;
using Newtonsoft.Json;
using WebApp.Model;

namespace WebApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly IConfiguration _configuration;

        public OrdersController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public async  Task<IActionResult> Create(NewOrder newOrder)
        {
            var connectionString = _configuration.GetConnectionString("STORAGE_CONNECTION");
            var storageAccount =Microsoft.WindowsAzure.Storage.CloudStorageAccount.Parse(connectionString);
            var queueClient = storageAccount.CreateCloudQueueClient();
            var queue = queueClient.GetQueueReference("orders");
            await queue.CreateIfNotExistsAsync();
            CloudQueueMessage message = new CloudQueueMessage(JsonConvert.SerializeObject(newOrder));
            await queue.AddMessageAsync(message);
            return Ok("Done");
        }
    }
}
