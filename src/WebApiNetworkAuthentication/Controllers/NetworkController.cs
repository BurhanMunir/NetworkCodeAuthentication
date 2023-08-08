using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApiNetworkAuthentication.Data;
using WebApiNetworkAuthentication.Models;

namespace WebApiNetworkAuthentication.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class NetworkController : ControllerBase
    {

        private readonly ApplicationDbContext _dbContext;
        private static readonly Random random = new Random();
        public NetworkController(ApplicationDbContext dbContext) 
        {
            _dbContext = dbContext;
        }
        [HttpPost("SaveNetwork")]
        public IActionResult AddNetWork(NetworkModel network) 
        {
            try
            {
                if (IsDataValid(network)) 
                {
                    // Add the item to the Items DbSet
                    _dbContext.Add(network);

                    // Save the changes to the database
                    var isSaved = _dbContext.SaveChanges();
                    if (isSaved == 1) 
                    {
                        return Ok("Saved Successfully");
                    }
                    else
                    {
                        return Ok("Network Not saved, Something went wrong");
                    }

                } 
                else
                {
                    return BadRequest("Data Invalid");
                }
            } 
            catch (Exception)
            {

                return StatusCode(500, "Failed to save network.");
            }


        }

        [HttpGet("GetNetWork")]
        public async Task<IActionResult> GetClientPublicIPAddress() {
            try {
                using (var httpClient = new HttpClient()) {
                    // Make a request to ipify's API to get the public IP address.
                    var response = await httpClient.GetStringAsync("https://api.ipify.org?format=json");
                    var ipAddressInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

                    // Get the "ip" property from the response, which contains the public IP address.
                    string clientPublicIpAddress = ipAddressInfo.ip;

                    return Ok(clientPublicIpAddress);
                }
            } catch (HttpRequestException ex) {
                // Handle any errors that may occur during the HTTP request.
                return StatusCode(500, "Failed to retrieve the public IP address.");
            }
        }

        [HttpPost("ValidateNetwork")]
        public async Task<IActionResult> ValidateNetworkAsync(NetworkModel networkModel) {
            try {
                if (IsDataValid(networkModel)) {
                    var matchingNetwork = await _dbContext.Networks
      .FirstOrDefaultAsync(n => n.NetworkIpAddress == networkModel.NetworkIpAddress & n.Code == networkModel.Code);
                    if (matchingNetwork != null) {

                        UserModel user;
                        List<UserModel> users = _dbContext.Users.ToList();
                        if (users.Count != 0) {
                            user = RandomUser(users);
                            return Ok(user.UserName);
                        } else {
                            return Ok("No User Found");
                        }

                    }
                } else {
                    return StatusCode(400, "Invalid Data");
                }
                return BadRequest("Invalid Request");
            } catch (Exception ex) {
                return StatusCode(500, "Internal Server Error");
            }

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

        [HttpGet("GetRandomUser")]
        public IActionResult GetRAndomUser()
        {
            try 
            {
                UserModel user;
                List<UserModel> users = _dbContext.Users.ToList();
                if (users.Count != 0) 
                {
                    user = RandomUser(users);
                    return Ok(user);
                } 
                else 
                {
                    return Ok("No User Found");
                }


            } 
            catch (Exception) 
            {

                return StatusCode(500, "Failed to save network.");
            }

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
    }
}
