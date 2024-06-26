﻿using Microsoft.AspNetCore.Identity;

namespace DatingAppNeilCummings.Entities
{
	public class AppUser: IdentityUser
	{
		public DateTime DateOfBirth { get; set; }
		public string KnownAs { get; set; }
		public DateTime Created { get; set; } = DateTime.Now;
		public DateTime LastActive { get; set; } = DateTime.Now;
		public string Gender { get; set; }
		public string Introduction { get; set; }
		public string LookingFor { get; set; }
		public string Interests { get; set; }
		public string City { get; set; }
		public string Country { get; set; }

        public List<Photo> Photos { get; set; }
    }
}
