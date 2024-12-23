using System;
using System.Net;
using System.Net.Mail;
using Domain.Interfaces.Infrastructure.Email;

public class EmailSenderYandex : ISenderEmail
{
    

    public bool SendEmail(string toEmail, string subject, string body)
    {
   
        var smtpClient = new SmtpClient("smtp.yandex.ru") 
        {
            Port = 587, 
            Credentials = new NetworkCredential("InspireoService@yandex.ru", "gufhmplnvuingqli"), 
            EnableSsl = true,
            Timeout = 10000 
        };

        Console.WriteLine("Создание письма...");
        var mailMessage = new MailMessage
        {
            From = new MailAddress("InspireoService@yandex.ru"), 
            Subject = subject,
            Body = body,
            IsBodyHtml = false, 
        };
        mailMessage.To.Add(toEmail);
        Console.WriteLine("Письмо создано, отправляем...");

 
        try
        {
            smtpClient.Send(mailMessage);
            Console.WriteLine("Письмо успешно отправлено на адрес: " + toEmail);
            return true;    
        }
        catch (SmtpException smtpEx)
        {
            return false;
        }
        catch (Exception ex)

        {
             return false;
            
        }
        finally
        {
    
            mailMessage.Dispose();
            smtpClient.Dispose();
        }
    }
}

    
   


