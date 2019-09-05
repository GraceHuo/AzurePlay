using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Configuration;
using System.Web.Mvc;

namespace AzurePlay.Controllers
{
    public class StorageController : Controller
    {
        // GET: Storage
        public ActionResult Index()
        {
            // Retrieve the connection string
            var storageConnectionString = ConfigurationManager.ConnectionStrings["StorageConnectionString"].ConnectionString;

            // Retrieve the storage account from the connection string
            var storageAccount = CloudStorageAccount.Parse(storageConnectionString);

            // Create the table client
            var tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "customer" table
            var table = tableClient.GetTableReference("customer");

            // Create the table if it doesn't exist
            table.CreateIfNotExists();

            // Create a new customer entity
            var customer = new CustomerEntity(Guid.NewGuid())
            {
                FirstName = "Grace",
                LastName = "Huo",
                Email = "Grace@azureplay.com",
                PhoneNumber = "123-456-7890"
            };

            // Create the TableOperation object that inserts the customer entity
            var insertOpertation = TableOperation.Insert(customer);

            // Execute the insert operation
            table.Execute(insertOpertation);

            return View(customer);
        }
    }

    public class CustomerEntity : TableEntity
    {
        public CustomerEntity(Guid employeeId)
        {
            PartitionKey = "customer";
            RowKey = employeeId.ToString();
        }

        public CustomerEntity()
        {

        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }
    }
}