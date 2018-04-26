using System;
using System.Threading.Tasks;
using Mandrill;
using NServiceBus;
using NServiceBus.Logging;
using NServiceBus.Mandrill;

namespace Sample
{
    internal class EmailResultHandler : IHandleMessages<MandrillEmailResult>, IHandleMessages<GetMailContent>
    {
        public IMandrillMessagesApi MandrillApi { get; set; }

        public EmailResultHandler(IMandrillMessagesApi mandrillApi)
        {
            MandrillApi = mandrillApi;
        }

        public async Task Handle(MandrillEmailResult message, IMessageHandlerContext context)
        {
            //do something with the message result
            Logger.InfoFormat("{0} {1} {2}", message.Response.Id, message.Response.Status, message.Response.Email);

            //Wait 5 minutes, then try to get the email content back
            var options = new SendOptions();
            options.DelayDeliveryWith(TimeSpan.FromMinutes(5));
            options.RouteToThisEndpoint();

            await context.Send<GetMailContent>(content => content.MessageId = message.Response.Id, options);
        }

        public async Task Handle(GetMailContent message, IMessageHandlerContext context)
        {
            var content = await MandrillApi.ContentAsync(message.MessageId);
            Logger.InfoFormat("Message id {0} sent at {1} had text content {2}", message.MessageId, content.Ts,
                content.Text);
        }

        private static readonly ILog Logger = LogManager.GetLogger(typeof (EmailResultHandler));
    }
}