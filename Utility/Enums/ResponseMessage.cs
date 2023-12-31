﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Enums
{
    public static class ResponseMessage
    {
        public const string Error = "Somethings wrong.";
        public const string Success = "Successful !!!";


        public const string CustomerNotFound = "Not found this customer.";
        public const string CustomerConflict = "UserName or Email already exist.";
        public const string CustomerRegisterSuccess = "Register successfull. Please check your mail to verify!!!";

        public const string NoFile = "No files uploaded.";
        public const string NumberProductFile = "Please choose 4 files.";
        public const string ErrorAddImage = "can't upload more upto only four image.";

        public const string OrderNotFound = "Order is not found";

        //======================= 
        public const string ProductNotFound = "Not found this product.";

        public const string ValidateHashError = "Validate failed, please recheck hash value";

        public const string TxnRefExist = "Exist order in transaction";

        public const string InvalidStateToGeneratePaymentUrl = "Order is not correct state to generate payment url";

        public const string AmountNotValid = "Invalid amount";

        public const string PaymentSuccess = "Payment successfully";


        //=======================
    }
}
