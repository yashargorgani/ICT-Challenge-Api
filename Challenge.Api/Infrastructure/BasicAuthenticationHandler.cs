using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Challenge.Api.Infrastructure
{
    public class BasicAuthenticationHandler : DelegatingHandler
    {
        private const string basicAuthenticationResponseHeader = "WWW-Authenticate";
        private const string basicAuthenticationResponseHeaderValue = "Basic";

        private readonly ChallengePrincipalProvider principalProvider;

        public BasicAuthenticationHandler(ChallengePrincipalProvider principalProvider)
        {
            this.principalProvider = principalProvider;
        }

        protected async override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            var authValue = request.Headers.Authorization;

            if (authValue != null && !string.IsNullOrWhiteSpace(authValue.Parameter))
            {
                var parsedCredentials = ParseAuthorizationHeader(authValue.Parameter);

                request.GetRequestContext().Principal =
                   await principalProvider.CreatePrincipals(parsedCredentials.UserName, parsedCredentials.PassWord);
            }

            return await base.SendAsync(request, cancellationToken).ContinueWith(task =>
            {
                var response = task.Result;
                if (response.StatusCode == HttpStatusCode.Unauthorized &&
                !response.Headers.Contains(basicAuthenticationResponseHeader))
                    response.Headers.Add(basicAuthenticationResponseHeader, basicAuthenticationResponseHeaderValue);

                return response;
            });
        }

        private Credentials ParseAuthorizationHeader(string authHeader)
        {
            var credentials = Encoding.ASCII.GetString(Convert.FromBase64String(authHeader)).Split(':');

            if (credentials.Length != 2 ||
                credentials.Any(string.IsNullOrEmpty))
                return null;

            return new Credentials()
            {
                UserName = credentials[0],
                PassWord = credentials[1]
            };
        }
    }

    public class Credentials
    {
        public string UserName { get; set; }
        public string PassWord { get; set; }
    }
}