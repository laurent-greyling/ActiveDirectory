using System;
using System.Linq;
using System.Threading.Tasks;
using ActiveDirectory.Interfaces;
using Android.App;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using Xamarin.Forms;

[assembly: Dependency(typeof(ActiveDirectory.Droid.Helper.Authenticator))]
namespace ActiveDirectory.Droid.Helper
{
    class Authenticator : IAuthenticator
    {
        private AuthenticationContext _authContext;

        public async Task<AuthenticationResult> Authenticate(string authority, string resource, string clientId, string returnUri)
        {
            try
            {
                Context(authority);

                var uri = new Uri(returnUri);
                var platformParams = new PlatformParameters((Activity)Forms.Context);
                var auth =  await _authContext.AcquireTokenAsync(resource, clientId, uri, platformParams);
                
                return auth;
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void UnAuthenticate(string authority)
        {
            try
            {
                Context(authority);
                _authContext.TokenCache.Clear();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        private void Context(string authority)
        {
            _authContext = new AuthenticationContext(authority);

            if (_authContext.TokenCache.ReadItems().Any())
                _authContext = new AuthenticationContext(_authContext.TokenCache.ReadItems().First().Authority);
        }
    }
}