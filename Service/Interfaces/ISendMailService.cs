using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Interfaces
{
    public interface ISendMailService
    {
        Task SendVerificationEmail(string userEmail, string verificationToken);
        Task SendOrderConfirmationEmail(Order order);
        Task SendMailNotifyForCustomerOrder(Order order);
    }
}
