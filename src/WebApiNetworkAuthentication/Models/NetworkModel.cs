using System.ComponentModel.DataAnnotations.Schema;

namespace WebApiNetworkAuthentication.Models 
{
    [Table("Network")]
    public class NetworkModel
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string NetworkIpAddress { get; set; }
    }
}
