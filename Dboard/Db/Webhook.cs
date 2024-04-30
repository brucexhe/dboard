using System.ComponentModel.DataAnnotations.Schema;

namespace Dboard.Db
{
    [Table("webhook")]
    public class Webhook
    {

        public string Id { get; set; }

        public string ContainerName { get; set; }

        public string Token { get; set; }

        public DateTime CreateTime { get; set; }
    }
}
