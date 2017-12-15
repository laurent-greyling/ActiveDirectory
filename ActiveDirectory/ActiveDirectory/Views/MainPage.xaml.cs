using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ActiveDirectory.Interfaces;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActiveDirectory.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class MainPage : ContentPage
	{
	    public static string AppId = "Application Id";
	    public static string Authority = "https://login.windows.net/common";
	    public static string ReturnUri = "Your return URL";
	    private const string GraphResourceUri = "https://graph.windows.net";

        public MainPage ()
		{
			InitializeComponent ();
		}

	    private async void Signin(object sender, EventArgs e)
	    {
	        try
	        {
	            var auth = DependencyService.Get<IAuthenticator>();
	            var data = await auth.Authenticate(Authority, GraphResourceUri, AppId, ReturnUri);
                
                var userName = $"{data.UserInfo.GivenName} {data.UserInfo.FamilyName}";

	            await Navigation.PushAsync(new LandingPage(userName));
	        }
	        catch (Exception ex)
	        {
	            Debug.WriteLine(ex);
	            throw;
	        }
        }
	}
}
