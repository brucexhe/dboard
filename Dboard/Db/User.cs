

using System.ComponentModel.DataAnnotations.Schema;

namespace Dboard.Db
{

    [Table("user")]
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        public string Password { get; set; }

        public DateTime CreateTime { get; set; }
        public DateTime UpdateTime { get; set; }

        public int Status { get; set; }

        public User() { }

    }
}
