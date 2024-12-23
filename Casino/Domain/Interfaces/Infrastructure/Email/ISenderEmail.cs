using System;
using System.Net;
using System.Net.Mail;
namespace Domain.Interfaces.Infrastructure.Email
{
    public interface ISenderEmail
    {
       bool SendEmail(string toEmail, string subject, string body);
       
}}