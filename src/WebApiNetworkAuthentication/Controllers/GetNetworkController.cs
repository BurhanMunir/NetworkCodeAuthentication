using Microsoft.AspNetCore.Mvc;

namespace WebApiNetworkAuthentication.Controllers 
{
    [ApiController]
    [Route("[controller]")]
    public class GetNetworkController : ControllerBase 
    {

        [HttpGet("GetNetWork")]
        public async Task<IActionResult> GetClientPublicIPAddress()
        {
            try 
            {
                using (var httpClient = new HttpClient()) 
                {
                    // Make a request to ipify's API to get the public IP address.
                    var response = await httpClient.GetStringAsync("https://api.ipify.org?format=json");
                    var ipAddressInfo = Newtonsoft.Json.JsonConvert.DeserializeObject<dynamic>(response);

                    // Get the "ip" property from the response, which contains the public IP address.
                    string clientPublicIpAddress = ipAddressInfo.ip;

                    return Ok(clientPublicIpAddress);
                }
            } 
            catch (HttpRequestException ex) 
            {
                // Handle any errors that may occur during the HTTP request.
                return StatusCode(500, "Failed to retrieve the public IP address.");
            }
        }
    }

}
