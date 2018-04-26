using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Mandrill.Model;
using NServiceBus.Mandrill;
using NServiceBus.Unicast;

namespace NServiceBus
{
    public static class MandrillBusExtensions
    {
        public static Task SendEmail(this IPipelineContext context, MandrillMessage message)
        {
            var msg = new SendMandrillEmail(message);
            return Send(context, msg);
        }

        public static Task SendEmailTemplate(this IPipelineContext context, MandrillMessage message, string templateName,
            IList<MandrillTemplateContent> templateContents = null)
        {
            if (templateName == null)
            {
                throw new ArgumentNullException(nameof(templateName));
            }

            var msg = new SendMandrillEmail(message, templateName, templateContents);

            return Send(context, msg);
        }

        private static Task Send(IPipelineContext context, SendMandrillEmail msg)
        {
            return context.SendLocal(msg);
        }

        public static Task SendEmail(this IMessageSession session, MandrillMessage message)
        {
            var msg = new SendMandrillEmail(message);
            return Send(session, msg);
        }

        public static Task SendEmailTemplate(this IMessageSession session, MandrillMessage message, string templateName,
            IList<MandrillTemplateContent> templateContents = null)
        {
            if (templateName == null)
            {
                throw new ArgumentNullException(nameof(templateName));
            }

            var msg = new SendMandrillEmail(message, templateName, templateContents);

            return Send(session, msg);
        }

        private static Task Send(IMessageSession session, SendMandrillEmail msg)
        {
            return session.SendLocal(msg);
        }
    }
}