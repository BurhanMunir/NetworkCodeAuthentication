## NetworkCodeAuthentication
#Basic Setups
Project Uses xamarin forms and Asp .net core web api for communicationg with SQL
xamarin forms project and web api projects are in the same solution.

##****Xamarin Forms Project**
Project uses MVVM pattern and some services and helper
There is a networkservice for communicationg with api's(database)
There is a navigationservice that is implemented for page navigation purpose and registering viewmodels against views in this way we can navigating through viewmodels easily.
For showing toast messages some native functionality is also implemented in AppServices class by the help of IMessage interface that has implementation in each platform.

**Views And ViewModels**
We have three views first is StartUpview with Startup ViewModel, second is ValidationView with ValidationViewModel and third is WelcomeView with WelcomViewModel.
First Page
At first Page Four are three controls lable for showing random four digit controls, label for Ip Address of the computer,refesh button(invisible) and Next Button.
At first page application loads and make search for the IP address of the computer by API, until app IP is found there is text "Searching network..." in lable text, if IP found that is shown in label text other wise it shows "Not Found" and Refresh button is visible to make search again no need to satrt the app again.
![Alt Text](images/page1.png)
![Alt Text](images/Refresh.jpeg)
