using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNet.Security.OpenIdConnect.Primitives;
using AspNet.Security.OpenIdConnect.Server;

namespace BistroFiftyTwo.WebApp.Framework.Security
{
    public class AuthorizationProvider : OpenIdConnectServerProvider
    {
        public override Task ValidateAuthorizationRequest(ValidateAuthorizationRequestContext context)
        {
            return base.ValidateAuthorizationRequest(context);
        }

        public override Task HandleTokenRequest(HandleTokenRequestContext context)
        {
            return base.HandleTokenRequest(context);
        }

        public override Task ValidateTokenRequest(ValidateTokenRequestContext context)
        {
            // Reject the token request that don't use grant_type=password or grant_type=refresh_token.
            if (!context.Request.IsPasswordGrantType() && !context.Request.IsRefreshTokenGrantType())
            {
                context.Reject(
                    error: OpenIdConnectConstants.Errors.UnsupportedGrantType,
                    description: "Only resource owner password credentials and refresh token " +
                                 "are accepted by this authorization server");
                return Task.FromResult(0);
            }
            // Since there's only one application and since it's a public client
            // (i.e a client that cannot keep its credentials private), call Skip()
            // to inform the server the request should be accepted without 
            // enforcing client authentication.
            context.Skip();
            return Task.FromResult(0);
        }
    }
}
