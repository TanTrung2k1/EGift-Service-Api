using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Settings
{
    public class AppSetting
    {
        public string Secret { get; set; } = null!;

		//send mail
		public string SendGridApiKey { get; set; } = null!;
		public string EMailAddress { get; set; } = null!;
		public string NameApp { get; set; } = null!;
		public bool UseSSL { get; set; }
		public int Port { get; set; }
		public string Host { get; set; } = null!;
		public string Username { get; set; } = null!;
		public string Password { get; set; } = null!;
		public bool UseStartTls { get; set; }

        public VnpaySetting VnPay { get; set; } = null!;
    }
}
