using System.Collections.Generic;

namespace TimeSheetApp.API.Models
{
    public class Project
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserProject> UserProjects { get; set; }
    }
}