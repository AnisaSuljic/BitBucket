using Azure.Core;
using CustomerServiceCampaign.DTOs;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace CustomerServiceCampaign.Security
{
    public class BasicAuthenticationHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly Services.Interfaces.IAuthService _authService;
        public BasicAuthenticationHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder,
            ISystemClock clock, Services.Interfaces.IAuthService authService) : base(options, logger, encoder, clock)
        {
            _authService = authService;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
                return AuthenticateResult.Fail("Authorization header is missing");

            string authenticationHeader = Request.Headers["Authorization"];
            if (string.IsNullOrEmpty(authenticationHeader) || !authenticationHeader.StartsWith("Basic "))
                return AuthenticateResult.Fail("Invalid Authorization header");

            string encodedUsernamePassword = authenticationHeader.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries)[1]?.Trim();
            string decodedUsernamePassword = Encoding.UTF8.GetString(Convert.FromBase64String(encodedUsernamePassword));
            string username = decodedUsernamePassword.Split(':', 2)[0];
            string password = decodedUsernamePassword.Split(':', 2)[1];

            if (await _authService.Login(username, password) != null)
            {
                var claims = new[] { new Claim(ClaimTypes.Name, username) };
                var identity = new ClaimsIdentity(claims, Scheme.Name);
                var principal = new ClaimsPrincipal(identity);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);

                return AuthenticateResult.Success(ticket);
            }
            else
            {
                return AuthenticateResult.Fail("Invalid username or password");
            }
        }
    }

}
