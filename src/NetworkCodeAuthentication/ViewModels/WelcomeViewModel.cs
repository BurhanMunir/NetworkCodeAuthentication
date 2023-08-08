using System.Threading.Tasks;

namespace NetworkCodeAuthentication.ViewModels {
    public class WelcomeViewModel : ViewModelBase
    {
        private string _userName;
        public WelcomeViewModel() 
        {

        }

        public string UserName 
        {
            get => _userName;
            set 
            {
                _userName = value;
                OnPropertyChanged();
            }
        }
        public override Task InitializeAsync(object navigationData) 
        {
            if (navigationData is string)
                UserName = navigationData.ToString();

            return base.InitializeAsync(navigationData);
        }
    }
}
