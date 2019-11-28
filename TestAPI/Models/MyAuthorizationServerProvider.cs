﻿using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TestAPI.Models
{
    public class MyAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            string clientId = "";
            string clientSecret = "";
            context.TryGetBasicCredentials(out clientId, out clientSecret);
            context.Validated();
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            UserMasterRepository _repo = new UserMasterRepository();
            var user = _repo.ValidateUser(context.UserName, context.Password, context.Scope[0]); //hardcode for now
            if (user == null)
            {
                context.SetError("invalid_grant", "Provided username and password is incorrect");
                return;
            }
            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim(ClaimTypes.Role, user.UserRole));
            identity.AddClaim(new Claim(ClaimTypes.Name, user.UserName));
            identity.AddClaim(new Claim("RoleType", user.UserType));
            identity.AddClaim(new Claim("Email", user.UserEmail));
            identity.AddClaim(new Claim("UserId", user.UserId));
            context.Validated(identity);
            
        }

    }
}