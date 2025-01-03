﻿using Microsoft.AspNetCore.Identity;

namespace Tasker.Identity.Models
{
	public class ApplicationRole : IdentityRole
	{
		public ApplicationRole() : base() { }
		public ApplicationRole(string roleName) : base(roleName) { }
	}
}
