using Dboard.Db;
using Microsoft.AspNetCore.Mvc;

namespace Dboard.Api
{
    public class BaseController : ControllerBase
    {

        protected readonly SqliteDbContext db;

        public BaseController(SqliteDbContext dbContext)
        {
            db = dbContext;
        }

    }
}
