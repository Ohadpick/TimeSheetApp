using System;
using System.Collections.Generic;

namespace TimeSheetApp.API.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public String PhotoUrl { get; set; }
        public virtual ICollection<Day> Days { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }
    }
}