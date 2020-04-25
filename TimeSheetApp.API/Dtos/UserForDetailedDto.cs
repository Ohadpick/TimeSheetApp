using System;
using System.Collections.Generic;
using TimeSheetApp.API.Models;

namespace TimeSheetApp.API.Dtos
{
    public class UserForDetailedDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string photoUrl { get; set; }
    }
}