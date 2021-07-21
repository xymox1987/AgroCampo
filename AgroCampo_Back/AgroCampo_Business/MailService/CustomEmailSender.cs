
using AgroCampo_Common.Models;
using AgroCampo_Common.DTOs;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AgroCampo_Business.MailService

{


    public class CustomEmailSender : IMailSender
    {
        private readonly EmailSettings _emailSettings;
        private readonly IHostingEnvironment _env;

        public CustomEmailSender(
            IOptions<EmailSettings> emailSettings,
            IHostingEnvironment env)
        {
            _emailSettings = emailSettings.Value;
            _env = env;
        }

        public async Task SendEmailAsync(List<string> to, List<string> cc, string subject, string message, List<string> attachments, Boolean isHtml = false)
        {


            if (cc == null)
            {
                cc = new List<string>();
            }
            if (attachments == null)
            {
                attachments = new List<string>();
            }

            var mimeMessage = new MimeMessage();
            var builder = new BodyBuilder();
            mimeMessage.From.Add(new MailboxAddress(_emailSettings.SenderName, _emailSettings.Sender));

            foreach (var itemTo in to)
            {
                mimeMessage.To.Add(new MailboxAddress(itemTo.Trim().Replace(" ","")));
            }


            foreach (var itemCC in cc)
            {
                mimeMessage.Cc.Add(new MailboxAddress(itemCC.Trim().Replace(" ", "")));
            }


            mimeMessage.Subject = subject;


            foreach (var itemAttachment in attachments)
            {
                builder.Attachments.Add(itemAttachment);
            }

            if (isHtml)
            {
                builder.HtmlBody = message;
            }
            else
            {
                builder.TextBody = message;
            }
            mimeMessage.Body = builder.ToMessageBody();


            using (var client = new SmtpClient())
            {
                // For demo-purposes, accept all SSL certificates (in case the server supports STARTTLS)
                client.ServerCertificateValidationCallback = (s, c, h, e) => true;

                await client.ConnectAsync(_emailSettings.MailServer, _emailSettings.MailPort);

                // Note: only needed if the SMTP server requires authentication
                await client.AuthenticateAsync(_emailSettings.Sender, _emailSettings.Password);

                await client.SendAsync(mimeMessage);

                await client.DisconnectAsync(true);
            }

        }

    }


}
