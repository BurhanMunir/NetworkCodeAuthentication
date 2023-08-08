using NetworkCodeAuthentication.Models;
using NetworkCodeAuthentication.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace NetworkCodeAuthentication.ViewModels
{
    public class ValidationViewModel : ViewModelBase
    {
        //private fields
        private string _validationCode;
        private string _currentNetwok = "Searching Network....";
        public ValidationViewModel()
        {
            GetNetwork();
        }

        //Properites
        public string CurrentNetwork 
        {
            get { return _currentNetwok; }
            set 
            {
                _currentNetwok = value;
                OnPropertyChanged();
            }
        }

        public string ValidationCode
        {
            get { return _validationCode; }
            set 
            {
                _validationCode = value;
                OnPropertyChanged();
            }
        }

        //Commands
        public ICommand SubmitCommand => new Command(NavigateToWelcomePage);
        private void NavigateToWelcomePage() 
        {
            var user = SubmitAndValidateNetworkAsync();
            if(user!=null) 
            {
                NavigationService.Instance.NavigateToAsync<WelcomeViewModel>(user);
            } 
            else 
            {
                NavigationService.Instance.NavigateToAsync<WelcomeViewModel>("");
            }
           
        }

        public ICommand BackCommand=> new Command(GoBack);

        private async void GoBack()
        {
          await  NavigationService.Instance.NavigateBackAsync();
        }

        //Get network name computer is running on from API
        async void GetNetwork() 
        {
            var network = await NetworkService.Instance.GetComputerConnectionNameAsync();
            if (!string.IsNullOrEmpty(network))
            {
                CurrentNetwork = network;
            }

        }

        //Validate code and network with the record in database
        private  string SubmitAndValidateNetworkAsync() 
        {
            if (ValidationCode != null & !CurrentNetwork.Contains("Searching")) 
            {

                NetworkModel network = new NetworkModel() 
                {
                    NetworkIpAddress = CurrentNetwork,
                    Code = ValidationCode
                };
                var user = NetworkService.Instance.ValidateNetwork(network);
                if (user != null)
                {
                    return user;
                } 
                else 
                {
                    return "";
                }
              
            }
            return "";
        }
    }
}
