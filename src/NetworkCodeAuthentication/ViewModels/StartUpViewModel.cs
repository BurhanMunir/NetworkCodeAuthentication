using NetworkCodeAuthentication.Helper;
using NetworkCodeAuthentication.Models;
using NetworkCodeAuthentication.Services;
using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace NetworkCodeAuthentication.ViewModels {
    public class StartUpViewModel : ViewModelBase
    {
        //Private Fields
        private string _randomCode;
        private string _currentNetwok="Searching Network....";
        private bool _refreshBtnVisible=false;
        public StartUpViewModel()
        {
            //Make it constant for testing on multipl emulators/simulators at the same time
           // RandomCode = Utils.RandomCode;
              RandomCode=GenerateRandomCode();
           
            GetNetwork();
        }                        

        //Properites
        public string RandomCode 
        {
            get { return _randomCode; }
            set 
            {
                _randomCode = value;
                OnPropertyChanged();
            }
        }

        public string CurrentNetwork 
         {
            get { return _currentNetwok; }
            set 
            {
                _currentNetwok = value;
                OnPropertyChanged();
            }
        }

        public bool RefreshBtnVisible 
        {
            get {return _refreshBtnVisible; }
            set
            {
                _refreshBtnVisible = value;
                OnPropertyChanged();
            }
        }
        //Commands
        public ICommand NextCommand => new Command(NavigateToValidationPage);

        private async void NavigateToValidationPage()
        {
          await  SaveNetworkCode();
           await NavigationService.Instance.NavigateToAsync<ValidationViewModel>();
        }
        public ICommand RefreshCommand => new Command(GetNetwork);

        private async Task<bool>  SaveNetworkCode() 
        {
            NetworkModel network = new NetworkModel() 
            {
                Code = RandomCode,
                NetworkIpAddress=CurrentNetwork
            };
           var result=await NetworkService.Instance.SaveNetwork(network);
            if (result)
            {
                return true;
            }
               

            return false;
        }

        private string GenerateRandomCode()
        {
                  Random random=new Random();
            string digits = "0123456789";
            string alphabets = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            int length = 4;
            char[] code = new char[length];
            // Generate the alphabetic character (second position)
            code[1] = alphabets[random.Next(alphabets.Length)];
            // Generate the remaining digits (first and third positions)
            for (int i = 0; i < length; i++)
            {
                if (i != 1) 
                {
                    code[i] = digits[random.Next(digits.Length)];
                }
            }

            // Convert the character array to a string and return the random code
            return new string(code);
        }

        //Get Connected Network Name
        public async void CheckInternetAndNetworkName()
        {
            try 
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    string networkName = await GetWifiNetworkName();
                    if (!string.IsNullOrEmpty(networkName)) 
                    {
                        Console.WriteLine($"Connected to Wi-Fi network: {networkName}");
                    } 
                    else
                    {
                        Console.WriteLine("Connected via other means (e.g., mobile data or wired connection).");
                    }
                }
                else
                {
                    Console.WriteLine("Not connected to the internet.");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error: {ex.Message}");
            }                                                             
        }

        async Task<string> GetWifiNetworkName() {
            try {
                var currentNetwork = Connectivity.ConnectionProfiles.FirstOrDefault();
                if (currentNetwork == ConnectionProfile.WiFi) {
                    var wifiInfo = currentNetwork.ToString();
                    return wifiInfo;
                }
            } catch (Exception ex) {
                // Handle any exception that may occur while accessing network information
                Console.WriteLine($"Error: {ex.Message}");
            }

            return null;
        }

        async void GetNetwork()
        {
            var network =await NetworkService.Instance.GetComputerConnectionNameAsync();
            if (!string.IsNullOrEmpty(network))
            {
                CurrentNetwork = network;
                RefreshBtnVisible = false;
            }
            else
            {
                CurrentNetwork = "Not Found";
                RefreshBtnVisible = true;
            }

        }
    }
}
