using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TimeSheetApp.API.Helpers;
using TimeSheetApp.API.Models;

namespace TimeSheetApp.API.Data
{
    public interface ITimeSheetRepository
    {
         void Add<T>(T entity) where T: class;
         void Delete<T>(T entity) where T: class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int id);
         Task<Day> GetDay (DateTime reportedDate, int userId);
         Task<List<Day>> GetDaysForUser (DayParams dayParams);
         Task<Project> GetProject (int projectId);
         Task<IEnumerable<Project>> GetProjects (ProjectParams projectParams);
         Task<PagedList<UserProject>> GetProjectsForUser (int userId, ProjectParams projectParams);
    }
}