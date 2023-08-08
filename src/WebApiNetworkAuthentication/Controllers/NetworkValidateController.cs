using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiNetworkAuthentication.Data;
using WebApiNetworkAuthentication.Models;

namespace WebApiNetworkAuthentication.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class NetworkValidateController : Controller 
    {

        private readonly ApplicationDbContext _dbContext;
        private static readonly Random random = new Random();
        public NetworkValidateController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }

      

        private UserModel RandomUser(List<UserModel> users)
        {
            if (users == null || users.Count == 0)
            {
                return null; // Return null if the list is empty or null
            }

            int randomIndex = random.Next(users.Count); // Generate a random index within the range of the list

            return users[randomIndex]; // Return the user at the randomly generated index
        }
        private bool IsDataValid(NetworkModel network)
        {
            if (network != null) 
            {
                if (network.NetworkIpAddress != "" & network.Code != "")
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            return false;
        }
    }
}
