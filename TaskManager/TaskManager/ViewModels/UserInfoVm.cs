using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TaskManager.ViewModels
{
	public class UserInfoVm
	{
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public long PhoneNumber { get; set; }
		public string EmailId { get; set; }
		public string RoleName { get; set; }
	}
}