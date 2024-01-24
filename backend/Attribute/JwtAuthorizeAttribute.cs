using backend.Filters;
using Microsoft.AspNetCore.Mvc;

namespace backend.Attribute
{
    public class JwtAuthorizeAttribute : TypeFilterAttribute
    {
        public JwtAuthorizeAttribute()
           : base(typeof(JwtAuthorizeFilter))
        {
        }
    }
}