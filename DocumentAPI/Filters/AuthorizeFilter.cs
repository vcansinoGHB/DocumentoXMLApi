using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Protocols;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using DocumentAPI.ErrorHandlers;

namespace DocumentAPI.Filters
{
    public class AuthorizeFilter : Attribute, IAsyncAuthorizationFilter
    {
        public async Task OnAuthorizationAsync(AuthorizationFilterContext context)
        {
            bool IsAuthorized = await VerifyTokenAuthorizationAsync(context);
            if (!IsAuthorized) {
                throw new InvalidTokenException("Access token is expired or invalid");
            }
        }

        private static async Task<bool> VerifyTokenAuthorizationAsync(AuthorizationFilterContext context)
        {

            var token = context.HttpContext.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            
            bool IsValid = true;

            if (token == null) { throw new InvalidTokenException("Access token was no provided."); }

            IsValid = IsExpired(token);

            IsValid = CheckAudience(token);

            IsValid = await IsTokenValid(token);

             return IsValid;            
        }

        static bool IsExpired(string token)
        {
            JwtSecurityToken jwtSecurityToken;
            jwtSecurityToken = new JwtSecurityToken(token);
            return jwtSecurityToken.ValidTo > DateTime.Now;
        }

        static bool CheckAudience(string token)
        {
            bool isAudience = false;
            JwtSecurityToken jwtSecurityToken;
            jwtSecurityToken = new JwtSecurityToken(token);
            IEnumerable<string> audiences = jwtSecurityToken.Audiences;
            var audience = audiences.Where(x => x.Contains("https://invoice-transformation.cti.com")).First();
            if (audience != null)
            {
                isAudience = true;
            }
            return isAudience;
        }


         static async Task<bool> IsTokenValid(string token)
         {
            JwtSecurityToken jwtSecurityToken;
            jwtSecurityToken = new JwtSecurityToken(token);
            bool IsValid = true;

            var issuer = jwtSecurityToken.Issuer;
            var issuerOpenIdUri = "https://dev-otrwvksw.us.auth0.com/.well-known/openid-configuration";

            string stsDiscoveryEndpoint = issuerOpenIdUri;
            var configManager  = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration config = configManager.GetConfigurationAsync().Result;

            var tokenValidationParameters = new TokenValidationParameters()
            {
                ValidAudiences = jwtSecurityToken.Audiences,
                ValidIssuer = issuer,

                IssuerSigningKeys = config.SigningKeys,
                ValidateIssuerSigningKey = true,

                ValidateLifetime = true,
                ValidateAudience = true,
                ValidateIssuer = true
            };

            var claim =  new JwtSecurityTokenHandler().ValidateToken(token, tokenValidationParameters, out _);

            if (claim == null) { IsValid = false; }

            return IsValid;

        }

    }
}