using System.Threading.Tasks;
using Mandrill.Model;
using NServiceBus;

namespace Tests
{
    internal class SendEmailTestHandler : IHandleMessages<MandrillSendTest.SendEmail>, IHandleMessages<MandrillSendTest.SendTemplateEmail>
    {

        public Task Handle(MandrillSendTest.SendEmail message, IMessageHandlerContext context)
        {
            var email = new MandrillMessage
            {
                Subject = "This is a test",
                Text = "Hello World"
            };

            return context.SendEmail(email);
        }

        public Task Handle(MandrillSendTest.SendTemplateEmail message, IMessageHandlerContext context)
        {
            var email = new MandrillMessage
            {
                Subject = "This is a test",
                Text = "Hello World"
            };

            return context.SendEmailTemplate(email, "email-template");
        }
    }
}