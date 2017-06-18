using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace SimpleExe
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionString =
                ConfigurationManager.ConnectionStrings["STORAGE_CONNECTION"].ConnectionString;
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var tableClient = storageAccount.CreateCloudTableClient();
            var table = tableClient.GetTableReference("Log");
            table.CreateIfNotExists();
            table.Execute(TableOperation.Insert(new LogMessage($"WebJob RULEZ!!!! - Utc:{DateTime.UtcNow} Local:{DateTime.Now}")));

        }
        
        class LogMessage:TableEntity
        {

            public LogMessage(string msg)
            {
                PartitionKey = DateTime.UtcNow.Year.ToString() + DateTime.UtcNow.Month.ToString();
                RowKey = Guid.NewGuid().ToString();
                Message = msg;
            }
            public string Message { get; set; }
        }
    }
}
