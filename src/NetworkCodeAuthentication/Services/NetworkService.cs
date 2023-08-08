using NetworkCodeAuthentication.Helper;
using NetworkCodeAuthentication.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace NetworkCodeAuthentication.Services 
{
    public class NetworkService 
    {
        private static NetworkService _instance;
         //Implementing Signleton
        public static NetworkService Instance 
        {
            get
            {
                if (_instance == null)
                    _instance = new NetworkService();
                return _instance;
            }
        }
        HttpClient client;
        private  NetworkService()
        {
            var httpClientHandler = new HttpClientHandler();

            httpClientHandler.ServerCertificateCustomValidationCallback =
            (message, cert, chain, errors) => { return true; };

            client = new HttpClient(httpClientHandler);
        }
        public async Task<string> GetComputerConnectionNameAsync() 
        {
            try 
            {
                if (Utils.IsConnectedToInternet())                                  
                {  
                    // Make a request to get IP of the network
                    var response = await client.GetAsync($"{Utils.BaseUrl}Network/GetNetwork");
                    var resContent = await response.Content.ReadAsStringAsync();
                    return resContent;
                }
                else
                {
                    AppServices.LongAlert("Please Connect to Internet");
                }
               
            } 
            catch (HttpRequestException ex)
            {
                // Handle any errors that may occur during the HTTP request.
                return "";
            }
            return "";
        }

        public async Task<bool> SaveNetwork(NetworkModel network)
        {
            try
            {
                if (Utils.IsConnectedToInternet())
                {
                    var request = JsonConvert.SerializeObject(network);
                    HttpContent httpContent = new StringContent(request, Encoding.UTF8, "application/json");

                    // Send the POST request to the API
                    HttpResponseMessage response = await client.PostAsync($"{Utils.BaseUrl}Network/SaveNetwork", httpContent);
                    if (response.IsSuccessStatusCode)
                    {

                        AppServices.ShortAlert("Network Saved");
                        return true;
                    } 
                    else 
                    {
                        AppServices.ShortAlert("Network Not Saved");
                        return false;
                    }
                } 
                else 
                {
                    AppServices.LongAlert("Please Connect to Internet");
                }
               
              
            } 
            catch (Exception ex)
            {
                    //Logs here
            }
            return false;
            
        }

        public  string ValidateNetwork(NetworkModel network)
        {
            try 
             {
                if (Utils.IsConnectedToInternet())
                {
                    var request = JsonConvert.SerializeObject(network);
                    HttpContent httpContent = new StringContent(request, Encoding.UTF8, "application/json");

                    // Send the POST request to the API
                    HttpResponseMessage response = client.PostAsync($"{Utils.BaseUrl}Network/ValidateNetwork", httpContent).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        var resContent = response.Content.ReadAsStringAsync();
                        var responseContent = resContent.Result;
                        if (responseContent != null)
                        {
                            return responseContent;
                        }
                    }
                }
                else
                {
                    AppServices.LongAlert("Please Connect to Internet");
                }
                
                return "";
            }
            catch (Exception ex)
            {
                //Log here....
            }
            return "";

        }
    }
}
