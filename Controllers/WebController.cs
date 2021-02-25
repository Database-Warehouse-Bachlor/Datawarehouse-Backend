using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Datawarehouse_Backend.Models;
using Datawarehouse_Backend;

namespace SecureAPI.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class WebController : ControllerBase
    {
        private static readonly string[] Employee = new[]
        {
            "Eskil", "Emil", "Sverre", "Daniel", "Sigurd", "Petter", "Mari", "Anders", "Sunde", "Trym"
        };

        private readonly ILogger<AbsenceRegister> _logger;

        public WebController(ILogger<AbsenceRegister> logger)
        {
            _logger = logger;
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<AbsenceRegister> Get()
        {
            var rng = new Random();
        
            return Enumerable.Range(0, 9).Select(index => new AbsenceRegister
            {
                Duration = rng.Next(1, 31),
                EmployeeName = Employee[index],
                AbsenceId = index + 1
            })
            .ToArray();
        }
    }
}