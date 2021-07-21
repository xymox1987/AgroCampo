using AgroCampo_Common.DTOs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AgroCampo_Business.MailService
{
   public interface IMailSender
    {
        Task SendEmailAsync(List<string> to, List<string> cc, string subject, string message, List<string> attachments, Boolean isHtml = false);
    }
}
