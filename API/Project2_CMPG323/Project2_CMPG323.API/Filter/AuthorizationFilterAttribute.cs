using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Authentication;
using System.Security.Claims;
using System.Threading;
using System.Web;
using System.IdentityModel.Tokens.Jwt;

namespace Project2_CMPG323.API.Filter
{
	public class AuthorizationFilterAttribute : ActionFilterAttribute
	{
		public AuthorizationFilterAttribute()
		{

		}

		public override void OnActionExecuting(ActionExecutingContext context)
		{
			try
			{
                var userClaimskeys = context.HttpContext.Request.Headers;
                var token = userClaimskeys.First(x => x.Key.Contains("Authorization")).Value.ToString().Substring(7);
                var handler = new JwtSecurityTokenHandler();
                var jwt = handler.ReadJwtToken(token);
                var UserId = Int32.Parse(jwt.Claims.First(claim => claim.Type == "UserId").Value);
            }
			catch (Exception)
			{
				throw new UnauthorizedAccessException();
			}
		}
	}
}
