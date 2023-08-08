using Xamarin.Essentials;

namespace NetworkCodeAuthentication.Helper
 {
    //for constants values
    public class Utils 
    {

        public static string BaseUrl = "https://192.168.8.101:45456/";

        public static string RandomCode { get; set; } = "4F61";
        public static bool IsConnectedToInternet() 
        {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet) 
            {
                return true;
            }
            else
            {
                 return false;
            }
        }
    }
}
