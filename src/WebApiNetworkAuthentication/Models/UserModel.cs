using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNetworkAuthentication.Models 
{
    [Table("User")]
    public class UserModel
    {
        public int Id { get; set; }
        public string UserName { get; set; }
    }
}
