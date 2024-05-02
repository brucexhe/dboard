using AntDesign; 
using Dboard.Db;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Dboard.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class WebhookController : BaseController
    {
        public WebhookController(SqliteDbContext dbContext) : base(dbContext)
        {
        }


        // GET api/<WebhookController>/5
        [HttpGet("{id}")]
        public R Get(string id)
        {
            var data = db.Webhooks.FirstOrDefault(f => f.Id == id);

            return R.Success(data);
        }

        // GET api/<WebhookController>/trigger?token=123
        [HttpGet("trigger")]
        public R Trigger(string token)
        {
            var data = db.Webhooks.FirstOrDefault(f => f.Token == token);

            return R.Success(data);
        }

        // POST api/webhook/add
        [HttpPost("add")]
        public void Add([FromBody] Webhook value)
        {

        }


        // DELETE api/webhook/delete/5
        [HttpGet("delete/{id}")]
        public void Delete(string id)
        {

        }
    }
}
