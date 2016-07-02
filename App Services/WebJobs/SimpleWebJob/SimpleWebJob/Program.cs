using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;

namespace SimpleWebJob
{
    public class Program
    {
        public static void Main()
        {
            var storageConn = ConfigurationManager.ConnectionStrings["AzureWebJobsStorage"].ConnectionString;
            var dashboardConn = ConfigurationManager.ConnectionStrings["AzureWebJobsDashboard"].ConnectionString;
            //var _serviceBusConn = ConfigurationManager.ConnectionStrings["MyServiceBusConnection"].ConnectionString;

            JobHostConfiguration config = new JobHostConfiguration()
            {
                Tracing = { ConsoleLevel = TraceLevel.Verbose },
                StorageConnectionString = storageConn,
                DashboardConnectionString = dashboardConn,
                
            };

            // Other Options
            //config.ServiceBusConnectionString = _serviceBusConn;
            //config.UseFiles();
            //config.UseTimers();

            config.UseWebHooks();
            JobHost host = new JobHost(config);
            host.RunAndBlock();
        }

        public static void ProcessQueueMessage(
            [QueueTrigger("webjobsqueue")] string inputText,
            [Blob("webjobscontainer/webjobsblob")]TextWriter writer)
        {
            writer.WriteLine(inputText);
        }

        /// <summary>
        /// webhooks url are in the following format:
        ///     https://[Site]/api/continuouswebjobs/[Job]/passthrough/[Path]
        /// Site – App Service’s SCM site (e.g. yoursite.scm.azurewebsites.net)
        /// Job – The name of your WebJob
        /// Path – Path to the WebHook function.This should be of the format ClassName/MethodName
        /// </summary>
        /// <param name="body"></param>
        /// <param name="logger"></param>
        public static void ProcessWebHookA([WebHookTrigger] string body, TextWriter logger)
        {
            logger.WriteLine($"WebHookA invoked! Body: {body}");
        }   
    }
}
