using Dboard.Db;
using log4net;
using Microsoft.AspNetCore.Mvc;

namespace Dboard.Api
{
    public class BaseController : ControllerBase
    {
        protected ILog log = LogManager.GetLogger("controller");

        protected readonly SqliteDbContext db;

        public BaseController(SqliteDbContext dbContext)
        {
            db = dbContext;
        }

    }
}
