using System;
using System.Threading.Tasks;
using ActiveDirectory.Interfaces;
using Android.Webkit;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ActiveDirectory.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class LandingPage : ContentPage
	{
	    public static string Authority = "https://login.windows.net/common";
        public string DisplayMessage { get; set; }

	    public LandingPage (string userName)
		{
			InitializeComponent ();

		    DisplayMessage = $"Welcome {userName}, you are now signed in with Active Directory";

		    BindingContext = this;
		}

	    private async Task SignOut(object sender, EventArgs e)
	    {
	        var auth = DependencyService.Get<IAuthenticator>();
            auth.UnAuthenticate(Authority);

            CookieManager.Instance.RemoveAllCookie();

	        await Navigation.PushAsync(new MainPage());
	    }

	}
}