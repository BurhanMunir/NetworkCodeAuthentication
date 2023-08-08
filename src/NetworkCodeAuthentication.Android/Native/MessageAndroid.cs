using Android.App;
using Android.Views;
using Android.Widget;
using NetworkCodeAuthentication.Droid.Native;
using NetworkCodeAuthentication.Interfaces;
using Plugin.CurrentActivity;

[assembly: Xamarin.Forms.Dependency(typeof(MessageAndroid))]
namespace NetworkCodeAuthentication.Droid.Native {
    public class MessageAndroid : IMessage 
    {
        public void LongAlert(string message) 
        {
            Toast toast = Toast.MakeText(Application.Context, message, ToastLength.Long);
            toast.SetGravity(GravityFlags.Bottom, 0, -10);
            toast.Show();
            //Toast.MakeText(Application.Context, message, ToastLength.Long).Show();
        }

        public void ShortAlert(string message) 
        {
            Toast toast = Toast.MakeText(Application.Context, message, ToastLength.Short);
            toast.SetGravity(GravityFlags.Bottom, 0, -10);
            toast.Show();
            //Toast.MakeText(Application.Context, message, ToastLength.Short).Show();
        }

        public void ExitApp() 
        {
            CrossCurrentActivity.Current.Activity.Finish();
        }
    }
}