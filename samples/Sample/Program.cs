using System;
using System.Configuration;
using NServiceBus;

namespace Sample
{
    static class Program
    {
        static void Main()
        {
            var configuration = new EndpointConfiguration("mandrill.sample");
            var apiKey = Environment.GetEnvironmentVariable("MANDRILL_API_KEY") ?? ConfigurationManager.AppSettings["MANDRILL_API_KEY"];

            configuration.UsePersistence<InMemoryPersistence>();
            configuration.TimeoutManager();
            configuration.TimeToWaitBeforeTriggeringCriticalErrorOnTimeoutOutages(TimeSpan.FromMinutes(1));
            configuration.SendFailedMessagesTo("error");
            configuration.UseMandrill(apiKey: apiKey, replyResult: true);

            var endpoint = Endpoint.Start(configuration).GetAwaiter().GetResult();

            var sender = new EmailSender();
            try
            {
                sender.Start(endpoint).GetAwaiter().GetResult();
            }
            finally
            {
                endpoint.Stop();
            }

        }
    }
}