using System;

namespace TimeSheetApp.API.Dtos
{
    public class UserForListDto
    {
        public int Id { get; set; }
        public string username { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastActive { get; set; }
        public string photoUrl { get; set; }
    }
}