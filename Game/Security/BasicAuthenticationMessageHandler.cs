/*using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Security;
using webapirestfull.Data.SqlServer.Repositories.Interfaces;
using webapirestfull.Web.Common.ExceptionHandler;
using webapirestfull.Web.Api.Models.Membership;

namespace Game.Security
{
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        public const string BasicScheme = "Basic";
        public const string ChallengeAuthenticationHeaderName = "WWW-Authenticate";
        public const char AuthorizationHeaderSeparator = ':';


        private readonly IActionExceptionHandler _actionExceptionHandler;
        private readonly IUserRepository _repository;

        public BasicAuthenticationMessageHandler(IActionExceptionHandler actionExceptionHandler,
            IUserRepository repository)
        {
            _actionExceptionHandler = actionExceptionHandler;
            _repository = repository;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request,
            CancellationToken cancellationToken)
        {
            AuthenticationHeaderValue authHeader = request.Headers.Authorization;
            if (authHeader == null)
            {
                return CreateUnauthorizedResponse();
            }

            if (authHeader.Scheme != BasicScheme)
            {
                return CreateUnauthorizedResponse();
            }

            string encodedCredentials = authHeader.Parameter;
            byte[] credentialBytes = Convert.FromBase64String(encodedCredentials);
            string credentials = Encoding.ASCII.GetString(credentialBytes);
            string[] credentialParts = credentials.Split(AuthorizationHeaderSeparator);

            if (credentialParts.Length != 2)
            {
                return CreateUnauthorizedResponse();
            }

            string username = credentialParts[0].Trim();
            string password = credentialParts[1].Trim();
            try
            {
                if (!_repository.ValidateUser(username, password))
                {
                    return CreateUnauthorizedResponse();
                }
            }
            catch (Exception ex)
            {
                HttpResponseMessage response;
                throw new HttpResponseException(_actionExceptionHandler.HttpResponseMessageHandleException(ex,
                    out response));
            }


            SetPrincipal(username);
            return base.SendAsync(request, cancellationToken);

        }

        private void SetPrincipal(string username)
        {
            string[] roles = _repository.GetRolesForUser(username);
            var user = Membership.GetUser(username);
            var mappedUser = new User
            {
                username = user.UserName,
                email = user.Email,
                firstname = string.Empty,
                lastname = string.Empty,
                userid = Guid.Empty,
                roles = roles
            };
            GenericIdentity identity = CreateIdentity(username, mappedUser);


            var principal = new GenericPrincipal(identity, roles);
            Thread.CurrentPrincipal = principal;

            if (HttpContext.Current != null)
            {
                HttpContext.Current.User = principal;
            }
        }

        private GenericIdentity CreateIdentity(string username, User mappedUser)
        {
            var identity = new GenericIdentity(username, BasicScheme);
            identity.AddClaim(new Claim(ClaimTypes.Sid, mappedUser.userid.ToString()));
            identity.AddClaim(new Claim(ClaimTypes.GivenName, mappedUser.firstname));
            identity.AddClaim(new Claim(ClaimTypes.Surname, mappedUser.lastname));
            identity.AddClaim(new Claim(ClaimTypes.Email, mappedUser.email));
            foreach (string role in mappedUser.roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, role));
            }

            return identity;
        }

        private Task<HttpResponseMessage> CreateUnauthorizedResponse()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Headers.Add(ChallengeAuthenticationHeaderName, BasicScheme);

            var taskCompletionSource = new TaskCompletionSource<HttpResponseMessage>();
            taskCompletionSource.SetResult(response);
            return taskCompletionSource.Task;
        }
    }
}*/