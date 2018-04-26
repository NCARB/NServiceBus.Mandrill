using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mandrill.Model;
using NServiceBus;

namespace Sample
{
    public class EmailSender
    {
        public async Task Start(IEndpointInstance endpoint)
        {
            Console.WriteLine("Hit any key to send a email using the mandrill handler. \nHit <q> for exit");

            while (Console.ReadKey().Key.ToString().ToLower() != "q")
            {
                var mail = new MandrillMessage
                {
                    FromEmail = "mandrill.net@example.com",
                    To = new List<MandrillMailAddress>(){new MandrillMailAddress("nservicebus@example.com", "Udi Dahan") },
                    Subject = "NServiceBus.Mandrill test",
                    Text = "Hello NSericeBus! \nRegards"
                };

                Console.WriteLine("Sending email to {0}", mail.To[0].Email);

                await endpoint.SendEmail(mail);
            }
        }

        public void Stop()
        {
            //no-op
        }
    }
}