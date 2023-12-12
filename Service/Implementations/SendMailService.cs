using SendGrid.Helpers.Mail;
using SendGrid;
using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Utility.Settings;
using MimeKit;
using MailKit.Security;
using Data.Entities;
using Newtonsoft.Json.Linq;
using System.Globalization;

namespace Service.Implementations
{
    public class SendMailService : ISendMailService
    {
		private readonly string _sendGridApiKey;
		private readonly string _emailAddress;
		private readonly string _nameApp;
		private readonly string _username;
		private readonly string _password;
		private readonly int _port;
		private readonly string _host;
		private readonly bool _useSSL;
		private readonly bool _useStartTls;
		private readonly static CancellationToken ct = new CancellationToken();
		public SendMailService(IOptions<AppSetting> appSettings)
        {
			_sendGridApiKey = appSettings.Value.SendGridApiKey;
			_emailAddress = appSettings.Value.EMailAddress;
			_nameApp = appSettings.Value.NameApp;
			_username = appSettings.Value.Username;
			_password = appSettings.Value.Password;
			_port = appSettings.Value.Port;
			_host = appSettings.Value.Host;
			_useSSL = appSettings.Value.UseSSL;
			_useStartTls = appSettings.Value.UseStartTls;

		}
        
        public async Task SendVerificationEmail(string userEmail, string token)
        {
			var verificationLink = $"https://egift-d50fc.web.app/verification/{token}";

			//var client = new SendGridClient(_sendGridApiKey);
			//var from = new EmailAddress(_emailAddress, _nameApp);
			//var to = new EmailAddress(userEmail);
			//var subject = "Email Verification";
			//var plainTextContent = $"Please verify your email by clicking the following link: {verificationLink}";
			//var htmlContent = $"<p>Please verify your email by clicking the following link: <a href=\"{verificationLink}\">{verificationLink}</a></p>";

			//var msg = MailHelper.CreateSingleEmail(from, to, subject, plainTextContent, htmlContent);
			//await client.SendEmailAsync(msg);
			try
			{
				var mail = new MimeMessage();

				// Sender
				mail.From.Add(new MailboxAddress(_nameApp, _emailAddress));
				mail.Sender = new MailboxAddress(_nameApp, _emailAddress);

				// Receiver
				mail.To.Add(MailboxAddress.Parse(userEmail));

				// Add Content to Mime Message
				var body = new BodyBuilder();
				mail.Subject = "Email Verification";
				body.HtmlBody = $"<p>Please verify your email by clicking the following link: <a href=\"{verificationLink}\">{verificationLink}</a></p>";
				mail.Body = body.ToMessageBody();

				using var smtp = new MailKit.Net.Smtp.SmtpClient();

				if (_useSSL)
				{
					await smtp.ConnectAsync(_host, _port, SecureSocketOptions.SslOnConnect, ct);
				}
				else if (_useStartTls)
				{
					await smtp.ConnectAsync(_host, _port, SecureSocketOptions.StartTls, ct);
				}
				await smtp.AuthenticateAsync(_username, _password, ct);
				await smtp.SendAsync(mail, ct);
				await smtp.DisconnectAsync(true, ct);
			}
			catch (Exception)
			{
				throw;
			}
		}

        public async Task SendOrderConfirmationEmail(Order order)
        {
            try
            {
                var mail = new MimeMessage();

                // Sender
                mail.From.Add(new MailboxAddress(_nameApp, _emailAddress));
                mail.Sender = new MailboxAddress(_nameApp, _emailAddress);

                // Receiver
                mail.To.Add(MailboxAddress.Parse(order.Email));

                // Add Content to Mime Message
                var body = new BodyBuilder();
                mail.Subject = "Confirm Order";


                //----------------------
                body.HtmlBody = "<h2 style='color: #333; font-family: Arial, sans-serif; margin-bottom: 10px;'>Thank you for your order!</h2>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px; font-weight: bold; text-transform: capitalize;'>Dear " + order.Customer.Name + ",</p>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px;'>Thank you for choosing our services!</p>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px;'>We will deliver your order as soon as possible.</p>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px;'>Please check your order details:</p>";

                body.HtmlBody += "<table style='border-collapse: collapse; width: 100%;'>";
                body.HtmlBody += "<tr>";
                body.HtmlBody += "<th style='border: 1px solid #ddd; padding: 10px;'>Image</th>";
                body.HtmlBody += "<th style='border: 1px solid #ddd; padding: 10px;'>Product</th>";
                body.HtmlBody += "<th style='border: 1px solid #ddd; padding: 10px;'>Price</th>";
                body.HtmlBody += "<th style='border: 1px solid #ddd; padding: 10px;'>Quantity</th>";
                body.HtmlBody += "</tr>";

                foreach (var detail in order.OrderDetails)
                {
                    body.HtmlBody += "<tr>";
                    body.HtmlBody += "<td style='border: 1px solid #ddd; padding: 10px; text-align: center;'>";

                    // Product image
                    var product = detail.Product;
                    var firstImage = product.ProductImages.FirstOrDefault();
                    if (firstImage != null)
                    {
                        var imageUrl = firstImage.Url;
                        body.HtmlBody += $"<div style='display: flex; justify-content: center; align-items: center; height: 100%;'><img src='{imageUrl}' alt='Product Image' style='width: 80px; height: auto; margin: 0 auto;' /></div>";
                    }

                    body.HtmlBody += "</td>";
                    body.HtmlBody += "<td style='border: 1px solid #ddd; padding: 10px;'>";
                    body.HtmlBody += $"<h3 style='color: #333; font-family: Arial, sans-serif; margin: 0;'>{product.Name}</h3>";
                    body.HtmlBody += "</td>";
                    body.HtmlBody += "<td style='border: 1px solid #ddd; padding: 10px;'>";
                    body.HtmlBody += $"<p style='color: #777; font-family: Arial, sans-serif; margin: 0;'>{product.Price.ToString("C", new CultureInfo("vi-VN"))}</p>";
                    body.HtmlBody += "</td>";
                    body.HtmlBody += "<td style='border: 1px solid #ddd; padding: 10px;'>";
                    body.HtmlBody += $"<p style='color: #777; font-family: Arial, sans-serif; margin: 0;'>{detail.Quantity}</p>";
                    body.HtmlBody += "</td>";
                    body.HtmlBody += "</tr>";
                }

                body.HtmlBody += "</table>";
                body.HtmlBody += "<h3 style='color: #333; font-family: Arial, sans-serif; margin-top: 20px;'>Phone number: " + order.Phone + "</h3>";
                body.HtmlBody += "<h3 style='color: #333; font-family: Arial, sans-serif;'>Address: " + order.Address + "</h3>";
                body.HtmlBody += "<h3 style='color: #333; font-family: Arial, sans-serif;'>Amount: " + order.Amount.ToString("C", new CultureInfo("vi-VN")) + "</h3>";

                if (order.IsPaid)
                {
                    body.HtmlBody += "<h3 style='color: #333; font-family: Arial, sans-serif;'>Payment methods: VNPay</h3>";
                }
                else
                {
                    body.HtmlBody += "<h3 style='color: #333; font-family: Arial, sans-serif;'>Payment methods: Cash</h3>";
                }
                

                //--------------------------------
                mail.Body = body.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                if (_useSSL)
                {
                    await smtp.ConnectAsync(_host, _port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (_useStartTls)
                {
                    await smtp.ConnectAsync(_host, _port, SecureSocketOptions.StartTls, ct);
                }
                await smtp.AuthenticateAsync(_username, _password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task SendMailNotifyForCustomerOrder(Order order)
        {
            try
            {
                var mail = new MimeMessage();

                // Sender
                mail.From.Add(new MailboxAddress(_nameApp, _emailAddress));
                mail.Sender = new MailboxAddress(_nameApp, _emailAddress);

                // Receiver
                mail.To.Add(MailboxAddress.Parse(order.Email));

                // Add Content to Mime Message
                var body = new BodyBuilder();
                mail.Subject = "Order Delivery Notification";


                //----------------------
                body.HtmlBody = "<h2 style='color: #333; font-family: Arial, sans-serif; margin-bottom: 10px;'>Your Order Has Been Shipped!</h2>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px; font-weight: bold; text-transform: capitalize;'>Dear " + order.Customer.Name + ",</p>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px;'>Your order has been handed over to our shipping partner. Please allow 2-3 days for delivery.</p>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px;'>After receiving the package, kindly confirm the delivery on our system to help us improve our services.</p>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px;'>Thank you for choosing our services. If you have any questions, feel free to contact our support team.</p>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px;'>Best regards,</p>";
                body.HtmlBody += "<p style='color: #777; font-family: Arial, sans-serif; margin-bottom: 20px;'>E-Commerce Gift Company.</p>";

                body.HtmlBody += "<hr style='border: 1px solid #333; margin: 20px 0;'>";

                body.HtmlBody += "<p style='color: #333; font-family: Arial, sans-serif; margin-bottom: 20px; font-weight: bold;'>Amount: " + order.Amount.ToString("C", new CultureInfo("vi-VN")) + "</p>";

                body.HtmlBody += "<table style='border-collapse: collapse; width: 100%;'>";
                body.HtmlBody += "<tr>";
                body.HtmlBody += "<th style='border: 1px solid #ddd; padding: 10px;'>Image</th>";
                body.HtmlBody += "<th style='border: 1px solid #ddd; padding: 10px;'>Product</th>";
                body.HtmlBody += "<th style='border: 1px solid #ddd; padding: 10px;'>Price</th>";
                body.HtmlBody += "<th style='border: 1px solid #ddd; padding: 10px;'>Quantity</th>";
                body.HtmlBody += "</tr>";

                foreach (var detail in order.OrderDetails)
                {
                    body.HtmlBody += "<tr>";
                    body.HtmlBody += "<td style='border: 1px solid #ddd; padding: 10px; text-align: center;'>";

                    // Product image
                    var product = detail.Product;
                    var firstImage = product.ProductImages.FirstOrDefault();
                    if (firstImage != null)
                    {
                        var imageUrl = firstImage.Url;
                        body.HtmlBody += $"<div style='display: flex; justify-content: center; align-items: center; height: 100%;'><img src='{imageUrl}' alt='Product Image' style='width: 80px; height: auto; margin: 0 auto;' /></div>";
                    }

                    body.HtmlBody += "</td>";
                    body.HtmlBody += "<td style='border: 1px solid #ddd; padding: 10px;'>";
                    body.HtmlBody += $"<h3 style='color: #333; font-family: Arial, sans-serif; margin: 0;'>{product.Name}</h3>";
                    body.HtmlBody += "</td>";
                    body.HtmlBody += "<td style='border: 1px solid #ddd; padding: 10px;'>";
                    body.HtmlBody += $"<p style='color: #777; font-family: Arial, sans-serif; margin: 0;'>{product.Price.ToString("C", new CultureInfo("vi-VN"))}</p>";
                    body.HtmlBody += "</td>";
                    body.HtmlBody += "<td style='border: 1px solid #ddd; padding: 10px;'>";
                    body.HtmlBody += $"<p style='color: #777; font-family: Arial, sans-serif; margin: 0;'>{detail.Quantity}</p>";
                    body.HtmlBody += "</td>";
                    body.HtmlBody += "</tr>";
                }

                body.HtmlBody += "</table>";


                //--------------------------------
                mail.Body = body.ToMessageBody();

                using var smtp = new MailKit.Net.Smtp.SmtpClient();

                if (_useSSL)
                {
                    await smtp.ConnectAsync(_host, _port, SecureSocketOptions.SslOnConnect, ct);
                }
                else if (_useStartTls)
                {
                    await smtp.ConnectAsync(_host, _port, SecureSocketOptions.StartTls, ct);
                }
                await smtp.AuthenticateAsync(_username, _password, ct);
                await smtp.SendAsync(mail, ct);
                await smtp.DisconnectAsync(true, ct);
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
