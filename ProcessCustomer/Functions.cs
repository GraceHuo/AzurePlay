using Microsoft.Azure.WebJobs;
using System.IO;

namespace ProcessCustomer
{
    public class Functions
    {
        // This function will get triggered/executed when a new message is written 
        // on an Azure Queue called queue.
        public static void ProcessQueueMessage([QueueTrigger("customerqueue")] string message, TextWriter log)
        {
            // processing + email
            log.WriteLine(message);
        }
    }
}
