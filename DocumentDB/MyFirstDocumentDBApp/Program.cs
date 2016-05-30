using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;

namespace MyFirstDocumentDBApp
{
    class Program
    {
        private const string EndpointUri = "<your endpoint URI>";
        private const string PrimaryKey = "<your key>";
        private DocumentClient client;

        static void Main(string[] args)
        {
           
            try
            {
                Program p = new Program();
                p.GetStartedDemo().Wait();
            }
            catch (DocumentClientException de)
            {
                Exception baseException = de.GetBaseException();
                Console.WriteLine("{0} error occurred: {1}, Message: {2}", de.StatusCode, de.Message, baseException.Message);
            }
            catch (Exception e)
            {
                Exception baseException = e.GetBaseException();
                Console.WriteLine("Error: {0}, Message: {1}", e.Message, baseException.Message);
            }
            finally
            {
                Console.WriteLine("End of demo, press any key to exit.");
                Console.ReadKey();
            }
        }

        private async Task GetStartedDemo()
        {
        
            this.client = new DocumentClient(new Uri(EndpointUri), PrimaryKey);

            var databaseName = "FirstDB";
            var collectionName = "FirstCollection";
            try
            {
                await this.client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(databaseName));
            }
            catch (DocumentClientException ex) when(ex.StatusCode==HttpStatusCode.NotFound)
            {
                await client.CreateDatabaseAsync(new Database() {Id = databaseName});
            }

            var documentCollection = new DocumentCollection();
            documentCollection.Id = collectionName;
            documentCollection.IndexingPolicy=new IndexingPolicy(new RangeIndex(DataType.String) {Precision = -1});
            var response=await client.CreateDocumentCollectionAsync(UriFactory.CreateDatabaseUri(databaseName),
                documentCollection,
                new RequestOptions()
                {
                    OfferThroughput = 400
                });


            var bart=new Person()
            {
                Id = "123",
                FirstName = "Bart",
                LastName = "Simpson",
                Address = new Address()
                {
                    City = "Springfield",
                    County = "USA",
                    State = "Wahtever"
                }
            };
            await this.client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), bart);


            FeedOptions queryOptions = new FeedOptions { MaxItemCount = -1 };

            // Here we find the Andersen family via its LastName
            IQueryable<Person> personQuery = this.client.CreateDocumentQuery<Person>(
                    UriFactory.CreateDocumentCollectionUri(databaseName, collectionName), queryOptions)
                    .Where(f => f.LastName == "Simpson");

            foreach (var person in personQuery)
            {
                Console.WriteLine($"\tRead {person}");
            }

        }
    }

    public class Person
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        public string LastName { get; set; }
        public string FirstName { get; set; }
        public Address Address { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }
    }

    public class Address
    {
        public string State { get; set; }
        public string County { get; set; }
        public string City { get; set; }
    }
}
