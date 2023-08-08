using NetworkCodeAuthentication.Interfaces;
using Xamarin.Forms;

namespace NetworkCodeAuthentication.Services 
{
    public static class AppServices 
    {
        public static void ShortAlert(string message) 
        {
            DependencyService.Get<IMessage>().ShortAlert(message);
        }

        public static void LongAlert(string message) 
        {
            DependencyService.Get<IMessage>().LongAlert(message);
        }

        public static void ExitApp()
        {
            DependencyService.Get<IMessage>().ExitApp();
        }
    }
}
