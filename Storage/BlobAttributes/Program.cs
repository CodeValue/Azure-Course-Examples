using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobAttributes
{
    class Program
    {
        const string ImageToUpload = "HelloWorld.png";

        static void Main(string[] args)
        {

            var connectionString = Microsoft.Azure.CloudConfigurationManager.GetSetting("StorageConnectionString");
            
            //Parse the connection string for the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(connectionString);

            //Create the service client object for credentialed access to the Blob service.
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container. 
            CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

            // Create the container if it does not already exist.
            container.CreateIfNotExists();

            // Fetch container properties and write out their values.
            container.FetchAttributes();
            Console.WriteLine("Properties for container {0}", container.StorageUri.PrimaryUri.ToString());
            Console.WriteLine("LastModifiedUTC: {0}", container.Properties.LastModified.ToString());
            Console.WriteLine("ETag: {0}", container.Properties.ETag);

            ReadAndSetBlobAttributesAndMetadata(container);


            Console.WriteLine();
        }

        private static void ReadAndSetBlobAttributesAndMetadata(CloudBlobContainer container)
        {
            CloudBlockBlob blobReference = container.GetBlockBlobReference(ImageToUpload);
            blobReference.UploadFromFile(ImageToUpload);

            blobReference.FetchAttributes();
            blobReference.Metadata.Add("originalFileName",ImageToUpload);
            blobReference.SetMetadata();
            foreach (var prop in blobReference.Metadata)
            {
                Console.WriteLine($"blob metadata {prop.Key} : {prop.Value}");
            }
        }

        public static void AddContainerMetadata(CloudBlobContainer container)
        {
            //Add some metadata to the container.
            container.Metadata.Add("docType", "textDocuments");
            container.Metadata["category"] = "guidance";

            //Set the container's metadata.
            container.SetMetadata();
        }

        public static void ListContainerMetadata(CloudBlobContainer container)
        {
            //Fetch container attributes in order to populate the container's properties and metadata.
            container.FetchAttributes();

            //Enumerate the container's metadata.
            Console.WriteLine("Container metadata:");
            foreach (var metadataItem in container.Metadata)
            {
                Console.WriteLine("\tKey: {0}", metadataItem.Key);
                Console.WriteLine("\tValue: {0}", metadataItem.Value);
            }
        }
    }
}
