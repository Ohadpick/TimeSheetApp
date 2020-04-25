using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AutoMapper;
using TimeSheetApp.API.Data;
using TimeSheetApp.API.Dtos;
using TimeSheetApp.API.Helpers;
using TimeSheetApp.API.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace TimeSheetApp.API.Controllers
{
    [ServiceFilter(typeof(LogUserActivity))]
    [Authorize]
    [Route("api/users/{userId}/[controller]")]
    [ApiController]
    public class DaysController : ControllerBase
    {
        private readonly ITimeSheetRepository _repo;
        private readonly IMapper _mapper;
        public DaysController(ITimeSheetRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;

        }

        [HttpGet("{reportedDate}", Name = "GetDay")]
        public async Task<IActionResult> GetDay(int userId, [FromQuery]DateTime reportedDate)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var DayFromRepo = await _repo.GetDay(reportedDate, userId);

            if (DayFromRepo == null)
                return NotFound();

            return Ok (DayFromRepo);
        }

        [HttpGet]
        public async Task<IActionResult> GetDaysForUser (int userId, [FromQuery]DayParams DayParams)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            DayParams.UserId = userId;

            var DaysFromRepo = await _repo.GetDaysForUser(DayParams);

            var Days = _mapper.Map<IEnumerable<DayToReturnDto>>(DaysFromRepo);

            //Response.AddPagination(DaysFromRepo.CurrentPage,
            //                       DaysFromRepo.PageSize, DaysFromRepo.TotalCount, DaysFromRepo.TotalPages);
            
            return Ok(Days);
        }

        [HttpPost]
        public async Task<IActionResult> CreateDay (int userId, [FromBody]DayForCreationDto DayForCreationDto)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var Day = _mapper.Map<Day>(DayForCreationDto);

            _repo.Add(Day);

            if (await _repo.SaveAll()) {
                var DayToReturn = _mapper.Map<DayToReturnDto>(Day);
                return CreatedAtRoute ("GetDay", new {userId, reportedDate = Day.ReportedDate}, DayToReturn);
            }

            throw new Exception("Creating the Day fail on save");
        }

        [HttpPost("{reportedDate}")]
        public async Task<IActionResult> DeleteDay (int userId, [FromBody]DateTime reportedDate)
        {
            if (userId != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
            {
                return Unauthorized();
            }

            var messagFromRepo = await _repo.GetDay(reportedDate, userId);

            if (messagFromRepo != null)
                _repo.Delete(messagFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception ("Error deleting the Day");
        }

    }
}