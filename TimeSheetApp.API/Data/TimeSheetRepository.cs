using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TimeSheetApp.API.Helpers;
using TimeSheetApp.API.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Data.SqlClient;
using System.Data;

namespace TimeSheetApp.API.Data
{
    public class TimeSheetRepository : ITimeSheetRepository
    {
        private readonly DataContext _context;
        public TimeSheetRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<User> GetUser(int id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == id);

            return user;
        }

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _context.Users.OrderByDescending(u => u.LastActive).AsQueryable();

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }

            return await PagedList<User>.CreateAsync(users, userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<Day> GetDay(DateTime reportedDate, int userId)
        {
            var day = await _context.Days.FirstOrDefaultAsync(d => d.ReportedDate == reportedDate && d.UserId == userId);

            return day;
        }

        public async Task<List<Day>> GetDaysForUser(DayParams dayParams)
        {
            List<Day> days = new List<Day>();
            
            string connectionString =  "Data Source=localhost;Initial Catalog=TimeSheet;User ID=sa;Password=Go2hell1234";

            using (SqlConnection sqlConnection = new SqlConnection(connectionString))
            {
                using(SqlCommand sqlCommand = new SqlCommand("stp_GetDaysForUser", sqlConnection)) {
                    sqlCommand.CommandType = System.Data.CommandType.StoredProcedure;
                    sqlCommand.Parameters.Add ("@userId", SqlDbType.Int).Value = "1";
                    if (dayParams.startDate.HasValue)
                        sqlCommand.Parameters.Add ("@startDate", SqlDbType.DateTime2).Value = dayParams.startDate.ToString();
                    else    
                        sqlCommand.Parameters.Add ("@startDate", SqlDbType.DateTime2).Value = DBNull.Value;
                    if (dayParams.endDate.HasValue)
                        sqlCommand.Parameters.Add ("@endDate", SqlDbType.DateTime2).Value = dayParams.endDate.ToString();
                    else    
                        sqlCommand.Parameters.Add ("@endDate", SqlDbType.DateTime2).Value = DBNull.Value;
                    
                    sqlConnection.Open();
                    SqlDataReader sqlDataReader = await sqlCommand.ExecuteReaderAsync();
                    
                    while (sqlDataReader.Read())
                    {
                        Day day = new Day();
                        day.ReportedDate = DateTime.Parse(sqlDataReader["ReportedDate"].ToString());
                        day.DaySequenceId = Int32.TryParse(sqlDataReader["DaySequenceId"].ToString(), out var tempDaySequenceId) ? tempDaySequenceId : (int?)null;
                        day.ProjectId = Int32.TryParse(sqlDataReader["ProjectId"].ToString(), out var tempProjectId) ? tempProjectId : (int?)null;
                        day.StartTime = TimeSpan.TryParse(sqlDataReader["StartTime"].ToString(), out var tempStartTime) ? tempStartTime : (TimeSpan?) null;
                        day.EndTime = TimeSpan.TryParse(sqlDataReader["EndTime"].ToString(), out var tempEndTime) ? tempEndTime : (TimeSpan?) null;
                        day.Remark = sqlDataReader["Remark"].ToString();

                        days.Add (day);
                    }

                }
            }

            return days;
        }

        public async Task<Project> GetProject(int projectId)
        {
            var project = await _context.Projects.FirstOrDefaultAsync(p => p.Id == projectId);

            return project;
        }

        public async Task<IEnumerable<Project>> GetProjects(ProjectParams projectParams)
        {
            var projects = _context.Projects.OrderByDescending(p => p.Description).AsQueryable();

            return await PagedList<Project>.CreateAsync(projects, projectParams.PageNumber, projectParams.PageSize);
        }

        public async Task<PagedList<UserProject>> GetProjectsForUser(int userId, ProjectParams projectParams)
        {
            var userProjects = _context.UserProjects.AsQueryable();

            userProjects = userProjects.Where (up => up.UserId == userId);

            userProjects = userProjects.OrderByDescending(up => up.Project.Description);

            return await PagedList<UserProject>.CreateAsync(userProjects, projectParams.PageNumber, projectParams.PageSize);
        }
    }
}