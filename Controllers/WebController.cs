
namespace SecureAPI.Controllers
{
    [Route("web/")]
    [ApiController]

    public class WebController : ControllerBase
    {

        //SecurityContext security;

        private readonly IConfiguration config;
        private readonly WarehouseContext warehouseDb;
        private readonly LoginDatabaseContext logindb;

        public WebController(IConfiguration config, WarehouseContext warehouseDb, LoginDatabaseContext logindb)
        {
            this.config = config;
            this.warehouseDb = warehouseDb;
            this.logindb = logindb;
        }

        [Authorize]
        [HttpGet("absence")]
        public List<AbsenceRegister> getAbsenceRegister([FromForm] string businessId)
        {
            var tennant = warehouseDb.Tennants.
            Where(t => t.businessId == businessId)
            .FirstOrDefault<Tennant>();
            var absence = warehouseDb.AbsenceRegisters
            .Where(i => i.employee.tennant == tennant)
            .ToList();
            return absence;

        }

    }



}